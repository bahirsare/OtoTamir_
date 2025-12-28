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
    public class EfCorePosTerminalDal : EfCoreGenericRepositoryDal<PosTerminal, DataContext>, IPosTerminalDal
    {

        private readonly DataContext _context;

        public EfCorePosTerminalDal(DataContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<int> CreateAsync(PosTerminal pos)
        {
            pos.CreatedDate = DateTime.Now;
            pos.ModifiedDate = DateTime.Now;
            pos.Name = pos.Name.ToUpper();
            return await base.CreateAsync(pos);
        }
        public async Task<PosTerminal> GetOneAsync(
        int id,
        string mechanicId)
        {
            var query = _context.PosTerminals.Include(p => p.Bank).Where(b => b.Id == id && b.Bank.Treasury.MechanicId == mechanicId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<PosTerminal>> GetAllAsync(
        string mechanicId,

        Expression<Func<PosTerminal, bool>> filter = null
        )
        {
            var query = _context.PosTerminals.Include(p => p.Bank)
                .Where(b => b.Bank.Treasury.MechanicId == mechanicId);



            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

    }
}
