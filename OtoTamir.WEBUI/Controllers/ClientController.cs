using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Managers;
using OtoTamir.CORE.DTOs.ClientDTOs;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using OtoTamir.WEBUI.Services;
using System.Security.Permissions;

namespace OtoTamir.WEBUI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly UserManager<Mechanic> _userManager;
        private readonly IMapper _mapper;
        private readonly IServiceProcessManager _serviceProcessManager;


        public ClientController(IClientService clientService, UserManager<Mechanic> userManager, IMapper mapper, IServiceProcessManager serviceProcessManager)
        {
            _clientService = clientService;
            _userManager = userManager;
            _mapper = mapper;
            _serviceProcessManager = serviceProcessManager;
        }
        public async Task<IActionResult> Clients(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Search = searchString;


            var mechanic = await _userManager.GetUserAsync(User);
            if (!mechanic.IsProfileCompleted)
            {
                TempData["Message"] = "Lütfen bilgilerinizi doldurunuz";
                return RedirectToAction("Profile", "Account");
            }
            var clients = await _clientService.GetAllAsync(mechanic.Id, includeVehicles: true);

            if (!string.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.Name.IndexOf(searchString, StringComparison.CurrentCultureIgnoreCase) >= 0 || c.PhoneNumber.IndexOf(searchString, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
            }

            clients = sortOrder switch
            {
                "name_desc" => clients.OrderByDescending(c => c.Name).ToList(),
                "balance" => clients.OrderBy(c => c.Balance).ToList(),
                "balance_desc" => clients.OrderByDescending(c => c.Balance).ToList(),
                "created" => clients.OrderBy(c => c.CreatedDate).ToList(),
                "created_desc" => clients.OrderByDescending(c => c.CreatedDate).ToList(),
                "modified" => clients.OrderBy(c => c.ModifiedDate).ToList(),
                "modified_desc" => clients.OrderByDescending(c => c.ModifiedDate).ToList(),
                _ => clients.OrderBy(c => c.Name).ToList()
            };

            return View(clients);
        }

        public async Task<IActionResult> ClientDetails(int clientId)
        {

            var mechanic = await _userManager.GetUserAsync(User);
            if (mechanic == null)
            {
                TempData["FailMessage"] = "Tamirci bulunamadı.";
                return RedirectToAction("Clients", "Client");
            }
            var client = await _clientService.GetOneAsync(clientId, mechanic.Id, includeServiceRecords: true, includeVehicles: true);
            if (client == null)
            {

                TempData["FailMessage"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Client");
            }
            return View(client);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNotes(string note, int clientId, string returnUrl)
        {
            List<string> URL = returnUrl.Split('/').ToList();
            var mechanic = await _userManager.GetUserAsync(User);
            if (mechanic == null)
            {
                TempData["FailMessage"] = "Tamirci bulunamadı, devam etmek için giriş yapınız.";
                return RedirectToAction("Login", "Account");
            }
            var client = await _clientService.GetOneAsync(clientId, mechanic.Id, includeServiceRecords: true, includeVehicles: true);
            if (client == null)
            {
                TempData["FailMessage"] = "Müşteri bulunamadı.";
                return RedirectToAction(URL[1], URL[0]);
            }
            client.Notes = note;
            var result = await _clientService.UpdateAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Not güncellendi.";
                return RedirectToAction(URL[1], URL[0], clientId); ;
            }
            TempData["FailMessage"] = "Bir hata oluştu.Lütfen tekrar deneyiniz.";

            return RedirectToAction(URL[1], URL[0], clientId);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDTO model)
        {
            List<string> URL = model.ReturnUrl.Split('/').ToList();
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Müşteri Eklenemedi, Lütfen Bilgileri Eksiksiz Doldurun";

                return RedirectToAction(URL[0], URL[1]);
            }
            var currentMechanicId = _userManager.GetUserId(User);

            bool mustBeUnique = await _clientService
    .AnyAsync(i => i.PhoneNumber == model.PhoneNumber && i.MechanicId == currentMechanicId);

            if (!mustBeUnique)
            {
                model.PhoneNumber = model.PhoneNumber?.Replace(" ", "");
                Client client = _mapper.Map<Client>(model);
                client.MechanicId = _userManager.GetUserId(User);
                var result = await _clientService.CreateAsync(client);
                if (result == 1)
                {
                    TempData["Message"] = "Müşteri Eklendi!";
                }
                else
                {
                    TempData["Message"] = "Müşteri Eklenemedi.";
                }
                return RedirectToAction(URL[0], URL[1]);
            }
            else
            {
                TempData["FailMessage"] = "Aynı telefon numarasına sahip müşteri var!";
                return RedirectToAction(URL[0], URL[1]);
            }
        }
        public async Task<IActionResult> DeleteClient(int id)
        {
            var mechanicId = _userManager.GetUserId(User);
            var client = await _clientService.GetOneAsync(id, mechanicId, includeVehicles: false, includeServiceRecords: false);
            if (client == null)
            {
                TempData["FailMessage"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Client");
            }
            var result = await _clientService.DeleteAsync(id);
            if (result > 0)
            {
                TempData["Message"] = "Müşteri başarıyla silindi.";
            }
            else
            {
                TempData["Message"] = "Bir hata oluştu, müşteri silinemedi!";
            }
            return RedirectToAction("Clients", "Client");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(EditClientDTO model, IFormFile image)
        {
            var mechanicId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Müşteri bilgileri geçersiz!";
                return RedirectToAction("Clients", "Client");
            }
            var client = await _clientService.GetOneAsync(model.Id, mechanicId, includeVehicles: false, includeServiceRecords: false);
            if (client == null)
            {
                TempData["Message"] = "Müşteri bulunamadı.";
                return RedirectToAction("Clients", "Client");
            }
            _mapper.Map(model, client);
            var result = await _clientService.UpdateAsync();
            if (result > 0)
            {
                TempData["Message"] = "Müşteri başarıyla güncellendi.";
            }
            else
            {
                TempData["Message"] = "Güncelleme sırasında bir hata oluştu.";
            }
            return RedirectToAction("Clients", "Client");

        }
        [HttpGet]
        public async Task<IActionResult> FinancialHistory(int clientId)
        {
            
            var user = await _userManager.GetUserAsync(User);

           
            

            var statement = await _clientService.GetClientStatementAsync(clientId, user.Id, user.TreasuryId);

            if (statement == null)
            {
                return RedirectToAction("Clients");
            }

            return View(statement);
        }
        [HttpPost]
        public async Task<IActionResult> MakePayment(
        int clientId,
        decimal amount,
        string description,
        string paymentSource,
        string returnUrl,
        string authorName,
        int? posTerminalId, 
        int? targetBankId  
)
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                var sourceEnum = (PaymentSource)Enum.Parse(typeof(PaymentSource), paymentSource);

                

                await _serviceProcessManager.ReceivePaymentAsync(
                    clientId, amount, description, sourceEnum, user.Id, authorName, posTerminalId,targetBankId
                );

                TempData["SuccessMessage"] = "Ödeme alındı.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hata: " + ex.Message;
            }

            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("FinancialHistory", new { id = clientId });
        }
    }
}
