using Dal;
using Entity;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoudleCreateCode;
using SqlSugar;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace TestSqlSugarWebAPI.Controllers
{
    /// <summary>
    /// 工厂控制器
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class ToolController : BaseController
    {
        
        private readonly SingleTableTemplate singleTableTemplate;
        /// <summary>
        /// 构造函数注入服务
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public ToolController(ISqlSugarClient sqlSugarClient)
        {
            singleTableTemplate = new SingleTableTemplate(sqlSugarClient);
        }

        /// <summary>
        /// 实现获取功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TableFieldInfo>> GetTableFieldList(string tableName)
        {
            return await singleTableTemplate.GetTableFieldList(tableName);
        }

       
    }
}
