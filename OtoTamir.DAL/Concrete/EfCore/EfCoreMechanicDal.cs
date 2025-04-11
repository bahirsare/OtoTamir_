using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreMechanicDal:EfCoreGenericRepositoryDal<Mechanic, DataContext>
    {
        private readonly DataContext _context;
        public EfCoreMechanicDal(DataContext context) : base(context)
        {
            _context = context;

        }
        public Mechanic? GetByResetToken(string token)
        {
            return _context.Mechanics.FirstOrDefault(m => m.PasswordResetToken == token);
        }
    }
    
}
