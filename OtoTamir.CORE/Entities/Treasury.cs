using OtoTamir.CORE.Identity;

namespace OtoTamir.CORE.Entities
{
   
    public class Treasury:BaseEntity
    {
        
        public string MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public decimal CashBalance { get; set; }
        public decimal BankBalance { get; set; }
        public decimal ReceivablesBalance { get; set; }
        public decimal TotalBalance => CashBalance + BankAccounts.Sum(b => b.Balance);
        public decimal NetWorth => TotalBalance + ReceivablesBalance;
        public List<Bank> BankAccounts { get; set; }
        public List<TreasuryTransaction> Transactions { get; set; }
        public Treasury()
        {
            BankAccounts = new List<Bank>();
            Transactions = new List<TreasuryTransaction>();
        }
    }
}
