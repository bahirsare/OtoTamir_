using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OtoTamir.CORE.DTOs.ClientDTOs
{
    public class EditClientDTO
    {
        public int Id { get; set; }
        [DisplayName("Müşteri Adı")]
        public string Name { get; set; }
        [DisplayName("Bakiye")]
        public decimal Balance { get; set; }
        [DisplayName("Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Notlar")]
        public string? Notes { get; set; }
        public List<Entities.Vehicle> Vehicles { get; set; }
        public EditClientDTO()
        {
            Vehicles = new List<Entities.Vehicle>();
        }
    }
}
