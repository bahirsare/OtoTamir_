using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;

namespace OtoTamir.WEBUI.ViewComponents._Client.EditClient
{

    public class _EditClientViewComponentPartial : ViewComponent
    {
        private readonly IMapper _mapper;
        public _EditClientViewComponentPartial(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IViewComponentResult Invoke(CORE.Entities.Client model)
        {
            var editClientDTO = _mapper.Map<EditClientDTO>(model);


            return View(editClientDTO);
        }
    }


}
