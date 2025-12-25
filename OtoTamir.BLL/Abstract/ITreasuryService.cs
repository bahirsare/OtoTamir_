using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Abstract
{
    public interface ITreasuryService:IRepositoryService<Treasury>
    {
        Task<decimal> GetTotalBalanceAsync(int treasuryId, string mechanicId);
        Task<List<Treasury>> GetAllAsync(
        string mechanicId,
        int treasuryId,
        Expression<Func<Treasury, bool>> filter = null
        );
        Task<Treasury> GetOneAsync(
        int id,
        string mechanicId);
        
    }
}
