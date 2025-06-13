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
         public string VehiclePlate {  get; set; }
         public string VehicleName {  get; set; }
        public string SortColumn {  get; set; }
        public string SortDirection {  get; set; }
        public DateTime? StartDate {  get; set; }
        public DateTime? EndDate {  get; set; }

    }
}
