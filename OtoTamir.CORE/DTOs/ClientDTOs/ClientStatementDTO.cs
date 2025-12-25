using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ClientDTOs
{
    public class ClientStatementDTO
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
       

      
        public decimal TotalDebt { get; set; } 
        public decimal TotalPaid { get; set; } 
        public decimal Balance { get; set; }

        public List<StatementItem> Transactions { get; set; } = new List<StatementItem>();
    }

    public class StatementItem
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } 
        public string Type { get; set; }        
        public decimal Amount { get; set; }
        public int ReferenceId { get; set; }    
    }
}
