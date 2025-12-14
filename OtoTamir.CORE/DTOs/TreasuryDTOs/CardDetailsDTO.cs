using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class CardDetailsDTO
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string BankName { get; set; }

        public string Last4Digit { get; set; }
        public decimal Limit { get; set; }
        public decimal CurrentDebt { get; set; }
        public decimal AvailableLimit => Limit - CurrentDebt;

        public DateTime CutOffDay { get; set; }
        public int DueDay { get; set; }
        public int DaysLeftToCutOff { get; set; }
        public DateTime NextPaymentDate { get; set; }

        public List<OtoTamir.CORE.Entities.TreasuryTransaction> Transactions { get; set; }
    }
}
