using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.TreasuryDTOs;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._Treasury.EditPosTerminal
{
    public class _EditPosTerminalViewComponentPartial : ViewComponent
    {
        private readonly IPosTerminalService _posTerminalService;
        private readonly IBankService _bankService;
        private readonly IMapper _mapper; 

        public _EditPosTerminalViewComponentPartial(
            IPosTerminalService posTerminalService,
            IBankService bankService,
            IMapper mapper) 
        {
            _posTerminalService = posTerminalService;
            _bankService = bankService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var pos = await _posTerminalService.GetOneAsync(id, userId);
            if (pos == null) return Content("Bulunamadı");

            ViewBag.Banks = await _bankService.GetAllAsync(userId);

            var model = _mapper.Map<EditPosTerminalDTO>(pos);

            return View(model);
        }
    }
}
