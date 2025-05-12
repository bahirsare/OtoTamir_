using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreMechanicDal : EfCoreGenericRepositoryDal<Mechanic, DataContext>, IMechanicDal
    {
        private readonly DataContext _context;
        private readonly UserManager<Mechanic> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public EfCoreMechanicDal(DataContext context, UserManager<Mechanic> userManager, RoleManager<IdentityRole> roleManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Mechanic GetOne(string id)
        {
            return _context.Mechanics.FirstOrDefault(m => m.Id == id);
            //return _context.Mechanics.Include(m => m.Image).FirstOrDefault(m => m.Id == id);
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
                UserName = storeName.ToLower().Replace(" ",""),
                StoreName = storeName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsProfileCompleted = false,
                ImageUrl="avatar.png"
                //Image = new Image
                //{
                //    Url = "avatar.png",
                    
                //}

            };

            var result = await _userManager.CreateAsync(mechanic, password);
            //if (!_roleManager.Roles.Any(i => i.Name == "admin"))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
            //}

            //await _userManager.AddToRoleAsync(mechanic, "admin");


            if (result.Succeeded)
            {

                return (true, password, new List<string>());
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return (false, password, errors);
        }
        
        

        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        
    }
}
