using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class ServiceRecord:BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ServiceStatus Status { get; set; } = ServiceStatus.InProgress; 
        public int VehicleId { get; set; } 
        public Vehicle Vehicle { get; set; }
        public List<Symptom> SymptomList { get; set; }
        public string AuthorName { get; set; }

        public DateTime? CompletedDate {  get; set; }
        public ServiceRecord() {
            SymptomList=new List<Symptom>();
        }

        
    }
    public enum ServiceStatus
    {
        InProgress = 1,    
        Completed = 2,      
        Cancelled = 3       
    }
    

}
