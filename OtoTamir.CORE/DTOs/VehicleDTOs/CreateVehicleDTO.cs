using System.ComponentModel.DataAnnotations;

namespace OtoTamir.CORE.DTOs.VehicleDTOs
{
    public class CreateVehicleDTO
    {
        private string _plate;
        private string _brand;
        private string _model;
        [Required(ErrorMessage = "Plaka zorunludur.")]
        public string Plate { get => _plate; set => _plate = value?.ToUpper().Replace(" ",""); }

        [Required(ErrorMessage = "Marka zorunludur.")]
        public string Brand { get => _brand; set => _brand = value?.ToUpper(); }

        [Required(ErrorMessage = "Model zorunludur.")]
        public string Model { get => _model; set => _model = value?.ToUpper(); }

        [Range(1900, 2100, ErrorMessage = "Yıl geçerli bir değer olmalıdır.")]
        public int Year { get; set; }

        [Required]
        public int ClientId { get; set; }

        public string ReturnUrl {  get; set; }
    }
}