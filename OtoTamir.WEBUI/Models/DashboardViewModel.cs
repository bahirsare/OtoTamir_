using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Models
{
    public class DashboardViewModel
    {
        public int ActiveVehicleCount { get; set; }    
        public int TodayJobCount{ get; set; }     
        public int CompletedJobCount { get; set; }         
        public int CancelledJobCount { get; set; }   
        public decimal TodayIncome { get; set; }     
        public decimal MonthlyIncome { get; set; }   
        public decimal YearlyIncome { get; set; }   
        public decimal CashBalance { get; set; }   
        public decimal BankBalance { get; set; }   

                                                     
        public decimal TotalDebt { get; set; }       


        public List<ServiceRecord> RecentServices { get; set; }
    }
}
