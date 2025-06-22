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
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public int SymptomId { get; set; }
        public decimal? AdditionalCost { get; set; }
        public int? AdditionalDays { get; set; }
        public string Status { get; set; }

        public List<SparePart>? NeededParts { get; set; }

    }
}
