using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entity;

namespace Service
{
    public class BaseDao
    {
        private readonly ISqlSugarClient db;
        public BaseDao(ISqlSugarClient sqlSugarClient)
        {
            db = sqlSugarClient;
        }

        /// <summary>
        /// 主库链接
        /// </summary>
        /// <returns></returns>
        //public SqlSugarClient GetInstance()
        //{
        //    SqlSugarClient db = null;
        //    db = new SqlSugarClient(new SqlSugar.ConnectionConfig()
        //    {
        //        DbType = SqlSugar.DbType.Sqlite,
        //            ConnectionString = "Data Source=BIELITHZ356;Database=Test;Uid=sa;Pwd=6MonkeysRLooking^;Enlist=true;Pooling=true;Connect TimeOut=3000;",
        //            IsAutoCloseConnection = true,
        //    });
        //    db.Ado.IsEnableLogEvent = true;
        //    return db;
        //}

        /// <summary>
        /// 通过Id获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async ValueTask<T> GetEntity<T>(int id) where T : BaseEntity, new()
        {
            return await db.Queryable<T>().InSingleAsync(id);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<int> SaveEntity<T>(object entity) where T : BaseEntity, new()
        {
            return await db.Insertable<T>(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<int> UpdateEntity<T>(object entity) where T : BaseEntity, new()
        {
            return await db.Updateable<T>(entity).CallEntityMethod(x => x.Modify()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async ValueTask<int> DeleteEntity<T>(int id) where T : BaseEntity, new()
        {
            return await db.Deleteable<T>().In(id).ExecuteCommandAsync();
        }

        /// <summary>
        /// 获取List数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async ValueTask<List<T>> GetList<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            return await db.Queryable<T>().Where(expression).ToListAsync();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async ValueTask<List<T>> GetPageList<T>(Expression<Func<T, bool>> expression, int pageIndex, int pageSize) where T : BaseEntity, new()
        {
            return await db.Queryable<T>().Where(expression).ToPageListAsync(pageIndex, pageSize);
        }
    }
}
