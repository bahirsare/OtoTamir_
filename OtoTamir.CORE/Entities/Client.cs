using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OtoTamir.CORE.Identity;

namespace OtoTamir.CORE.Entities
{
    public class Client : BaseEntity
    {

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }    
        public List<BalanceLog> BalanceLogs { get; set; }
        public string? Notes { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public  Mechanic Mechanic { get; set; }
        public  string MechanicId { get; set; }
        public Client()
        {
            Vehicles= new List<Vehicle>();
            BalanceLogs = new List<BalanceLog>();
        }


    }
    public class BalanceLog
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
        public decimal OldBalance { get; set; }
        public string Description { get; set; }
        public Client Client { get; set; }
        public int ClientId {  get; set; }

    }
}
