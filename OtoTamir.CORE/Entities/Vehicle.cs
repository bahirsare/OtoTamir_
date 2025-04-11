using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public int ProductYear { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        List<string> Complains { get; set; }
        List<string> FoundProblems { get; set; }
    }
}
