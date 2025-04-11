using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface IClientDal
    {
        List<Client> GetAll();
        List<Client> GetAll(Expression<Func<Client, bool>> filter = null);
        Client GetOne(int id);
        int Create(Client entity);
        int Update();
        int Delete(int id);
    }
}
