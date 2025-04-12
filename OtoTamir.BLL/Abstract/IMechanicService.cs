using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Abstract
{
    public interface IMechanicService : IGenericRepository<Mechanic>
    {
        public Mechanic? GetByResetToken(string token);
    }
}
