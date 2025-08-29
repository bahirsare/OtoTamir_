using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.SymptomDTOs
{
    public class SymptomDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal EstimatedCost { get; set; }
        public int EstimatedDaysToFix { get; set; }
        
        public bool IsCompleted { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
