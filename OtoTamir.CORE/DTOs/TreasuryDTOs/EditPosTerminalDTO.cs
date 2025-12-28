using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class EditPosTerminalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BankId { get; set; }
        public decimal CommissionRate { get; set; }
        public int MaturityDays { get; set; }
    }
}
