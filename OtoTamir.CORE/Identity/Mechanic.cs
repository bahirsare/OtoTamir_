using Microsoft.AspNetCore.Identity;
using OtoTamir.CORE.Entities;

namespace OtoTamir.CORE.Identity
{
    public class Mechanic : IdentityUser
    {


        public string StoreName { get; set; }
        public string? Skills { get; set; }
        public bool Status { get; set; }
        public string? Adress { get; set; }
        public string ImageUrl { get; set; }
        public bool IsProfileCompleted { get; set; }
        public Treasury Treasury { get; set; }
        public int TreasuryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<Client> Clients { get; set; }
        public List<Bank> Banks { get; set; }

        public Mechanic()
        {
            Clients = new List<Client>();
            Banks = new List<Bank>();
        }
    }
}
