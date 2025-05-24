using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class SparePart : BaseEntity
    {
        public string Name { get; set; }
        public string PartNumber { get; set; }           // Stok/Parça numarası
        public string Brand { get; set; }                // Marka
        public string Description { get; set; }          // Açıklama (isteğe bağlı)
        public string CompatibleVehicles { get; set; }   // Uyumlu araçlar (basit açıklama metni)

        public decimal SalePrice { get; set; }           // Satış fiyatı (sabit, güncellenebilir)

        // İlişki
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } // Alım detayları
    }

}
