using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface IPosTerminalDal : IRepositoryService<PosTerminal>
    {

        Task<PosTerminal> GetOneAsync(
            int id,
            string mechanicId);
        Task<List<PosTerminal>> GetAllAsync(
            string mechanicId,
            Expression<Func<PosTerminal, bool>> filter = null
        );
    
    }
}
