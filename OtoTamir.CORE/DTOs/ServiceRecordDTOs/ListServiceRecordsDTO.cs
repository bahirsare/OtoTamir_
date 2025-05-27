using OtoTamir.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs.ServiceRecordDTOs
{
    public class ListServiceRecordsDTO
    {
        public List<ServiceRecord> Records { get; set; }
        public string ClientName {  get; set; }
         public string CurrentStatus {  get; set; }
    }
}
