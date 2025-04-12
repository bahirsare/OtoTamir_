using Microsoft.AspNetCore.Identity;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

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

        public async Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName)
        {
            var password = GenerateRandomPassword();

            var mechanic = new Mechanic
            {
                UserName = storeName,
                StoreName = storeName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,

            };

            var result = await _userManager.CreateAsync(mechanic, password);

            if (result.Succeeded)
            {

                return (true, password, new List<string>());
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return (false, password, errors);
        }

        private string GenerateRandomPassword(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public Mechanic GetOne(string id)
        {
            return _mechanicDal.GetOne(id);
        }
        public int Delete(string id)
        {
            return _mechanicDal.Delete(id);
        }
    }
}
