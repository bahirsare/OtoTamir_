using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using System.Linq.Expressions;

namespace OtoTamir.BLL.Concrete
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryDal _transactionCategoryDal;

        public TransactionCategoryService(ITransactionCategoryDal transactionCategoryDal)
        {
            _transactionCategoryDal = transactionCategoryDal;
        }

        public async Task<List<TransactionCategory>> GetAllAsync(string mechanicId)
        {
            return await _transactionCategoryDal.GetAllAsync(mechanicId);
        }

        public async Task<int> CreateAsync(TransactionCategory entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name)) return 0;


            bool exists = await _transactionCategoryDal.IsNameExistsAsync(entity.Name, entity.MechanicId);
            if (exists) throw new Exception("Bu kategori zaten mevcut.");

            return await _transactionCategoryDal.CreateAsync(entity);
        }

        async Task<List<TransactionCategory>> ITransactionCategoryService.GetAllAsync(string mechanicId)
        {
            return await _transactionCategoryDal.GetAllAsync(mechanicId);
        }

        async Task<TransactionCategory> ITransactionCategoryService.GetOneAsync(int id, string mechanicId)
        {
            return await _transactionCategoryDal.GetOneAsync(id, mechanicId);
        }

        async Task<bool> ITransactionCategoryService.IsCategoryExistsAsync(string name, string mechanicId)
        {
            return await _transactionCategoryDal.IsNameExistsAsync(name, mechanicId);
        }

        async Task<int> IRepositoryService<TransactionCategory>.CreateAsync(TransactionCategory entity)
        {
            return await CreateAsync(entity);
        }

        async Task<int> IRepositoryService<TransactionCategory>.UpdateAsync()
        {
            return await _transactionCategoryDal.UpdateAsync();
        }

        async Task<int> IRepositoryService<TransactionCategory>.DeleteAsync(int id)
        {
            return await _transactionCategoryDal.DeleteAsync(id);
        }

        async Task<bool> IRepositoryService<TransactionCategory>.AnyAsync(Expression<Func<TransactionCategory, bool>> filter)
        {
            return await _transactionCategoryDal.AnyAsync(filter);
        }

        Task<PagedResult<TransactionCategory>> IRepositoryService<TransactionCategory>.GetPagedAsync(Expression<Func<TransactionCategory, bool>> filter, Func<IQueryable<TransactionCategory>, IOrderedQueryable<TransactionCategory>> orderBy, int page, int pageSize, params Expression<Func<TransactionCategory, object>>[] includes)
        {
            return _transactionCategoryDal.GetPagedAsync(filter, orderBy, page, pageSize, includes);
        }
    }
}