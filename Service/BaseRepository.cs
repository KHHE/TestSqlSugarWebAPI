using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal
{
    public class BaseRepository<T>: SimpleClient<T> where T : class, new()
    {
        public BaseRepository(ISqlSugarClient db)
        {
            base.Context = db;
        }

        /// <summary>
        /// 扩展方法，仅查询sql返回List<T>，不分页
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<List<T>> SqlQueryable(string sql)
        {
            return await base.Context.SqlQueryable<T>(sql).ToListAsync();
        }

        /// <summary>
        /// 扩展方法，仅查询sql返回List<T>，带sql参数，不分页
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">如果多个参数 new { id=1 , name="xx"} 用逗号隔开</param>
        /// <returns></returns>
        public async Task<List<T>> SqlQueryable(string sql, object parameters)
        {
            return await base.Context.SqlQueryable<T>(sql).AddParameters(parameters).ToListAsync();
        }

        /// <summary>
        /// 扩展方法，仅查询sql返回List<T>，sql内不要写分页，不带sql参数，自动分页
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> SqlQueryablePage(string sql, int pageIndex, int pageSize, RefAsync<int> totalNumber)
        {
            return await base.Context.SqlQueryable<T>(sql).ToPageListAsync(pageIndex, pageSize, totalNumber);
        }

        /// <summary>
        /// 扩展方法，仅查询sql返回List<T>，sql内不要写分页，带sql参数，自动分页
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">如果多个参数 new { id=1 , name="xx"} 用逗号隔开</param>
        /// <returns></returns>
        public async Task<List<T>> SqlQueryablePage(string sql, object parameters, int pageIndex, int pageSize, RefAsync<int> totalNumber)
        {
            return await base.Context.SqlQueryable<T>(sql).AddParameters(parameters).ToPageListAsync(pageIndex, pageSize, totalNumber);
        }

        /// <summary>
        /// 扩展方法，仅执行更新、删除sql语句，返回是否执行成功
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">如果多个参数 new { id=1 , name="xx"} 用逗号隔开</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandAsync(string sql, object parameters)
        {
            return await base.Context.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }
    }
}
