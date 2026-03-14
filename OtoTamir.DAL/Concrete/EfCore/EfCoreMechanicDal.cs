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
        private readonly ITreasuryDal _treasuryDal;

        public EfCoreMechanicDal(DataContext context, UserManager<Mechanic> userManager, RoleManager<IdentityRole> roleManager, ITreasuryDal treasuryDal)
            : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _treasuryDal = treasuryDal;
        }

        public async Task<Mechanic> GetOneAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }


        public async Task<List<Mechanic>> GetAllAsync(
            bool includeClient,
            bool includeVehicle,
            Func<IQueryable<Mechanic>, IOrderedQueryable<Mechanic>> orderBy = null,
            Expression<Func<Mechanic, bool>> filter = null
            )
        {
            var query = _context.Mechanics.AsQueryable();


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

            // 1. USTAYI SADECE HAFIZADA OLUŞTUR (Henüz veritabanına kaydetmiyoruz)
            var mechanic = new Mechanic
            {
                // Not: Identity burada mechanic.Id değerini arka planda otomatik olarak üretti bile!
                UserName = storeName.ToLower().Replace(" ", ""),
                StoreName = storeName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsProfileCompleted = false,
                ImageUrl = "avatar.png"
            };

            // 2. ÖNCE KASAYI OLUŞTUR VE VERİTABANINA KAYDET (Çünkü Usta Kasaya muhtaç)
            var newTreasury = new Treasury
            {
                CashBalance = 0,
                ReceivablesBalance = 0,
                CreatedDate = DateTime.Now,
                MechanicId = mechanic.Id // Hafızadaki ustanın ID'sini Kasaya verdik
            };

            // Kasayı kaydediyoruz. Artık Kasa veritabanında var ve bir ID'ye (newTreasury.Id) sahip!
            await _treasuryDal.CreateAsync(newTreasury);

            // 3. KASANIN ID'SİNİ USTAYA BAĞLA VE ŞİMDİ USTAYI KAYDET
            mechanic.TreasuryId = newTreasury.Id;

            var result = await _userManager.CreateAsync(mechanic, password);

            if (result.Succeeded)
            {
                return (true, password, new List<string>());
            }

            // 4. (GÜVENLİK) Eğer şifre kuralı vb. yüzünden Usta oluşturulamazsa, 
            // Boşuna açtığımız yetim Kasayı siliyoruz ki veritabanı kirlenmesin.
            await _treasuryDal.DeleteAsync(newTreasury.Id);

            var errors = result.Errors.Select(e => e.Description).ToList();
            return (false, password, errors);
        }
        public async Task<List<Mechanic>> GetDeletedMechanicsAsync()
        {
            // Kalkanı indir ve sadece silinen ustaları getir
            return await _context.Users
                .IgnoreQueryFilters()
                .Where(m => m.IsDeleted == true)
                .ToListAsync();
        }

        public async Task<bool> RestoreMechanicAsync(string id)
        {
            var mechanic = await _context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (mechanic != null)
            {
                mechanic.IsDeleted = false;
                _context.Users.Update(mechanic);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
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
