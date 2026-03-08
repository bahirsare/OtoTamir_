namespace OtoTamir.CORE.Exceptions
{
    /// <summary>
    /// Uygulama genelinde beklenen (kullanıcıya gösterilebilir) iş kuralı hataları için temel sınıf.
    /// catch (OtoTamirException ex) ile controller'da kolayca yakalanır.
    /// </summary>
    public class OtoTamirException : Exception
    {
        public OtoTamirException(string message) : base(message) { }
        public OtoTamirException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Kayıt bulunamadığında fırlatılır (404 benzeri).
    /// Örn: Kasa bulunamadı, Müşteri bulunamadı.
    /// </summary>
    public class NotFoundException : OtoTamirException
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} bulunamadı. (ID: {key})") { }

        public NotFoundException(string message) : base(message) { }
    }

    /// <summary>
    /// İş kuralı ihlali. Örn: Yetersiz bakiye, fazla ödeme girişimi.
    /// </summary>
    public class BusinessRuleException : OtoTamirException
    {
        public BusinessRuleException(string message) : base(message) { }
    }

    /// <summary>
    /// Geçersiz işlem durumu. Örn: Zaten tamamlanmış servise işlem yapma.
    /// </summary>
    public class InvalidOperationException : OtoTamirException
    {
        public InvalidOperationException(string message) : base(message) { }
    }

    /// <summary>
    /// Yetki hatası. Başka bir tamirciye ait kaydı değiştirme girişimi.
    /// </summary>
    public class UnauthorizedAccessException : OtoTamirException
    {
        public UnauthorizedAccessException(string message = "Bu işlem için yetkiniz bulunmamaktadır.")
            : base(message) { }
    }
}
