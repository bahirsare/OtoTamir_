using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Models
{
    public class ServiceRecordListViewModel
    {
        public List<ServiceRecord> Records { get; set; }
        public string CurrentStatus { get; set; }
        public string SearchTerm { get; set; }
    }

}
