using Microsoft.AspNetCore.Identity;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace OtoTamir.BLL.Concrete
{
    public class MechanicService : IMechanicService
    {
        private readonly IMechanicDal _mechanicDal;
        private readonly UserManager<Mechanic> _userManager;

        public MechanicService(IMechanicDal mechanicDal, UserManager<Mechanic> userManager)
        {
            _mechanicDal = mechanicDal;
            _userManager = userManager;
        }

        public async Task<int> CreateAsync(Mechanic mechanic)
        {
            return await _mechanicDal.CreateAsync(mechanic);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _mechanicDal.DeleteAsync(id);
        }

        public async Task<List<Mechanic>> GetAllAsync()
        {
            return await _mechanicDal.GetAllAsync();
        }

        public async Task<List<Mechanic>> GetAllAsync(Expression<Func<Mechanic, bool>> filter = null)
        {
            return await _mechanicDal.GetAllAsync(filter);
        }

        public async Task<Mechanic> GetOneAsync(int id)
        {
            return await _mechanicDal.GetOneAsync(id);
        }

        public async Task<int> UpdateAsync()
        {
            return await _mechanicDal.UpdateAsync();
        }

        public async Task<Mechanic> GetOneAsync(string id)
        {
            return await _mechanicDal.GetOneAsync(id);
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _mechanicDal.DeleteAsync(id);
        }

        public async Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName)
        {
            return await _mechanicDal.CreateMechanicAsync(storeName);
        }

        public string GenerateRandomPassword()
        {
            return _mechanicDal.GenerateRandomPassword();
        }
    }
}
