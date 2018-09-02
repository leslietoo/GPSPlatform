using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4.Models
{
    public class GPS_EntityBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Token { get; set; }

        public string ClientIp { get; set; }

        public string ClaimsJson { get; set; }

        public string Remark { get; set; }
    }
}
