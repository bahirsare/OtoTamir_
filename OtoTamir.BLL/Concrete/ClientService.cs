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

        public async Task    <List<Client>>  GetAllAsync()
        {
            return await _clientDal.GetAllAsync();
        }

        public async Task<List<Client>>  GetAllAsync(Expression<Func<Client, bool>> filter = null)
        {
            return await _clientDal.GetAllAsync(filter);
        }

        public async Task<Client> GetOneAsync(int id)
        {
            return await _clientDal.GetOneAsync(id);
        }      

        public async Task<int > UpdateAsync()
        {
            return await _clientDal.UpdateAsync();
        }
    }
}
