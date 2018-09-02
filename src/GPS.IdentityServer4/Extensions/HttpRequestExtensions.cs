using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetIp(this HttpRequest httpRequest)
        {
            Microsoft.Extensions.Primitives.StringValues ips;
            if (httpRequest.Headers.TryGetValue("X-Real-IP", out ips))
            {
                return ips.FirstOrDefault() ?? "";
            }
            else
            {
                return httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
            }
        }
    }
}
