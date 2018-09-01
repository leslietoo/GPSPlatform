using GPS.IdentityServer4.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4.Dtos
{
    public class JwtOptionsResultDto
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public JwtResultCode ResultCode { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public IDictionary<string, string> Claims { get; set; }
    }
}
