using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
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

        public int Delete(int id)
        {
            return _posTerminalDal.Delete(id);
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
    }
    }
