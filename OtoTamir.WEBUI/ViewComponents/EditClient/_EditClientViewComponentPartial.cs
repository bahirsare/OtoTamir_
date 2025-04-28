using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.Client;
using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.ViewComponents.EditClient
{

    public class _EditClientViewComponentPartial : ViewComponent
    {
        private readonly IMapper _mapper;
        public _EditClientViewComponentPartial(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IViewComponentResult Invoke(Client model)
        {
            var editClientDTO = _mapper.Map<EditClientDTO>(model);


            return View(editClientDTO);
        }
    }


}
