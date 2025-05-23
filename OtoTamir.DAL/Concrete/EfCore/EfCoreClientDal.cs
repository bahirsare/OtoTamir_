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
        public override async Task<List<Client>> GetAllAsync(Expression<Func<Client, bool>> filter = null)
        {
            var entities = _context.Clients.Include(i => i.Vehicles).ThenInclude(i => i.ServiceRecords).AsQueryable();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        } 
        public override async Task<List<Client>> GetAllAsync()
        {
            var entities =  _context.Clients.Include(i => i.Vehicles).ThenInclude(i => i.ServiceRecords).AsQueryable();

            return entities.ToList();
        }
        public override async Task<Client> GetOneAsync(int id)
        {
            return await _context.Clients.Include(i => i.Vehicles).ThenInclude(i => i.ServiceRecords).FirstOrDefaultAsync(i => i.Id == id);
        }

    }
}
