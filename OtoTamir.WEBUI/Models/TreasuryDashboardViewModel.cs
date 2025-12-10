using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Models
{
    public class TreasuryDashboardViewModel
    {
        
        public Treasury Treasury { get; set; }

        
        public List<Bank> Banks { get; set; }
        public List<BankCard> BankCards { get; set; }
        public List<TreasuryTransaction> Transactions { get; set; }

        
      
        public TreasuryDashboardViewModel()
        {
            Banks = new List<Bank>();
            BankCards = new List<BankCard>();
            Transactions = new List<TreasuryTransaction>();
            
        }
    }
}