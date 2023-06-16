using Dal;
using Entity;
using Interface;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestSqlSugarWebAPI.Controllers
{
    /// <summary>
    /// 工厂控制器
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class FactoryController : BaseController
    {
        
        private readonly IFactoryLogic factoryLogic;
        /// <summary>
        /// 构造函数注入服务
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public FactoryController(ISqlSugarClient sqlSugarClient)
        {
            factoryLogic = new FactoryLogic(sqlSugarClient);
        }

        /// <summary>
        /// 实现获取功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Factory> GetEntity(int id)
        {
            return await factoryLogic.GetEntity(id);
        }

        /// <summary>
        /// 实现新增功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SaveEntity(Factory entity)
        {
            return await factoryLogic.SaveEntity(entity);
        }

        /// <summary>
        /// 实现删除功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> DeleteEntity(int id)
        {
            return await factoryLogic.DeleteEntity(id);
        }

        /// <summary>
        /// 实现更新功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> UpdateEntity(Factory entity)
        {
            return await factoryLogic.UpdateEntity(entity);
        }

        /// <summary>
        /// 实现List功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Factory>> GetList(FactoryDTO dto)
        {
            var expression = LinqExtensions.True<Factory>();
            expression = expression.And(x => x.Name.Contains(dto.Name));
            return await factoryLogic.GetList(expression);
        }

        /// <summary>
        /// 实现分页功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Factory>> GetPageList(FactoryDTO dto, int pageIndex, int pageSize)
        {
            var expression = LinqExtensions.True<Factory>();
            expression = expression.And(x => x.Name.Contains(dto.Name));
            expression = expression.Or(x => x.Addr.Contains(dto.Addr));
            return await factoryLogic.GetPageList(expression, pageIndex, pageSize);
        }
    }
}
