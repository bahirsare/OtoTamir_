using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class ClientMechanic
    {
        public int ClientId { get; set; }  // Client
        public Client Client { get; set; }

        public int MechanicId { get; set; }  // Mechanic
        public Mechanic Mechanic { get; set; }
        public string Notes { get; set; }

    }
}
