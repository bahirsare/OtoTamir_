namespace OtoTamir.CORE.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
