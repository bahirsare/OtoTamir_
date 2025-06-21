using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ClientDTOs
{
    public class BalanceLogDTO
    {
        public Client Client { get; set; }
        public List<BalanceLog> BalanceLogs { get; set; }
    }
}
