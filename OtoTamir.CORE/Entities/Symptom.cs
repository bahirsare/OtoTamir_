using System;

namespace OtoTamir.CORE.Entities
{
    public class Symptom : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal EstimatedCost {  get; set; }
        public string PossibleSolution {  get; set; }

        public int EstimatedDaysToFix { get; set; }
        public bool Status { get; set; }
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

}
