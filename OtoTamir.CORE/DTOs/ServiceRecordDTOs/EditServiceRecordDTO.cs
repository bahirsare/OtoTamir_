using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ServiceRecordDTOs
{
    public class EditServiceRecordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ServiceStatus Status { get; set; }
        public Vehicle Vehicle { get; set; }
        public string AuthorName { get; set; }
        public decimal AdditionalPriceNote { get; set; }
        public List<Symptom> SymptomList { get; set; }
        public List<RepairComment> RepairComments { get; set; }
        public string ReturnUrl { get; set; }
    }
}

