using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ClientDTOs
{
    public class ClientDetailsDTO
    {
        public string Name {  get; set; }

        public decimal Balance { get; set; }
        public string PhoneNumber { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate{ get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public List<Vehicle> Vehicles { get; set; }

              
    }
}
