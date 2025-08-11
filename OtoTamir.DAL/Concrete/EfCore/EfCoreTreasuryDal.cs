using OtoTamir.CORE.Entities;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreTreasuryDal : EfCoreGenericRepositoryDal<Treasury, DataContext>, ITreasuryDal

    {
        private readonly DataContext _context;

        public EfCoreTreasuryDal(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
