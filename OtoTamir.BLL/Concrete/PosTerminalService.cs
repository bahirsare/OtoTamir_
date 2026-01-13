using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class PosTerminalService : IPosTerminalService
    {
        private readonly IPosTerminalDal _posTerminalDal;

        public PosTerminalService(IPosTerminalDal posTerminalDal)
        {
            _posTerminalDal = posTerminalDal;
        }

        public async Task<bool> AnyAsync(Expression<Func<PosTerminal, bool>> filter)
        {
            return await _posTerminalDal.AnyAsync(filter);
        }

        public async Task<int> CreateAsync(PosTerminal entity)
        {
            return await _posTerminalDal.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _posTerminalDal.DeleteAsync(id);
        }

        public async Task<List<PosTerminal>> GetAllAsync(string mechanicId, Expression<Func<PosTerminal, bool>> filter = null)
        {
            return await _posTerminalDal.GetAllAsync(mechanicId, filter);
        }

        public async Task<PosTerminal> GetOneAsync(int id, string mechanicId)
        {
            return await _posTerminalDal.GetOneAsync(id, mechanicId);
        }

        public async Task<int> UpdateAsync()
        {
            return await _posTerminalDal.UpdateAsync();
        }

        Task<PagedResult<PosTerminal>> IRepositoryService<PosTerminal>.GetPagedAsync(Expression<Func<PosTerminal, bool>> filter, Func<IQueryable<PosTerminal>, IOrderedQueryable<PosTerminal>> orderBy, int page, int pageSize, params Expression<Func<PosTerminal, object>>[] includes)
        {
            return _posTerminalDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }
    }
    }
