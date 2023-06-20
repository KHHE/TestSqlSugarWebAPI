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
        /// 身份认证密钥
        /// </summary>
        public const string AUTH_KEY = "AUTH_KEY_C2118DD4-28CD-4E0B-A4B2-B58DFCAC3B6F";

        public ApiAuthorizeHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
           : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authKey = Request.Headers["swagger"];
            if (AUTH_KEY != authKey) //验证授权Key错误
            {
                await Context.Response.WriteAsJsonAsync(new { Code = 401, Message = "身份验证不通过", Result = string.Empty });
                return AuthenticateResult.NoResult();
            }
            return AuthenticateResult.Success(GetAuthTicket("admin", "admin"));
        }

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
