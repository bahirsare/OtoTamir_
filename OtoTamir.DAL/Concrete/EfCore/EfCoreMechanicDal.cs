using Microsoft.AspNetCore.Identity;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreMechanicDal : EfCoreGenericRepositoryDal<Mechanic, DataContext>, IMechanicDal
    {
        private readonly DataContext _context;
        private readonly UserManager<Mechanic> _userManager;
        public EfCoreMechanicDal(DataContext context, UserManager<Mechanic> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }
        public Mechanic GetOne(string id)
        {
            return _context.Set<Mechanic>().Find(id);
        }
        public int Delete(string id)
        {
            var entity = _context.Set<Mechanic>().Find(id);

            if (entity != null)
            {
                _context.Set<Mechanic>().Remove(entity);
            }
            return _context.SaveChanges();
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
                IsProfileCompleted = false,
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

    }
}
