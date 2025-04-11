using OtoTamir.CORE.Identity;
using OtoTamir.DAL.Concrete.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class MechanicService
    {
        private readonly EfCoreMechanicDal _efCoreMechanicDal;

        public MechanicService(EfCoreMechanicDal efCoreMechanicDal)
        {
            _efCoreMechanicDal = efCoreMechanicDal;
        }

       
        public void CreateMechanic(string storeName, string email)
        {
            
            var token = Guid.NewGuid().ToString();

            
            var mechanic = new Mechanic
            {
                StoreName = storeName,
                Email = email,
                PasswordResetToken = token,
                ResetTokenExpiration = DateTime.UtcNow.AddHours(24)
            };

            
            _efCoreMechanicDal.Create(mechanic);

           
            string resetLink = $"https://seninsite.com/Account/ResetPassword?token={token}";
            EmailHelper.SendPasswordResetEmail(email, storeName, resetLink);
        }

       
        public bool IsValidResetToken(string token)
        {
            var mechanic = _efCoreMechanicDal.GetByResetToken(token);
            return mechanic != null && mechanic.ResetTokenExpiration > DateTime.UtcNow;
        }

        
        public bool ResetPassword(string token, string newPassword)
        {
            var mechanic = _efCoreMechanicDal.GetByResetToken(token);

            if (mechanic == null || mechanic.ResetTokenExpiration <= DateTime.UtcNow)
                return false; 
            mechanic.Password = newPassword;
            mechanic.PasswordResetToken = null; 
            mechanic.ResetTokenExpiration = null; 

            _efCoreMechanicDal.Update(); 
            return true;
        }
    }
}
