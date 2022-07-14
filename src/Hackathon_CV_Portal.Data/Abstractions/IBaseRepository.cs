﻿using Hackathon_CV_Portal.Data.Pagination;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Data.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<DomainPagedResult<T>> GetAllAsyncByPage(int page, int resultsPerPage = 10);
        Task<T> GetAsync(object key);
        Task<T> GetForUpdateAsync(object key);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(params object[] key);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
