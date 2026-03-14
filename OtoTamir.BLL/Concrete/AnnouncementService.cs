using OtoTamir.BLL.Abstract;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Repositories;
using OtoTamir.CORE.Utilities;
using OtoTamir.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OtoTamir.BLL.Concrete
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementDal _announcementDal;

        public AnnouncementService(IAnnouncementDal announcementDal)
        {
            _announcementDal = announcementDal;
        }

       

        public async Task<int> UpdateAsync()
        {
            return await _announcementDal.UpdateAsync();
        }

        // --- Çöp Kutusu (Recycle Bin) için eklediğimiz özel metodlar ---
        public async Task<int> RestoreAsync(int id)
        {
            return await _announcementDal.RestoreAsync(id);
        }

        public async Task<PagedResult<Announcement>> GetDeletedPagedAsync(Expression<Func<Announcement, bool>> filter = null, Func<IQueryable<Announcement>, IOrderedQueryable<Announcement>> orderBy = null, int page = 1, int pageSize = 10, params Expression<Func<Announcement, object>>[] includes)
        {
            return await _announcementDal.GetDeletedPagedAsync(filter, orderBy, page, pageSize, includes);
        }

        async Task<int> IRepositoryService<Announcement>.CreateAsync(Announcement entity)
        {
            return await _announcementDal.CreateAsync(entity);
        }

        async Task<int> IRepositoryService<Announcement>.DeleteAsync(int id)
        {
            return await _announcementDal.DeleteAsync(id);
        }

        async Task<bool> IRepositoryService<Announcement>.AnyAsync(Expression<Func<Announcement, bool>> filter)
        {
            return await _announcementDal.AnyAsync(filter);
        }

        async Task<PagedResult<Announcement>> IRepositoryService<Announcement>.GetPagedAsync(Expression<Func<Announcement, bool>> filter, Func<IQueryable<Announcement>, IOrderedQueryable<Announcement>> orderBy, int page, int pageSize, params Expression<Func<Announcement, object>>[] includes)
        {
            return await _announcementDal.GetPagedAsync(filter,orderBy,page,pageSize, includes);
        }
    }
}