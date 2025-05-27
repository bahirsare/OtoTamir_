using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreMechanicDal : EfCoreGenericRepositoryDal<Mechanic, DataContext>, IMechanicDal
    {
        private readonly DataContext _context;
        private readonly UserManager<Mechanic> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EfCoreMechanicDal(DataContext context, UserManager<Mechanic> userManager, RoleManager<IdentityRole> roleManager)
            : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Mechanic> GetOneAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public  List<Mechanic> GetAll(Expression<Func<Mechanic, bool>> filter = null)
        {
            //return filter == null
            //    ? context.Set<T>().ToList()
            //    : context.Set<T>().Where(filter).ToList();

            var entities = _context.Mechanics.AsQueryable();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }
            return entities.ToList();
        }

        public async Task<List<Mechanic>> GetAllAsync(
            Expression<Func<Mechanic, bool>> filter = null,
            bool includeClient = true,
            bool includeVehicle = true,
            Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy = null)
        {
            var query = _context.Mechanics.AsQueryable();

            // Filtre uygula
            if (filter != null)
            {
                query = query.Where(filter);
            }

            
            if (includeClient)
            {
                var clientInclude = query.Include(m => m.Clients);

                query = includeVehicle
                    ? clientInclude.ThenInclude(v => v.Vehicles)
                    : clientInclude;
            }

            // Sıralama yap
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<int> DeleteAsync(string id)
        {
            var mechanic = await _userManager.FindByIdAsync(id);
            if (mechanic == null)
                return 0;

            var result = await _userManager.DeleteAsync(mechanic);
            return result.Succeeded ? 1 : 0;
        }

        public async Task<(bool Success, string Password, List<string> Errors)> CreateMechanicAsync(string storeName)
        {
            var password = GenerateRandomPassword();

            var mechanic = new Mechanic
            {
                UserName = storeName.ToLower().Replace(" ", ""),
                StoreName = storeName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsProfileCompleted = false,
                ImageUrl = "avatar.png"
            };

            var result = await _userManager.CreateAsync(mechanic, password);

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
