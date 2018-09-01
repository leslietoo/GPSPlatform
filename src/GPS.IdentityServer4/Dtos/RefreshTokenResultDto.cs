using GPS.IdentityServer4.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4.Dtos
{
    public class RefreshTokenResultDto
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public JwtResultCode ResultCode { get; set; }

        /// <summary>
        /// 返回token
        /// </summary>
        public string Token { get; set; }
    }
}
