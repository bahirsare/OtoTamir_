using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OtoTamir.CORE.Entities;

namespace OtoTamir.CORE.Identity
{
    public class Mechanic : IdentityUser
    {
        public string StoreName { get; set; }
        public string? Skills { get; set; }
        public bool Status { get; set; }
        public string? Adress { get; set; }
        public Image Image {  get; set; }
        public int ImageId {  get; set; }
        public bool IsProfileCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<Client> Clients { get; set; }

        public Mechanic()
        {
            
        }


    }
}
