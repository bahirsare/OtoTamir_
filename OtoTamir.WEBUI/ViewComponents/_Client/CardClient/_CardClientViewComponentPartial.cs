using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ClientDTOs;

namespace OtoTamir.WEBUI.ViewComponents._Client.CardClient
{
    public class _CardClientViewComponentPartial:ViewComponent
    {
        private readonly IClientService _clientService;

        public _CardClientViewComponentPartial(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId)
        {

            var model = await _clientService.GetOneAsync(clientId);

            return View(model);

        }
    }
}
