using System.ComponentModel.DataAnnotations;

namespace OtoTamir.WEBUI.Services.MailHelper
{
    public class Mail
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Subject { get; set; }

        [StringLength(500)]
        public string Message { get; set; }
    }
}
