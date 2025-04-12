using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class ClientService:IClientService
    {   
        private readonly IClientDal _clientDal;

        public ClientService(IClientDal clientDal)
        {
            _clientDal = clientDal;
        }

        public int Create(Client Entity)
        {
            return _clientDal.Create(Entity);
        }

        public int Delete(int id)
        {
            return _clientDal.Delete(id);
        }

        public List<Client> GetAll()
        {
            return _clientDal.GetAll();
        }

        public List<Client> GetAll(Expression<Func<Client, bool>> filter = null)
        {
            return _clientDal.GetAll(filter);
        }

        public Client GetOne(int id)
        {
            return _clientDal.GetOne(id);
        }      

        public int Update()
        {
            return _clientDal.Update();
        }
    }
}
