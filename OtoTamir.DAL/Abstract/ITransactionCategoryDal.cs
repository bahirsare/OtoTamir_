using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Abstract
{
    public interface ITransactionCategoryDal : IRepositoryService<TransactionCategory>
    {
      
        Task<List<TransactionCategory>> GetAllAsync(string mechanicId);

       
        Task<TransactionCategory> GetOneAsync(int id, string mechanicId);

        
        Task<bool> IsNameExistsAsync(string name, string mechanicId);
    }
}