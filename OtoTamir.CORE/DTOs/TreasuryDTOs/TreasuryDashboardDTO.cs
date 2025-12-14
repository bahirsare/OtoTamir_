using OtoTamir.CORE.Entities;

namespace OtoTamir.CORE.DTOs.TreasuryDTOs
{
    public class TreasuryDashboardDTO
    {
        
        public Treasury Treasury { get; set; }

        
        public List<Bank> Banks { get; set; }
        public List<BankCard> BankCards { get; set; }
        public List<TreasuryTransaction> Transactions { get; set; }


        public List<string> ChartLabels { get; set; } = new List<string>(); 
        public List<decimal> IncomeData { get; set; } = new List<decimal>();
        public List<decimal> ExpenseData { get; set; } = new List<decimal>();
        public TreasuryDashboardDTO()
        {
            Banks = new List<Bank>();
            BankCards = new List<BankCard>();
            Transactions = new List<TreasuryTransaction>();
            
        }
    }
}