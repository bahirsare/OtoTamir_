using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.ViewComponents.EditClient
{
    
        public class _EditClientViewComponentPartial : ViewComponent
        {
            public IViewComponentResult Invoke(Client client)
            {
                return View(client);
            }
        }
    

}
