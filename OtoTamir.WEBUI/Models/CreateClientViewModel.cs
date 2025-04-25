using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OtoTamir.WEBUI.Models
{
    public class CreateClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string PhoneNumber { get; set; }
        public string? Notes { get; set; }
        [ValidateNever]
        public string MechanicId{ get; set; }
    }
}
