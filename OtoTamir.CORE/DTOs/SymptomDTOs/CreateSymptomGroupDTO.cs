using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.SymptomDTOs
{
    public class CreateSymptomGroupDTO
    {
        public int VehicleId { get; set; }
        public string AuthorName { get; set; }

        public List<SymptomDTO> Symptoms { get; set; }
    }

   

}
