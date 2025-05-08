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
        public string UserName { get; set; }
        [DisplayName("İşletme Adı")]
        public string StoreName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Adres")]
        public string Adress { get; set; }

        [DisplayName("Yetenekler")]
        public string Skills { get; set; }

        [DisplayName("Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }

       
    }
}
