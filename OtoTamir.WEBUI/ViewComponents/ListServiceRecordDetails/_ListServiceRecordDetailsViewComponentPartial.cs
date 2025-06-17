using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Identity;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents.ListServiceRecordDetails
{
    public class _ListServiceRecordDetailsViewComponentPartial:ViewComponent
    {
        private readonly IServiceRecordService _serviceRecordService;
        private readonly UserManager<Mechanic> _userManager;

        public _ListServiceRecordDetailsViewComponentPartial(IServiceRecordService serviceRecordService, UserManager<Mechanic> userManager)
        {
            _serviceRecordService = serviceRecordService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var userId = (User as ClaimsPrincipal)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var record = await _serviceRecordService.GetOneAsync(id,userId,true,true);
            if (record == null)
                return View("Error", $"Servis kaydı bulunamadı. ID: {id}");
            return View(record);
        }
        }
}
