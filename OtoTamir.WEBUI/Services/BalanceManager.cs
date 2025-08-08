using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.Services
{
    public class BalanceManager
    {
        
        public async Task<BalanceLogDTO> UpdateBalanceAsync(Client client, decimal Amount)
        {

            List<BalanceLog> log = new List<BalanceLog>();



            decimal oldBalance = client.Balance;
            decimal newBalance = oldBalance + Amount;
            log.Add(new BalanceLog
            {
                ClientId = client.Id,
                PaymentDate = DateTime.Now,
                Amount = Amount,
                OldBalance = oldBalance,
                NewBalance = newBalance
            });

            var result = new BalanceLogDTO
            {
                Client = client,
                BalanceLogs = log
            };



            client.Balance = newBalance;








            return result;



        }



    }
}
