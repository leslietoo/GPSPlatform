using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4.Configs
{
    public class JwtOptions
    {
        /// <summary>
        /// 加密
        /// 至少要16字符
        /// </summary>
        public string SecretKEY { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
