using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        public List<VehicleFault> Faults { get; set; }
    }
}
