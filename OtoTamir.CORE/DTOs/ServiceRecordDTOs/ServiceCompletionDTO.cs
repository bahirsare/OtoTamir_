using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ServiceRecordDTOs
{
    public class ServiceCompletionDTO
    {
        public int ServiceRecordId { get; set; }
        public string MechanicId { get; set; }

       
        public PaymentSource PaymentMethod { get; set; }

       
        public int? BankId { get; set; }
    }
}
