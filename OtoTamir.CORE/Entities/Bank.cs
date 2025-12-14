namespace OtoTamir.CORE.Entities
{
    public class Bank : BaseEntity
    {

        public string BankName { get; set; }
        public decimal Balance { get; set; }
        public string Iban { get; set; }
        public string OwnerName { get; set; }
        public Treasury Treasury { get; set; }
        public int TreasuryId { get; set; }
        public List<BankCard> Cards { get; set; }
        public Bank()
        {
            Cards = new List<BankCard>();
        }
    }
    public class BankCard : BaseEntity
    {

        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public string CardName { get; set; }
        public string Last4Digit { get; set; }
        public int BillingDay { get; set; }
        public int DueDay { get; set; }
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }
    }
}
