using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class ServiceRecord:BaseEntity
    {
      
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public int VehicleId { get; set; }  // Araç
        public Vehicle Vehicle { get; set; }

        
    }

}
