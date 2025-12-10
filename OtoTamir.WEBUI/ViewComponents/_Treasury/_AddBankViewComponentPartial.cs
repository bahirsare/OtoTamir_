using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.TreasuryDTOs;

namespace OtoTamir.WEBUI.ViewComponents._Treasury
{
    public class _AddBankViewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View(new AddBankDTO());
        }
    }
}
