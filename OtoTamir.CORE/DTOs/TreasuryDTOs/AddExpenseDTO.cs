using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class AddExpenseDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        
        public PaymentSource PaymentSource { get; set; } 

        public int? BankId { get; set; }    
        public int? BankCardId { get; set; }  
        public int? CategoryId { get; set; } 
    }
}
