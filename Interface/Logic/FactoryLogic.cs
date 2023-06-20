using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entity;
using Dal;

namespace Interface
{
    public class FactoryLogic : IFactoryLogic
    {
        private readonly BaseRepository<Factory> baseRepo;

        /// <summary>
        /// 构造函数IOC注入ISqlSugarClient数据库服务
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public FactoryLogic(ISqlSugarClient sqlSugarClient)
        {
            baseRepo = new BaseRepository<Factory>(sqlSugarClient);
        }

        public async Task<bool> InsertAsync(Factory entity)
        {
            return await baseRepo.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(Factory entity)
        {
            throw new System.Exception("更新异常了");
            return await baseRepo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await baseRepo.DeleteByIdAsync(id);
        }

        public async Task<Factory> GetByIdAsync(int id)
        {
            return await baseRepo.GetByIdAsync(id);
        }

        public async Task<List<Factory>> GetListAsync(Expression<Func<Factory, bool>> expression)
        {
            return await baseRepo.GetListAsync(expression);
        }

        public async Task<List<Factory>> GetPageListAsync(Expression<Func<Factory, bool>> expression, int pageIndex, int pageSize)
        {
            return await baseRepo.GetPageListAsync(expression, new PageModel { PageIndex = pageIndex, PageSize = pageSize});
        }
    }
}
