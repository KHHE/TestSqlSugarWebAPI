using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TestSqlSugarWebAPI.AuthorizeHandler
{
    public class ApiAuthorizeHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "DEFAULT_SCHEME";

        public ApiAuthorizeHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
           : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers["User-Agent"];
            if (string.IsNullOrEmpty(token))
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
            }, SchemeName); 
            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, SchemeName);
        }
    }
}
