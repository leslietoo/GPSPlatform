using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GPS.IdentityServer4IGrain.Dtos
{
    public class JwtOptionsParameterDto
    {
        ///// <summary>
        ///// token是谁颁发的
        ///// </summary>
        //public string Issuer { get; set; }
        ///// <summary>
        ///// 是否需要认证谁颁发的
        ///// </summary>
        //public bool ValidateIssuer { get; set; }
        ///// <summary>
        ///// token可以给哪些客户端使用
        ///// </summary>
        //public string Audience { get; set; }
        ///// <summary>
        ///// 是否需要认证
        ///// </summary>
        //public bool ValidateAudience { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public IDictionary<string, string> Claims { get; set; }
        /// <summary>
        /// 时间间隔
        /// 默认为（7天）
        /// </summary>
        public int Interval { get; set; } = 7;
    }
}
