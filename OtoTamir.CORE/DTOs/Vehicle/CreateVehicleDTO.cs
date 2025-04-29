using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.Vehicle
{
    public class CreateVehicleDTO
    {
        [Required(ErrorMessage = "Plaka zorunludur.")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "Marka zorunludur.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model zorunludur.")]
        public string Model { get; set; }

        [Range(1900, 2100, ErrorMessage = "Yıl geçerli bir değer olmalıdır.")]
        public int Year { get; set; }

        [Required]
        public int ClientId { get; set; } // Bu değer modal açıldığında dışarıdan verilecek
    }
}