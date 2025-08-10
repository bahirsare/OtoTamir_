using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } // Örn: Tahsilat, Masraf, Transfer, Banka Havalesi, Kredi Kartı Ödemesi

        public List<TreasuryTransaction> Transactions { get; set; }
    }
}
