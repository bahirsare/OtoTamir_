using OtoTamir.CORE.Entities;

namespace OtoTamir.CORE.DTOs.ServiceRecordDTOs
{
    public class ServiceCompletionDTO
    {
        public int ServiceRecordId { get; set; }
        public string MechanicId { get; set; }

        public string AuthorName { get; set; }
        public PaymentSource PaymentMethod { get; set; }

        public int? BankId { get; set; }
        public int? PosTerminalId { get; set; }
    }
}
