using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ClientDTOs
{
    public class CreateClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string PhoneNumber { get; set; }
        public string? Notes { get; set; }
       
       
    }
}
