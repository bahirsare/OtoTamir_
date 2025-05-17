using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreClientDal:EfCoreGenericRepositoryDal<Client, DataContext>,IClientDal
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
            var entities = _context.Clients.Include(i => i.Vehicles).AsQueryable();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        }
    }
}
