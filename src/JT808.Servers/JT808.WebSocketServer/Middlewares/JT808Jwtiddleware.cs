using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JT808.WebSocketServer.Middlewares
{
    public class JT808Jwtiddleware 
    {
        private readonly RequestDelegate next;

        private readonly ILogger logger;

        public JT808Jwtiddleware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger<JT808Jwtiddleware>();
        }

        public async Task Invoke(HttpContext context) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(getIp(context));
            sb.Append(",");
            sb.Append(getBrowser(context));
            sb.Append(",");
            if (context.Request.Headers["Connection"] == "Upgrade")
            {
                if(context.Request.Query.TryGetValue("access_token", out var token))
                {
                    if (token == "")
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        sb.Append(token);
                    }
#warning 去认证服务器请求结果
                    // 去认证服务器请求结果
                }
            }
            logger.LogInformation(sb.ToString());
            await next(context);
        }

        private string getIp(HttpContext context)
        {
            Microsoft.Extensions.Primitives.StringValues ips;
            if (context.Request.Headers.TryGetValue("X-Real-IP", out ips))
            {
                return ips.FirstOrDefault() ?? "";
            }
            else
            {
                return context.Connection.RemoteIpAddress?.ToString() ?? "";
            }
        }

        private static string getBrowser(HttpContext context)
        {
            return context.Request.Headers["User-Agent"].FirstOrDefault();
        }
    }
}