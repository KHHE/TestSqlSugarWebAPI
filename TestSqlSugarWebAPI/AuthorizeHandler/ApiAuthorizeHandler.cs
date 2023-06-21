using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TestSqlSugarWebAPI.AuthorizeHandler
{
    /// <summary>
    /// WebApi权限认证控制器
    /// </summary>
    public class ApiAuthorizeHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// 权限认证类型
        /// </summary>
        public const string SCHEME_NAME = "DEFAULT_SCHEME";

        /// <summary>
        /// 身份认证密钥Key
        /// </summary>
        public const string AUTH_KEY = "SECRET";

        /// <summary>
        /// 身份认证密钥密码
        /// </summary>
        public const string AUTH_PWD = "AUTH_PWD_28CD-4E0B-A4B2-B58DFCAC3B6F";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        public ApiAuthorizeHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
           : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// 访问认证、授权逻辑
        /// </summary>
        /// <returns></returns>
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (AUTH_PWD != Request.Headers[AUTH_KEY]) //验证授权密码错误
            {
                await Context.Response.WriteAsJsonAsync(new { Code = 401, Message = "身份验证失败", Result = string.Empty });
                return AuthenticateResult.NoResult();
            }
            return AuthenticateResult.Success(GetAuthTicket("admin", "admin"));
        }

        /// <summary>
        /// 获取成功后的用户授权
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private AuthenticationTicket GetAuthTicket(string name, string role)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, role),
            }, SCHEME_NAME);
            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, SCHEME_NAME);
        }
    }
}
