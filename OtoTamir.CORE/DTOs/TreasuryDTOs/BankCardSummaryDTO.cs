using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class BankCardSummaryDTO
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string BankName { get; set; } // Banka adını buraya taşıyacağız
        public decimal Debt { get; set; }
        public decimal Limit { get; set; }

        // --- Hesaplanan Alanlar ---
        public decimal UsagePercent { get; set; }     // Doluluk Oranı (%)
        public decimal RemainingLimit { get; set; }   // Kalan Limit
        public string CriticalDateDisplay { get; set; } // Ekranda görünecek tarih (string formatında)
        public string DateLabel { get; set; }         // "Kesim" veya "Ödeme"
        public bool IsAlert { get; set; }
    }
}
