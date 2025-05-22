namespace OtoTamir.CORE.Entities
{
    public class Symptom : BaseEntity
    {
        public string Description { get; set; }
        public int ServiceRecordId { get; set; }
        public ServiceRecord ServiceRecord { get; set; }
    }

}
