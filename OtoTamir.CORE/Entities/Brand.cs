using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public bool Status { get; set; }
        public List<Vehicle> Cars { get; set; }
        public int CarId { get; set; }
        //public List<VehicleModel> CarModels { get; set; }
        //public int CarModelId { get; set; }


    }
}
