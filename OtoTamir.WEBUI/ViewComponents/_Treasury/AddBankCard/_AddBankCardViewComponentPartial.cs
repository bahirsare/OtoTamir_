using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Treasury.AddBankCard
{
    public class _AddBankCardViewComponentPartial : ViewComponent
    {
        private readonly IBankService _bankService;
        private readonly UserManager<Mechanic> _userManager;

        public _AddBankCardViewComponentPartial(IBankService bankService, UserManager<Mechanic> userManager)
        {
            _bankService = bankService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user =await _userManager.GetUserAsync((ClaimsPrincipal)User);

            var model = new AddBankCardDTO
            {
                // Dropdown için banka listesini burada dolduruyoruz
                AvailableBanks = await _bankService.GetAllAsync(user.Id)
            };

            return View(model);
        }
    }
}