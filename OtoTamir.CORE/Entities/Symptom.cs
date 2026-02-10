using System;
using System.ComponentModel.DataAnnotations;

namespace OtoTamir.CORE.Entities
{
    public class Symptom : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal EstimatedCost {  get; set; }
        

        public int EstimatedDaysToFix { get; set; }
        public SymptomStatus Status { get; set; } = SymptomStatus.Pending;
        public List<RepairComment> ServiceWorkflowLogs { get; set; }

        public List<SparePart> PossibleSpareParts { get; set; }
        public int ServiceRecordId { get; set; }
        public ServiceRecord ServiceRecord { get; set; }
        public Symptom()
        {
            ServiceWorkflowLogs= new List<RepairComment>();
            PossibleSpareParts = new List<SparePart>();
        }
    }
    public enum SymptomStatus
    {
        [Display(Name = "Bekliyor / İşlemde")]
        Pending = 1,

        [Display(Name = "Giderildi (Tamamlandı)")]
        Fixed = 2,

        [Display(Name = "Giderilemedi / İptal")]
        NotFixed = 3
    }
}
