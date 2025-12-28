using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Services
{
    public static class EnumExtensions
    {
        public static string ToTurkish(this ServiceStatus status)
        {
            return status switch
            {
                ServiceStatus.InProgress => "Devam Ediyor",
                ServiceStatus.Completed => "Tamamlandı",
                ServiceStatus.Cancelled => "İptal Edildi",
                _ => "Bilinmiyor"
            };
        }

        public static string ToBadgeClass(this ServiceStatus status)
        {
            return status switch
            {
                ServiceStatus.InProgress => "badge-primary", // Mavi
                ServiceStatus.Completed => "badge-success",  // Yeşil
                ServiceStatus.Cancelled => "badge-danger",   // Kırmızı
                _ => "badge-secondary"
            };
        }
        public static string ToTurkish(this SymptomStatus status)
        {
            return status switch
            {
                SymptomStatus.Pending => "İnceleniyor / Bekliyor",
                SymptomStatus.Fixed => "Giderildi ✅",
                SymptomStatus.NotFixed => "Yapılmadı ❌",
                _ => "-"
            };
        }

        public static string ToBadgeClass(this SymptomStatus status)
        {
            return status switch
            {
                SymptomStatus.Pending => "badge-warning", 
                SymptomStatus.Fixed => "badge-success",   
                SymptomStatus.NotFixed => "badge-secondary", 
                _ => "badge-light"
            };
        }
        public static string ToIconClass(this SymptomStatus status)
        {
            return status switch
            {
                SymptomStatus.Pending => "bi bi-hourglass-split text-warning", 
                SymptomStatus.Fixed => "bi bi-check-circle-fill text-success",
                SymptomStatus.NotFixed => "bi bi-x-circle-fill text-danger",    
                _ => "bi bi-info-circle text-secondary"
            };
        }
    }
}
