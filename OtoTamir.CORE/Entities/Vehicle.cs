using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class Vehicle:BaseEntity
    {

        public string Name { get; set; }
        public string Plate { get; set; }
        public string Brand{ get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        
        public Client Client { get; set; }
        public int ClientId { get; set; }

        //public List<Symptom> Symptoms { get; set; }  
        public List<ServiceRecord> ServiceRecords { get; set; }  
        public Vehicle()
        {
            //Symptoms = new List<Symptom>();
            ServiceRecords = new List<ServiceRecord>();
                

        }
    }
}
