using Service;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entity;

namespace Interface
{
    public class FactoryLogic : IFactoryLogic
    {
        private readonly BaseDao baseDao;
        public FactoryLogic(ISqlSugarClient sqlSugarClient)
        {
            baseDao = new BaseDao(sqlSugarClient);
        }

        public ValueTask<int> SaveEntity(Factory entity)
        {
            return baseDao.SaveEntity<Factory>(entity);
        }

        public ValueTask<int> UpdateEntity(Factory entity)
        {
            return baseDao.UpdateEntity<Factory>(entity);
        }

        public ValueTask<int> DeleteEntity(int id)
        {
            return baseDao.DeleteEntity<Factory>(id);
        }

        public ValueTask<Factory> GetEntity(int id)
        {
            return baseDao.GetEntity<Factory>(id);
        }

        public ValueTask<List<Factory>> GetList(Expression<Func<Factory, bool>> expression)
        {
            return baseDao.GetList(expression);
        }

        public ValueTask<List<Factory>> GetPageList(Expression<Func<Factory, bool>> expression, int pageIndex, int pageSize)
        {
            return baseDao.GetPageList(expression, pageIndex, pageSize);
        }
    }
}
