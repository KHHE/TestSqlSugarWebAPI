using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Threading.Tasks;
using TestSqlSugarWebAPI.Filter;
using Utils;

namespace TestSqlSugarWebAPI.Controllers
{
    /// <summary>
    /// 控制器访问拦截器
    /// </summary>
    public class BaseController : Controller
    {
        protected readonly ILogger _logger = null;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 接口执行前执行拦截功能，如记录日志、判断权限等
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            #region 异常获取
            var resultContext = await next();
            var controllerName = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["Action"].ToString();
            if (resultContext.Exception != null) //报异常了
            {
                //TData obj = new TData();
                ////obj.Message = resultContext.Exception.GetOriginalException().Message;
                //if (string.IsNullOrEmpty(obj.Message))
                //{
                //    obj.Message = "抱歉，异常或网络异常，请重试！";
                //}
                ////可定义权限控制过滤器，操作数据库记录访问日志
                //resultContext.ExceptionHandled = true;      //标识异常已经处理，不报异常
                //resultContext.Result = new JsonResult(obj); //设置接口异常后返回结果
                //return;
            }
            #endregion
        }

        /// <summary>
        /// 接口执行后执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
