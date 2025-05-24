using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreClientDal : EfCoreGenericRepositoryDal<Client, DataContext>, IClientDal
    {
        private readonly DataContext _context;

        public EfCoreClientDal(DataContext context) : base(context)
        {
            _context = context;

        }
        public override async Task<int> CreateAsync(Client client)
        {

            client.CreatedDate = DateTime.Now;
            client.ModifiedDate = DateTime.Now;

            return await base.CreateAsync(client);
        }
        public async Task<List<Client>> GetAllAsync(string mechanicId, Expression<Func<Client, bool>> filter = null)
        {
            if (string.IsNullOrEmpty(mechanicId))
                throw new ArgumentException("mechanicId cannot be null or empty.", nameof(mechanicId));

            var entities = _context.Clients
                .Include(c => c.Vehicles)
                    .ThenInclude(v => v.ServiceRecords)
                .Where(c => c.MechanicId == mechanicId);

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            return await entities.ToListAsync();
        }
        public override Task<List<Client>> GetAllAsync(Expression<Func<Client, bool>> filter = null)
        {
            throw new NotSupportedException("This method is not supported for Client. Please use the version with mechanicId.");
        }
        public override Task<List<Client>> GetAllAsync()
        {
            throw new NotSupportedException("This method is not supported for Client. Please use the version with mechanicId.");
        }


        public async Task<List<Client>> GetAllAsync(string mechanicId)
        {
            var entities =  _context.Clients.Include(i => i.Vehicles).ThenInclude(i => i.ServiceRecords).Where(c => c.MechanicId == mechanicId); ;

            return await entities.ToListAsync();
        }
        public override async Task<Client> GetOneAsync(int id)
        {
            return await _context.Clients.Include(i => i.Vehicles).ThenInclude(i => i.ServiceRecords).FirstOrDefaultAsync(i => i.Id == id);
        }

    }
}
