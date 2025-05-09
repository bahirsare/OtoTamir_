using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OtoTamir.CORE.DTOs.ClientDTOs
{
    public class CreateClientDTO
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


    }
}
