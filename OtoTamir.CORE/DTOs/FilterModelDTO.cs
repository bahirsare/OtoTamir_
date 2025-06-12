using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.DTOs
{
    public class FilterModelDTO
    {
        public string ClientName { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string SortColumn { get; set; } = "CreatedDate";
        public string SortDirection { get; set; } = "desc";
    }
}
