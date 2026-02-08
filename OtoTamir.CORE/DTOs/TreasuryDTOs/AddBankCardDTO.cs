using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class AddBankCardDTO
    {
        [Required(ErrorMessage = "Kart adı zorunludur.")]
        [Display(Name = "Kart / POS Adı")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "Bir banka seçmelisiniz.")]
        [Display(Name = "Bağlı Banka")]
        public int BankId { get; set; }
        public string Last4Digit { get; set; }
        [Range(1, 31, ErrorMessage = "Lütfen 1-31 arası bir gün giriniz.")]
        [Display(Name = "Hesap Kesim Günü")]
        public int BillingDay{ get; set; }

        [Range(1, 31, ErrorMessage = "Lütfen 1-31 arası bir gün giriniz.")]
        [Display(Name = "Son Ödeme Günü")]
        public int DueDay { get; set; }
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }

        public List<Bank>? AvailableBanks { get; set; }
    }
}
