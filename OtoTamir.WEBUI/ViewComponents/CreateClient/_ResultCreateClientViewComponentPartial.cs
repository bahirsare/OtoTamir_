using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Models;

namespace OtoTamir.WEBUI.ViewComponents.CreateClient
{
    public class _ResultCreateClientViewComponentPartial : ViewComponent
    {
       

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = new CreateClientViewModel();
            
            return View(model);

        }
        

    }
}
