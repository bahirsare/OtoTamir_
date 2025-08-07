using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;

namespace OtoTamir.WEBUI.Services
{
    public class BalanceManager
    {
        private readonly IClientService _clientService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly IBalanceLogService _balanceLogService;
        private readonly IMapper _mapper;

        public BalanceManager(IClientService clientService, UserManager<Mechanic> userManager, IBalanceLogService balanceLogService, IMapper mapper)
        {
            _clientService = clientService;
            _userManager = userManager;
            _balanceLogService = balanceLogService;
            _mapper = mapper;

        }
        public async Task<bool> AddPayment(int ClientId, DateTime PaymentDate, decimal Amount)
        {
            

            var client = await _clientService.GetOneAsync(id: ClientId, mechanicId: user.Id, false, false);
           
            decimal oldBalance = client.Balance;
            decimal newBalance = oldBalance - Amount;


            var log = new BalanceLog
            {
                ClientId = ClientId,
                PaymentDate = PaymentDate,
                Amount = Amount,
                OldBalance = oldBalance,
                NewBalance = newBalance
            };


            client.Balance = newBalance;


            var balanceLogResult = await _balanceLogService.CreateAsync(log);


            if (balanceLogResult > 0)
            {
                return true;
            }
            
            
            return false;
            

            
        }



    }
}
