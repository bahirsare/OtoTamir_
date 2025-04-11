using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;

namespace OtoTamir.CORE.Identity
{
    public class Mechanic : IdentityUser
    {
        public string StoreName { get; set; }
        public string Skills { get; set; }
        public string? PasswordResetToken { get; set; }  
        public DateTime? ResetTokenExpiration { get; set; }
        public List<ClientMechanic> ClientMechanics { get; set; }


    }
}
