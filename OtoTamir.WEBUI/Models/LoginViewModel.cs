using System.ComponentModel.DataAnnotations;

namespace OtoTamir.WEBUI.Models
{
    public class LoginViewModel
    {
        
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
