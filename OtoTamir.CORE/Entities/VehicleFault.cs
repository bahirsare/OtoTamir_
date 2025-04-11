using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class VehicleFault
    {
        public int Id { get; set; }
        public string FaultName { get; set; }
        public string FaultDescription { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public int VehicleId { get; set; }
    }
}
