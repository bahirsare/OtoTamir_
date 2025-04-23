using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Models
{
    public class CreateVehicleViewModel
    {
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        [ValidateNever]
        public int ClientId { get; set; }
    }
}
