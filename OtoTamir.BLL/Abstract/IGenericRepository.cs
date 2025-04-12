using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Abstract
{
    public interface IGenericRepository<T > where T : class
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T GetOne(int id);
        int Create(T Entity);
        int Update();
        int Delete(int id);
    }
}

