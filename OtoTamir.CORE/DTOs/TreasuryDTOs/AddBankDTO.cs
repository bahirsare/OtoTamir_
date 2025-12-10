using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class AddBankDTO
    {
        [Required(ErrorMessage = "Banka adı zorunludur.")]
        [Display(Name = "Banka Adı")]
        public string BankName { get; set; }

        [Display(Name = "IBAN / Hesap No")]
        [MinLength(24)]
        public string Iban { get; set; }
        public string OwnerName { get; set; }
        public int TreasuryId { get; set; }

        [Display(Name = "Başlangıç Bakiyesi")]
        public decimal InitialBalance { get; set; } = 0;
    }
}
