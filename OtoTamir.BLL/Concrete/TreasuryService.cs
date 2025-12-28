using AutoMapper;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context; // DataContext için
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class TreasuryService : ITreasuryService
    {
        private readonly ITreasuryDal _treasuryDal;
        private readonly IBankDal _bankDal;
        private readonly IBankCardDal _bankCardDal;
        private readonly ITreasuryTransactionDal _transactionDal;
        private readonly IClientDal _clientDal;
        private readonly IPosTerminalDal _posTerminalDal;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TreasuryService(
            ITreasuryDal treasuryDal,
            IBankDal bankDal,
            IBankCardDal bankCardDal,
            ITreasuryTransactionDal transactionDal,
            IClientDal clientDal,
            IPosTerminalDal posTerminalDal,
            DataContext context, IMapper mapper)
        {
            _treasuryDal = treasuryDal;
            _bankDal = bankDal;
            _bankCardDal = bankCardDal;
            _transactionDal = transactionDal;
            _clientDal = clientDal;
            _posTerminalDal = posTerminalDal;
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> AnyAsync(Expression<Func<Treasury, bool>> filter)
        {
            return _treasuryDal.AnyAsync(filter);
        }

        public Task<int> CreateAsync(Treasury entity)
        {
            return _treasuryDal.CreateAsync(entity);
        }

        public int Delete(int id)
        {
            return _treasuryDal.Delete(id);
        }

        public Task<List<Treasury>> GetAllAsync(string mechanicId, int treasuryId, Expression<Func<Treasury, bool>> filter = null)
        {
            return _treasuryDal.GetAllAsync(mechanicId, treasuryId, filter);
        }



        public Task<Treasury> GetOneAsync(int id, string mechanicId)
        {
            return _treasuryDal.GetOneAsync(id, mechanicId);
        }

        public Task<decimal> GetTotalBalanceAsync(int treasuryId, string mechanicId)
        {
            return _treasuryDal.GetTotalBalanceAsync(treasuryId, mechanicId);
        }

        public Task<int> UpdateAsync()
        {
            return _treasuryDal.UpdateAsync();
        }
        public async Task<TreasuryDashboardDTO> GetDashboardDataAsync(string mechanicId, int treasuryId)
        {
            var model = new TreasuryDashboardDTO();


            var treasury = await _treasuryDal.GetOneAsync(treasuryId, mechanicId);
            if (treasury == null)
            {
                return new TreasuryDashboardDTO { Treasury = new Treasury() };
            }
            model.Treasury = treasury;

            model.Banks = await _bankDal.GetAllAsync(mechanicId);
            model.BankCards = await _bankCardDal.GetAllAsync(mechanicId);


            var transactions = await _transactionDal.GetAllAsync(mechanicId, treasury.Id);
            model.Transactions = transactions.OrderByDescending(x => x.TransactionDate).Take(10).ToList();

            var clients = await _clientDal.GetAllAsync(mechanicId, false, false, c => c.Balance > 0);
            model.Treasury.ReceivablesBalance = clients.Sum(c => c.Balance);



            var posTerminals = await _posTerminalDal.GetAllAsync(mechanicId);
            var posTransactions = await _transactionDal.GetAllAsync(mechanicId, (int)treasuryId,
        x => x.PosTerminalId != null && x.TransactionType == TransactionType.Incoming);
            var posSummaryList = new List<PosTerminalSummaryDTO>();
            var now = DateTime.Now;
            var lastMonthStart = now.AddMonths(-1).Date;

            foreach (var pos in posTerminals)
            {

                var summaryItem = _mapper.Map<PosTerminalSummaryDTO>(pos);


                var myTrans = posTransactions.Where(t => t.PosTerminalId == pos.Id).ToList();

                summaryItem.BlockedBalance = myTrans
                    .Where(t => t.MaturityDate != null && t.MaturityDate > now)
                    .Sum(t => t.Amount);

                summaryItem.LastMonthTurnover = myTrans
                    .Where(t => t.TransactionDate >= lastMonthStart)
                    .Sum(t => t.Amount);

                posSummaryList.Add(summaryItem);
            }

            model.PosTerminals = posSummaryList;



            return model;
        }
        public async Task UpdateCashBalanceAsync(int treasuryId, string mechanicId, decimal amount)
        {
            
            await _treasuryDal.UpdateCashBalanceAsync(treasuryId, mechanicId, amount);
        }
    }
}