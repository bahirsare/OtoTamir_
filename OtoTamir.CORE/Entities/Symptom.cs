using System;

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
        Pending = 1,      // Bekliyor (Henüz bakılmadı)
        Fixed = 2,        // Giderildi (Tamir edildi)
        NotFixed = 3      // Giderilmedi (Müşteri istemedi / Parça yoktu vs.)
    }
}
