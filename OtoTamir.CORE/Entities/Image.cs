using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string MechanicID { get; set; }
        public Mechanic Mechanic { get; set; }
    }
}
