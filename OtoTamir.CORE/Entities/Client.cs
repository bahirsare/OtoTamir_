using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OtoTamir.CORE.Identity;

namespace OtoTamir.CORE.Entities
{
    public class Client : BaseEntity
    {

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }    
        
        public string? Notes { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public  Mechanic Mechanic { get; set; }
        public  string MechanicId { get; set; }
        public Client()
        {
            Vehicles= new List<Vehicle>();
            
        }


    }

}
