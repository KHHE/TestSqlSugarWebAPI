using Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interface
{
    public interface IBaseLogic<T> where T: BaseEntity, new()
    {
        /// <summary>
        /// 通过Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ValueTask<T> GetEntity(int id);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> SaveEntity(T entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> UpdateEntity(T entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ValueTask<int> DeleteEntity(int id);

        /// <summary>
        /// 获取List数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ValueTask<List<T>> GetList(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ValueTask<List<T>> GetPageList(Expression<Func<T, bool>> expression, int pageIndex, int pageSize);
    }
}
