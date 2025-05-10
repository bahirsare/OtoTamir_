
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OtoTamir.WEBUI.Models
{
    public class ChangePasswordViewModel
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
