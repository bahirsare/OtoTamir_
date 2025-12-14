using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.SymptomDTOs
{
    public class CreateSymptomGroupDTO
    {
    

        public int VehicleId { get; set; }
        [Required(ErrorMessage = "Yazar Adı boş bırakılamaz.")]
        [StringLength(15, ErrorMessage = "Yazar adı en fazla 15 karakter olabilir.")]
        [DisplayName("Yazar Adı")]
        public string AuthorName { get; set; }

        public List<SymptomDTO> Symptoms { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }
        public int? ReturnId { get; set; }
        public bool IsCompleted { get; set; }
        public int? PaymentMethod { get; set; }
        public int? BankId { get; set; }


        public CreateSymptomGroupDTO()
        {
           Symptoms= new List<SymptomDTO>();
        }
    }

   

}
