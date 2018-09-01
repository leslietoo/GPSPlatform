using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4.Dtos.Enums
{
    /// <summary>
    /// 返回状态
    /// </summary>
    public enum JwtResultCode : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Ok = 0,
        /// <summary>
        /// token过期
        /// </summary>
        TokenExpires = 1,
        /// <summary>
        /// token签名错误
        /// </summary>
        TokenInvalidSignature = 2,
        /// <summary>
        /// 内部错误
        /// </summary>
        Error = 9
    }
}
