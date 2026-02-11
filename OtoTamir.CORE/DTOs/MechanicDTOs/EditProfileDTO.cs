using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.MechanicDTOs
{
    public class EditProfileDTO
    {
        [DisplayName("Kullanıcı Adı")]
        [MinLength(5)]
        [MaxLength(12)]
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string UserName { get; set; }
        [DisplayName("İşletme Adı")]
        [MinLength(5)]
        [MaxLength(25)]
        [Required(ErrorMessage = "İşletme Adı boş bırakılamaz.")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Adres")]
        public string Adress { get; set; }

        [DisplayName("Yetenekler")]
        public string? Skills { get; set; }

        [Required(ErrorMessage = "Telefon numarası boş bırakılamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [RegularExpression(@"^05\d{9}$", ErrorMessage = "Telefon numarası 05xxxxxxxxx formatında olmalıdır.")]
        [DisplayName("Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Vergi Numarası")]
        [StringLength(11, ErrorMessage = "Vergi numarası en fazla 11 karakter olabilir.")]
        public string? TaxNumber { get; set; }

        [DisplayName("Vergi Dairesi")]
        public string? TaxOffice { get; set; }


    }
}
