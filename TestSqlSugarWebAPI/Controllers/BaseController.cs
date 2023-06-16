using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TestSqlSugarWebAPI.Controllers
{
    /// <summary>
    /// 控制器访问拦截器
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 接口执行前执行
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
                //记录异常记录
                //StringBuilder sbException = new StringBuilder();
                //Exception exception = resultContext.Exception;
                //sbException.AppendLine(exception.Message);
                //while (exception.InnerException != null)
                //{
                //    sbException.AppendLine(exception.InnerException.Message);
                //    exception = exception.InnerException;
                //}
                //sbException.AppendLine(resultContext.Exception.StackTrace);
                throw resultContext.Exception;
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
