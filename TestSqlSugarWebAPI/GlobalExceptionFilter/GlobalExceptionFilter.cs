using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Utils;

namespace TestSqlSugarWebAPI.Filter
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary> 
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
       
        /// <summary>
        /// 执行异常时设置结果 同步方式
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            TData obj = new TData();
            obj.Message = context.Exception.GetOriginalException().Message;
            if (string.IsNullOrEmpty(obj.Message))
            {
                obj.Message = "抱歉，系统异常或网络异常，请重试！";
            }
            //可定义权限控制过滤器，操作数据库记录访问日志
            context.ExceptionHandled = true;      //标识异常已经处理，不报异常
            context.Result = new JsonResult(obj); //设置接口异常后返回结果
        }

        /// <summary> 
        /// 执行异常时设置结果 异步方式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }

    public class TData
    {
        public string Message { get; internal set; }
    }

    public class TData<T>: TData
    {
        public T data { get; set; }
    }
}
