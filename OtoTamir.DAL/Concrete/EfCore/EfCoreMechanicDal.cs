using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Context;

namespace OtoTamir.DAL.Concrete.EfCore
{
    public class EfCoreMechanicDal : EfCoreGenericRepositoryDal<Mechanic, DataContext>, IMechanicDal
    {
        private readonly DataContext _context;
        public EfCoreMechanicDal(DataContext context) : base(context)
        {
            _context = context;

        }
        public Mechanic GetOne(string id)
        {
            return _context.Set<Mechanic>().Find(id);
        }
        public int Delete(string id)
        {
            var entity = _context.Set<Mechanic>().Find(id);

            if (entity != null)
            {
                _context.Set<Mechanic>().Remove(entity);
            }
            return _context.SaveChanges();
        }
        
    }
}
