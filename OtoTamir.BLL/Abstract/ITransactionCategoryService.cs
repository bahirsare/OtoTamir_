using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Abstract
{
    public interface ITransactionCategoryService: IRepositoryService<TransactionCategory>
    {
        Task<List<TransactionCategory>> GetAllAsync(string mechanicId);
        Task<TransactionCategory> GetOneAsync(int id, string mechanicId);
        

        Task<bool> IsCategoryExistsAsync(string name, string mechanicId);
    }
}
