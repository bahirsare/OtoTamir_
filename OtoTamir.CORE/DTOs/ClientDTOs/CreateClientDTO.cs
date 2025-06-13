using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OtoTamir.CORE.DTOs.ClientDTOs
{
    public class CreateClientDTO
    {
        public int Id { get; set; }

        [DisplayName("Müşteri Adı")]
        [Required(ErrorMessage = "Ad Soyad boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Ad Soyad en fazla 50 karakter olabilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bakiye boş bırakılamaz.")]
        [Range(0, double.MaxValue, ErrorMessage = "Bakiye negatif olamaz.")]
        [DisplayName("Bakiye")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Telefon numarası boş bırakılamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        
        [RegularExpression(@"^05\d{9}$", ErrorMessage = "Telefon numarası 05xxxxxxxxx formatında olmalıdır.")]
        [DisplayName("Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Notlar")]
        [StringLength(500, ErrorMessage = "Notlar en fazla 500 karakter olabilir.")]
        public string? Notes { get; set; }

        public string ReturnUrl { get; set; }


    }
}
