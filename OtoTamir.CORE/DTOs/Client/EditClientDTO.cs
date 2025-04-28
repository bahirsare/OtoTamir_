using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OtoTamir.CORE.DTOs.Client
{
    public class EditClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string PhoneNumber { get; set; }
        public string? Notes { get; set; }
        public List<Entities.Vehicle> Vehicles { get; set; }
        public EditClientDTO()
        {
            Vehicles = new List<Entities.Vehicle>();
        }
    }
}
