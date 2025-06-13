
using System.ComponentModel.DataAnnotations;

namespace OtoTamir.WEBUI.Models
{
    public class LoginViewModel
    {

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6)]
        
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
