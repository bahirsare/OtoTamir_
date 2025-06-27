using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.VehicleDTOs
{
    public class EditVehicleDTO
    {

        private string _plate;
        private string _brand;
        private string _model;
        public int Id {  get; set; }
        
        [Required(ErrorMessage = "Plaka zorunludur.")]
        public string Plate { get => _plate; set => _plate = value?.ToUpper().Replace(" ", ""); }

        [Required(ErrorMessage = "Marka zorunludur.")]
        public string Brand { get => _brand; set => _brand = value?.ToUpper(); }

        [Required(ErrorMessage = "Model zorunludur.")]
        public string Model { get => _model; set => _model = value?.ToUpper(); }

        [Range(1900, 2100, ErrorMessage = "Yıl geçerli bir değer olmalıdır.")]
        public int Year { get; set; }



    }
}
