using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.MechanicDTOs
{
    public class ChangePasswordDTO
    {
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Yeni Şifre")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifre Tekrarı")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ReNewPassword { get; set; }
    }
}
