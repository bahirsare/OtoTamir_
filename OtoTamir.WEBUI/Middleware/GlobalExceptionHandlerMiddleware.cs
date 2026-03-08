using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OtoTamir.CORE.Exceptions;
using System.Net;

namespace OtoTamir.WEBUI.Middleware
{
    /// <summary>
    /// Uygulama genelindeki tüm exception'ları yakalar.
    /// OtoTamirException türündeki hatalar kullanıcıya TempData ile gösterilir.
    /// Beklenmedik hatalar loglara yazılır, kullanıcıya genel mesaj gösterilir.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OtoTamirException ex)
            {
                // Beklenen iş hatası - kullanıcıya göster, stack trace'e gerek yok
                _logger.LogWarning("İş kuralı hatası: {Message} | Path: {Path}", ex.Message, context.Request.Path);
                await HandleExceptionAsync(context, ex.Message, isBusinessError: true);
            }
            catch (Exception ex)
            {
                // Beklenmedik hata - logla, kullanıcıya genel mesaj göster
                _logger.LogError(ex, "Beklenmedik hata: {Path}", context.Request.Path);
                await HandleExceptionAsync(context, "Beklenmedik bir hata oluştu. Lütfen tekrar deneyin.", isBusinessError: false);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, string message, bool isBusinessError)
        {
            // AJAX / API isteği ise JSON döndür
            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
                context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = isBusinessError
                    ? (int)HttpStatusCode.BadRequest
                    : (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { success = false, message });
                return;
            }

            // Normal MVC isteği ise TempData'ya yaz ve geri yönlendir
            // TempData cookie tabanlı olduğu için middleware'den de erişilebilir
            context.Response.Cookies.Append(
                "TempData_FailMessage",
                message,
                new CookieOptions { MaxAge = TimeSpan.FromSeconds(30), HttpOnly = false }
            );

            // Referer varsa oraya, yoksa ana sayfaya dön
            var referer = context.Request.Headers["Referer"].ToString();
            var redirectUrl = !string.IsNullOrEmpty(referer) ? referer : "/Home/Index";

            context.Response.Redirect(redirectUrl);
        }
    }

    // Program.cs'de kolay kullanım için extension method
    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
