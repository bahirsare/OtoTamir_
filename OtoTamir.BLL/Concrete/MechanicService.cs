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



        public MechanicService(IMechanicDal mechanicDal, UserManager<Mechanic> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mechanicDal = mechanicDal;
            _userManager = userManager;

        }

        public int Create(Mechanic mechanic)
        {
            return _mechanicDal.Create(mechanic);
        }

        public int Delete(int id)
        {
            return _mechanicDal.Delete(id);
        }

        public List<Mechanic> GetAll()
        {
            return _mechanicDal.GetAll();
        }

        public List<Mechanic> GetAll(Expression<Func<Mechanic, bool>> filter = null)
        {
            return _mechanicDal.GetAll(filter);
        }

        public Mechanic GetOne(int id)
        {
            return _mechanicDal.GetOne(id);
        }

        public int Update()
        {
            return _mechanicDal.Update(); ;
        }

        
        public Mechanic GetOne(string id)
        {
            return _mechanicDal.GetOne(id);
        }
        public int Delete(string id)
        {
            return _mechanicDal.Delete(id);
        }

        public Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName)
        {
            return _mechanicDal.CreateMechanicAsync(storeName);
        }
    }
}
