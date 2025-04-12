using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface IMechanicDal:IGenericRepository<Mechanic>
    {
        public Mechanic GetOne(string id);
        public int Delete(string id);

    }
}
