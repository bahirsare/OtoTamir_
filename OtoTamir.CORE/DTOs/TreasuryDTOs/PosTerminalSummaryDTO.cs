using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class PosTerminalSummaryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public decimal CommissionRate { get; set; }
        public int MaturityDays { get; set; }
        public decimal BlockedBalance { get; set; }    
        public decimal LastMonthTurnover { get; set; }
        public int BlockedTransactionCount { get; set; } 
        public int LastMonthTransactionCount { get; set; }
    }
}
