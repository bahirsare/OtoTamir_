using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class Vehicle:BaseEntity
    {
        

        public string Plate { get; set; }
        public string Brand{ get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }

        public List<Symptom> Symptoms { get; set; }  // Araçtaki semptomlar
        public List<ServiceRecord> ServiceRecords { get; set; }  // Servis geçmişi
        public Vehicle()
        {
            Symptoms = new List<Symptom>();
            ServiceRecords = new List<ServiceRecord>();
                

        }
    }
}
