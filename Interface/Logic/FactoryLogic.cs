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
        private readonly BaseRepository baseRepo;
        public FactoryLogic(ISqlSugarClient sqlSugarClient)
        {
            baseRepo = new BaseRepository(sqlSugarClient);
        }

        public ValueTask<int> SaveEntity(Factory entity)
        {
            return baseRepo.SaveEntity<Factory>(entity);
        }

        public ValueTask<int> UpdateEntity(Factory entity)
        {
            return baseRepo.UpdateEntity<Factory>(entity);
        }

        public ValueTask<int> DeleteEntity(int id)
        {
            return baseRepo.DeleteEntity<Factory>(id);
        }

        public ValueTask<Factory> GetEntity(int id)
        {
            return baseRepo.GetEntity<Factory>(id);
        }

        public ValueTask<List<Factory>> GetList(Expression<Func<Factory, bool>> expression)
        {
            return baseRepo.GetList(expression);
        }

        public ValueTask<List<Factory>> GetPageList(Expression<Func<Factory, bool>> expression, int pageIndex, int pageSize)
        {
            return baseRepo.GetPageList(expression, pageIndex, pageSize);
        }
    }
}
