using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class BankDetailsDTO
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public decimal CurrentBalance { get; set; }
        public string OwnerName { get; set; }
        public decimal TotalIncomingLastMonth { get; set; } 
        public decimal TotalOutgoingLastMonth { get; set; } 

        public List<OtoTamir.CORE.Entities.TreasuryTransaction> Transactions { get; set; }
    }
}
