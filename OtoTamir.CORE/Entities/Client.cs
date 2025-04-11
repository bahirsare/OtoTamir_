namespace OtoTamir.CORE.Entities
{
    public class Client : BaseEntity
    {

        public string Name { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public string Notes { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public List<ClientMechanic> ClientMechanics { get; set; }

    }
}
