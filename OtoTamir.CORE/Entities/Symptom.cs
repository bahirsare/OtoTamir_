namespace OtoTamir.CORE.Entities
{
    public class Symptom:BaseEntity
    {
       
        public string Description { get; set; }  
        public int VehicleId { get; set; }  
        public Vehicle Vehicle { get; set; }
    }

}
