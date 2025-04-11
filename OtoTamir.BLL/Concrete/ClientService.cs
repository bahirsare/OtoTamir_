using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Concrete.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class ClientService
    {
        private readonly EfCoreClientDal _efCoreClientDal;

        public ClientService(EfCoreClientDal efCoreClientDal)
        {
            _efCoreClientDal = efCoreClientDal;
        }

        public List<Client> List()
        {
            return _efCoreClientDal.GetAll();
        }

    }
}
