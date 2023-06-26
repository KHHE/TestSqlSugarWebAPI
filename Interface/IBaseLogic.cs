using Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interface
{
    public interface IBaseLogic<T> where T : BaseEntity, new()
    {
        Task<bool> InsertAsync(Factory entity);

        Task<bool> UpdateAsync(Factory entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<Factory> GetByIdAsync(int id);

        Task<List<Factory>> GetListAsync(Expression<Func<Factory, bool>> expression);

        Task<List<Factory>> GetPageListAsync(Expression<Func<Factory, bool>> expression, int pageIndex, int pageSize);
    }
}
