using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int Create(Client client)
        {

            client.CreatedDate = DateTime.Now;
            client.ModifiedDate = DateTime.Now;
            
            return base.Create(client);
        }
    }
}
