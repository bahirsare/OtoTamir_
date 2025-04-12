using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class MechanicService : IMechanicService
    {
        private readonly IMechanicDal _mechanicDal;

        public MechanicService(IMechanicDal mechanicDal)
        {
            _mechanicDal = mechanicDal;
        }

        public int Create(Mechanic mechanic)
        {
            var token = Guid.NewGuid().ToString();
            //string resetLink = $"https://seninsite.com/Account/ResetPassword?token={token}";
            // EmailHelper.SendPasswordResetEmail(email, storeName, resetLink);
            return _mechanicDal.Create(mechanic);
        }

        public int Delete(int id)
        {
            return _mechanicDal.Delete(id);
        }

        public List<Mechanic> GetAll()
        {
            return _mechanicDal.GetAll();
        }

        public List<Mechanic> GetAll(Expression<Func<Mechanic, bool>> filter = null)
        {
            return _mechanicDal.GetAll(filter);
        }

        public Mechanic? GetByResetToken(string token)
        {
            return _mechanicDal.GetByResetToken(token);
        }

        public Mechanic GetOne(int id)
        {
            return _mechanicDal.GetOne(id);
        }

        public bool IsValidResetToken(string token)
        {
            var mechanic = _mechanicDal.GetByResetToken(token);
            return mechanic != null && mechanic.ResetTokenExpiration > DateTime.UtcNow;
        }


        public bool ResetPassword(string token, string newPassword)
        {
            var mechanic = _mechanicDal.GetByResetToken(token);
            if (mechanic == null || mechanic.ResetTokenExpiration <= DateTime.UtcNow)
                return false;
            // mechanic.Password = newPassword;
            mechanic.PasswordResetToken = null;
            mechanic.ResetTokenExpiration = null;
            _mechanicDal.Update();
            return true;
        }

        public int Update()
        {
            return _mechanicDal.Update(); ;
        }


    }
}
