using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.SymptomDTOs
{
    public class ServiceWorkflowLogDTO
    { 
        public string? Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public int SymptomId { get; set; }
        public decimal? AdditionalCost { get; set; }
        public int? AdditionalDays { get; set; }
        public SymptomStatus Status { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsCompleted { get; set; } 
        public PaymentSource PaymentMethod { get; set; } = PaymentSource.Cash; 
        public int? BankId { get; set; }
        public int? PosTerminalId { get; set; } 
        
        

        public List<SparePart>? NeededParts { get; set; }

    }
}
