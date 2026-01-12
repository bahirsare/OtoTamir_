using Serilog;
namespace OtoTamir.WEBUI.Services
{
    public class ImageOperations
    {
        private static string GenerateUniqueFileName(string fileExtension = ".png")
        {
            var uniqueName = $"{Guid.NewGuid()}{fileExtension}";

            return uniqueName;
        }

        public static async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                string newFileName = GenerateUniqueFileName();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                Log.Information("Resim yüklendi! Dosya Adı: {FileName}", file.FileName);
              
                return newFileName;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Dosya yüklenirken hata oluştu! Dosya Adı: {FileName}", file.FileName);
                throw;
            }
        }

        public static void DeleteImage(string fileName)
        {
            if (fileName == "avatar.png")
            {
                return;
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","images", fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
