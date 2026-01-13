using Microsoft.AspNetCore.Identity;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class MechanicService : IMechanicService
    {
        private readonly IMechanicDal _mechanicDal;
        private readonly UserManager<Mechanic> _userManager;
        private readonly ITreasuryService _treasuryService; 

        public MechanicService(IMechanicDal mechanicDal, UserManager<Mechanic> userManager, ITreasuryService treasuryService)
        {
            _mechanicDal = mechanicDal;
            _userManager = userManager;
            _treasuryService = treasuryService;
        }

        public async Task<int> CreateAsync(Mechanic mechanic)
        {
             return await _mechanicDal.CreateAsync(mechanic);
           

        }

        public  async Task<int> DeleteAsync(int id)
        {
            return await _mechanicDal.DeleteAsync(id);
        }

        

        public async Task<List<Mechanic>> GetAllAsync(
            bool includeClient,
            bool includeVehicle, 
            Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy = null,
            Expression<Func<Mechanic, bool>> filter = null)
        {
            return await _mechanicDal.GetAllAsync(includeClient, includeVehicle, orderBy, filter);
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
            var result = await _mechanicDal.CreateMechanicAsync(storeName);
            var mechanic = await _mechanicDal.GetAllAsync(false,false,null,m => m.StoreName == storeName);
            var treasury = new Treasury
            {
                
                CashBalance = 0,
                ReceivablesBalance = 0,
                CreatedDate = DateTime.Now,
                MechanicId = mechanic[0].Id
            };
            var treasuryResult = await _treasuryService.CreateAsync(treasury);
           
                return result;
          

        }

        public string GenerateRandomPassword()
        {
            return _mechanicDal.GenerateRandomPassword();
        }

        public async Task<bool> AnyAsync(Expression<Func<Mechanic, bool>> filter)
        {
            return await _mechanicDal.AnyAsync(filter);
        }

        Task<PagedResult<Mechanic>> IRepositoryService<Mechanic>.GetPagedAsync(Expression<Func<Mechanic, bool>> filter, Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy, int page, int pageSize, params Expression<Func<Mechanic, object>>[] includes)
        {
            return _mechanicDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }
    }
}
