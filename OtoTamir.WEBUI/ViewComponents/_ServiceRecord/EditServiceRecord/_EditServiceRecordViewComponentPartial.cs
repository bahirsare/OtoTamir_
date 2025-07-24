using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System.Security.Claims;

namespace OtoTamir.WEBUI.ViewComponents._ServiceRecord.EditServiceRecord
{
    public class _EditServiceRecordViewComponentPartial:ViewComponent
    {
        
        private readonly IServiceRecordService _serviceRecordService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly IMapper _mapper;

        public _EditServiceRecordViewComponentPartial(IServiceRecordService serviceRecordService, UserManager<Mechanic> userManager, IMapper mapper)
        {
            
            _serviceRecordService = serviceRecordService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int recordId,string returnUrl)
        {
            var mechanic = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var record = await _serviceRecordService.GetOneAsync(id: recordId, mechanicId: mechanic.Id, includeSymptoms: true,includeVehicle:false);
            if (record == null)
            {
                TempData["Messaage"] = "Kayıt bulunamadı.";
            }
            var recordDTO = _mapper.Map<EditServiceRecordDTO>(record);
            recordDTO.ReturnUrl = returnUrl;
            return View(recordDTO);
        }
    }
}
