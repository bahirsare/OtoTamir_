using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtoTamir.CORE.Identity;

namespace OtoTamir.CORE.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Balance { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public bool Status { get; set; }
        public List<Mechanic> Mechanics { get; set; }

    }
}
