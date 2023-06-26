using Dal;
using Entity;
using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestSqlSugarWebAPI.Controllers
{
    /// <summary>
    /// 工厂控制器
    /// </summary>
    [Authorize] //需要授权密钥才能访问接口，逻辑：ApiAuthorizeHandler
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
        public async Task<IActionResult> GetEntity(int id)
        {
            return Json(await factoryLogic.GetByIdAsync(id));
        }

        /// <summary>
        /// 实现新增功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SaveEntity(Factory entity)
        {
            return await factoryLogic.InsertAsync(entity);
        }

        /// <summary>
        /// 实现删除功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteEntity(int id)
        {
            return await factoryLogic.DeleteByIdAsync(id);
        }

        /// <summary>
        /// 实现更新功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateEntity(Factory entity)
        {
            return await factoryLogic.UpdateAsync(entity);
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
            return await factoryLogic.GetListAsync(expression);
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
            return await factoryLogic.GetPageListAsync(expression, pageIndex, pageSize);
        }
    }
}
