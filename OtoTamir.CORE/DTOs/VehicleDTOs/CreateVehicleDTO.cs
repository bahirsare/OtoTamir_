using System.ComponentModel.DataAnnotations;

namespace OtoTamir.CORE.DTOs.VehicleDTOs
{
    public class CreateVehicleDTO
    {
        private string _plate;
        [Required(ErrorMessage = "Plaka zorunludur.")]
        public string Plate { get => _plate; set => _plate = value?.ToUpperInvariant(); }

        [Required(ErrorMessage = "Marka zorunludur.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model zorunludur.")]
        public string Model { get; set; }

        [Range(1900, 2100, ErrorMessage = "Yıl geçerli bir değer olmalıdır.")]
        public int Year { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}