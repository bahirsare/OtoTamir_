using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class PurchaseDetail : BaseEntity
    {
        public int SparePartId { get; set; }
        public SparePart SparePart { get; set; }

        public string SupplierName { get; set; }         // Tedarikçi
        public decimal Price { get; set; }               // Alış fiyatı
        public DateTime PurchaseDate { get; set; }       // Alım tarihi
        public int Quantity { get; set; }                // Kaç adet alındı
    }
}
