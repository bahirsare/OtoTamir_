using System.ComponentModel.DataAnnotations;

namespace OtoTamir.WEBUI.Models
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string StoreName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Adress { get; set; }
        public string? Image { get; set; }
        public string Skills {  get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? rePassword { get; set; }

    }
}
