using OtoTamir.CORE.DTOs.SymptomDTOs;
using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ServiceRecordDTOs
{
    public class ServiceRecordDetailDTO
    {
       
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ServiceStatus Status { get; set; }
        public string MechanicNote { get; set; } // Usta notları
        public int Odometer { get; set; } // O anki KM

       
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public decimal ClientBalance { get; set; }

        
        public int VehicleId { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
     

        
        public List<SymptomDTO> Symptoms { get; set; } = new List<SymptomDTO>();
        public List<ServiceItemDTO> Items { get; set; } = new List<ServiceItemDTO>();

        public decimal SubTotal { get; set; }  
       
        
    }

   
    

    public class ServiceItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        
        public decimal Amount { get; set; }
       
        public string Type { get; set; } 
        public bool IsPaid { get; set; }
    }
}