﻿using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System.Linq.Expressions;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreTreasuryDal : EfCoreGenericRepositoryDal<Treasury, DataContext>, ITreasuryDal

    {
        private readonly DataContext _context;

        public EfCoreTreasuryDal(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Treasury> GetOneAsync(
        int id,
        string mechanicId)
        {
            var query = _context.Treasuries.Where(t => t.Id == id && t.MechanicId == mechanicId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<Treasury>> GetAllAsync(
       string mechanicId,
        int treasuryId,
        Expression<Func<Treasury, bool>> filter = null
        )
        {
            var query = _context.Treasuries
                .Where(t => t.Id == treasuryId && t.MechanicId == mechanicId);



            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
        public async Task<decimal> GetTotalBalanceAsync(int treasuryId,string mechanicId)
        {
            var treasury= await GetOneAsync(treasuryId,mechanicId);
            return treasury.TotalBalance;
        }

    }
}
