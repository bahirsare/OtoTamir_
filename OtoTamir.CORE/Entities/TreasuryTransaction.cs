using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class TreasuryTransaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string AuthorName { get; set; }

        public string Description { get; set; }

       
        public TransactionType TransactionType { get; set; }
        public PaymentSource PaymentSource { get; set; }
        public decimal Amount { get; set; }

        // Treasury ile ilişki
        public int TreasuryId { get; set; }
        public Treasury Treasury { get; set; }

        // Banka ile ilişki (zorunlu değil)
        public int? BankId { get; set; }
        public Bank Bank { get; set; }

        // Kart ile ilişki (zorunlu değil)
        public int? BankCardId { get; set; }
        public BankCard BankCard { get; set; }

        // İşlem müşteriyle ilgiliyse
        public int? ClientId { get; set; }
        public Client Client { get; set; }

       
        
        
    }

    public enum PaymentSource
    {
        Cash,
        Bank,
        ClientBalance
    }

    public enum TransactionType
    {
        Incoming,  
        Outgoing   
    }
}
