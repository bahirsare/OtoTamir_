using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreBalanceLogDal : EfCoreGenericRepositoryDal<BalanceLog, DataContext>,IBalanceLogDal
    {
        private readonly DataContext _context;

        public EfCoreBalanceLogDal(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<BalanceLog> GetOneAsync(
        int id,
        string mechanicId)
        {
            var query = _context.BalanceLogs.Where(b => b.Id == id && b.Client.MechanicId == mechanicId);           

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<BalanceLog>> GetAllAsync(
        string mechanicId,
        int clientId,
        Expression<Func<BalanceLog, bool>> filter = null
        )
        {
            var query = _context.BalanceLogs
                .Where(b => b.ClientId==clientId && b.Client.MechanicId == mechanicId);

            

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

    }
}
