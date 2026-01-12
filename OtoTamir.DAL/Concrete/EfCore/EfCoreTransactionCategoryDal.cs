using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Concrete.EfCore
{
    // Senin örneğindeki gibi Generic Repository'den türüyoruz
    public class EfCoreTransactionCategoryDal : EfCoreGenericRepositoryDal<TransactionCategory, DbContext>, ITransactionCategoryDal
    {
        private readonly DataContext _context;

        public EfCoreTransactionCategoryDal(DataContext context) : base(context)
        {
            _context = context;
        }

        // Belirli bir tamircinin tüm kategorilerini getir
        public async Task<List<TransactionCategory>> GetAllAsync(string mechanicId)
        {
            var query = _context.TransactionCategories
                .Where(c => c.MechanicId == mechanicId);

            return await query.ToListAsync();
        }

        // ID'ye göre getir ama mechanicId kontrolü de yap (Başkası göremesin)
        public async Task<TransactionCategory> GetOneAsync(int id, string mechanicId)
        {
            var query = _context.TransactionCategories
                .Where(c => c.Id == id && c.MechanicId == mechanicId);

            return await query.FirstOrDefaultAsync();
        }

        // Aynı isimde kategori var mı kontrolü
        public async Task<bool> IsNameExistsAsync(string name, string mechanicId)
        {
            return await _context.TransactionCategories
                .AnyAsync(c => c.MechanicId == mechanicId && c.Name.ToLower() == name.ToLower());
        }
    }
}