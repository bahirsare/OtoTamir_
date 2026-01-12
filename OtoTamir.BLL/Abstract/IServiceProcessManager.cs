using OtoTamir.CORE.DTOs.ServiceRecordDTOs;
using OtoTamir.CORE.Entities;

namespace OtoTamir.BLL.Abstract
{
    public interface IServiceProcessManager
    {
        Task CompleteServiceProcessAsync(ServiceCompletionDTO model);
        Task ReceivePaymentAsync(int clientId, decimal amount, string description, PaymentSource paymentSource, string mechanicId, string authorName, int? posTerminalId = null, int? targetBankId = null);
    }
}
