using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Models
{
    public class EditServiceRecordViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public List<Symptom> SymptomList { get; set; }
        public List<RepairComment> RepairComments { get; set; }
    }
}
