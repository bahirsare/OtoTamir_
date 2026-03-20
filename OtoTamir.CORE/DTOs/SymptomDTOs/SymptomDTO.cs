using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.SymptomDTOs
{
    public class SymptomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal EstimatedCost { get; set; }
        private int? _estimatedDaysToFix;
        public int? EstimatedDaysToFix
        {
            
            get => _estimatedDaysToFix ?? 0;

            set => _estimatedDaysToFix = value ?? 0;
        }
        public bool IsCompleted { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
