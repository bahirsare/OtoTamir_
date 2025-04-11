using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OtoTamir.CORE.Entities;

namespace OtoTamir.CORE.Identity
{
    public class Mechanic : IdentityUser
    {
        public string StoreName { get; set; }
        public List<Client> Clients { get; set; }


    }
}
