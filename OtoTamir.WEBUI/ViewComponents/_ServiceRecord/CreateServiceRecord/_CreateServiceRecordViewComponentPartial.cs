using Microsoft.AspNetCore.Mvc;
using OtoTamir.CORE.DTOs.SymptomDTOs;

namespace OtoTamir.WEBUI.ViewComponents._ServiceRecord.CreateServiceRecord
{
    public class _CreateServiceRecordViewComponentPartial : ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync(int vehicleId)
        {
            var model = new CreateSymptomGroupDTO
            {
                VehicleId = vehicleId,
                Symptoms = new List<SymptomDTO>
        {
            new SymptomDTO()
        }

            };
            return View(model); 
        }
    }
}
