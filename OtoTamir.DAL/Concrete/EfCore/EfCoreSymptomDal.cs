using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreSymptomDal : EfCoreGenericRepositoryDal<Symptom, DataContext>, ISymptomDal
    {
        private readonly DataContext _context;
        public EfCoreSymptomDal(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
