using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Models
{
    public class DashboardViewModel
    {
        public int ActiveVehicleCount { get; set; }      // İçerideki Araç Sayısı
        public int PendingJobCount { get; set; }         // Bekleyen (Sıra Bekleyen) İşler
        public decimal TodayIncome { get; set; }         // Bugünün Cirosu (Nakit + Kart)
        public decimal MonthlyIncome { get; set; }       // Bu Ayın Cirosu
        public decimal TotalDebt { get; set; }           // Toplam Alacak (Veresiye)

        // Tablolar İçin Listeler
        public List<ServiceRecord> RecentServices { get; set; }
    }
}
