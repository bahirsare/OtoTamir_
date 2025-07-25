﻿using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class ClientService : IClientService
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

        public int Delete(int id)
        {
            return  _clientDal.Delete(id);
        }

       

        public async Task<List<Client>> GetAllAsync(string mechanicId, 
            bool includeVehicles, 
            bool includeServiceRecords,
            Expression<Func<Client, bool>> filter = null)
        {
            return await _clientDal.GetAllAsync(mechanicId,includeVehicles,includeServiceRecords,filter);
        }

        public async Task<Client> GetOneAsync(
        int id,
        string mechanicId,
        bool includeVehicles,
        bool includeServiceRecords)
        {
            return await _clientDal.GetOneAsync(id,mechanicId,includeVehicles,includeServiceRecords);
        }

        public async Task<int> UpdateAsync()
        {
            return await _clientDal.UpdateAsync();
        }
    }
}
