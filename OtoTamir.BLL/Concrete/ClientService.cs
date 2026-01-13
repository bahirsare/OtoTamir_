using AutoMapper;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class ClientService : IClientService
    {
        private readonly IClientDal _clientDal;
        private readonly IMapper _mapper;
        private readonly ITreasuryTransactionDal _transactionDal;

        public ClientService(IClientDal clientDal, ITreasuryTransactionDal transactionDal,IMapper mapper)
        {
            _clientDal = clientDal;
            _transactionDal = transactionDal;
            _mapper = mapper;
        }

        public async Task<bool> AnyAsync(Expression<Func<Client, bool>> filter)
        {
            return await _clientDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(Client Entity)
        {
            return await _clientDal.CreateAsync(Entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _clientDal.DeleteAsync(id);
        }



        public async Task<List<Client>> GetAllAsync(string mechanicId,
            bool includeVehicles,
            bool includeServiceRecords,
            Expression<Func<Client, bool>> filter = null)
        {
            return await _clientDal.GetAllAsync(mechanicId, includeVehicles, includeServiceRecords, filter);
        }

        public async Task<Client> GetOneAsync(
        int id,
        string mechanicId,
        bool includeVehicles,
        bool includeServiceRecords)
        {
            return await _clientDal.GetOneAsync(id, mechanicId, includeVehicles, includeServiceRecords);
        }

        public async Task<int> UpdateAsync()
        {
            return await _clientDal.UpdateAsync();
        }
        public async Task<decimal> GetTotalReceivablesAsync(string mechanidId)
        {
            return await _clientDal.GetTotalReceivablesAsync(mechanidId);
        }
        public async Task<ClientStatementDTO> GetClientStatementAsync(int clientId, string mechanicId, int treasuryId)
        {
            
            var client = await _clientDal.GetOneAsync(clientId, mechanicId, true, true);
            if (client == null) return null;

            
            var model = _mapper.Map<ClientStatementDTO>(client);

            var statementItems = new List<StatementItem>();

            if (client.Vehicles != null)
            {
                var services = client.Vehicles.SelectMany(v => v.ServiceRecords).ToList();
                statementItems.AddRange(_mapper.Map<List<StatementItem>>(services));
            }

            var payments = await _transactionDal.GetAllAsync(
                mechanicId,
                treasuryId,
                filter: x => x.ClientId == clientId && x.TransactionType == TransactionType.Incoming
            );
            statementItems.AddRange(_mapper.Map<List<StatementItem>>(payments));

           
            model.Transactions = statementItems.OrderByDescending(x => x.Date).ToList();

            model.TotalDebt = model.Transactions.Where(x => x.Type == "DEBT").Sum(x => x.Amount);
            model.TotalPaid = model.Transactions.Where(x => x.Type == "PAYMENT").Sum(x => x.Amount);

           

            return model;
        }

        public async Task<bool> UpdateBalanceAsync(string mechanicId, int clientId, decimal amount) { 
            return await _clientDal.UpdateBalanceAsync(mechanicId, clientId, amount);
        }

        Task<PagedResult<Client>> IRepositoryService<Client>.GetPagedAsync(Expression<Func<Client, bool>> filter, Func<IQueryable<Client>, IOrderedQueryable<Client>> orderBy, int page, int pageSize, params Expression<Func<Client, object>>[] includes)
        {
           return _clientDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }
    }
}
