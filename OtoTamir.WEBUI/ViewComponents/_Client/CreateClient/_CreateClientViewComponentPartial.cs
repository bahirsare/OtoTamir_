using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

namespace OtoTamir.WEBUI.ViewComponents._Client.CreateClient
{
    public class _CreateClientViewComponentPartial : ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync(string returnUrl)
        {

            var model = new CreateClientDTO() { ReturnUrl = returnUrl};

            return View(model);

        }



    }
}
