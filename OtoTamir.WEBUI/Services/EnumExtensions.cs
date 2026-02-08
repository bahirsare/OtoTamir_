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

        // GÜNCELLENEN KISIM: Bootstrap 5 Uyumlu & Modern Soft Renkler
        public static string ToBadgeClass(this ServiceStatus status)
        {
            return status switch
            {
                // bg-opacity-10: Arka planı %10 şeffaf yapar
                // text-primary: Yazı rengini koyu mavi yapar
                ServiceStatus.InProgress => "bg-primary bg-opacity-10 text-primary border border-primary border-opacity-25",
                ServiceStatus.Completed => "bg-success bg-opacity-10 text-success border border-success border-opacity-25",
                ServiceStatus.Cancelled => "bg-danger bg-opacity-10 text-danger border border-danger border-opacity-25",
                _ => "bg-secondary bg-opacity-10 text-secondary border border-secondary border-opacity-25"
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

        // GÜNCELLENEN KISIM: Semptomlar İçin
        public static string ToBadgeClass(this SymptomStatus status)
        {
            return status switch
            {
                SymptomStatus.Pending => "bg-warning bg-opacity-10 text-warning border border-warning border-opacity-25",
                SymptomStatus.Fixed => "bg-success bg-opacity-10 text-success border border-success border-opacity-25",
                SymptomStatus.NotFixed => "bg-danger bg-opacity-10 text-danger border border-danger border-opacity-25",
                _ => "bg-light text-dark border"
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