namespace OtoTamir.CORE.DTOs.Client
{
    public class CreateVehicleDTO
    {
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int ClientId { get; set; }
    }
}
