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
        public override int Create(Client client)
        {

            client.CreatedDate = DateTime.Now;
            client.ModifiedDate = DateTime.Now;
            
            return base.Create(client);
        }
        public override List<Client> GetAll(Expression<Func<Client, bool>> filter = null)
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
