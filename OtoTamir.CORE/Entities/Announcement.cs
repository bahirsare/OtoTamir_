using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OtoTamir.CORE.Entities
{
    public class Announcement : BaseEntity
    {
        public string Title { get; set; } // Duyuru Başlığı
        public string Message { get; set; } // Duyuru İçeriği
        public string Type { get; set; } // Tema Rengi: info, warning, danger, success
        public bool IsActive { get; set; } = false; // Ekranda görünsün mü?
    }
}
