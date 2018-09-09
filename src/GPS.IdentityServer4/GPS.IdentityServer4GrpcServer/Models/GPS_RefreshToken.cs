using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4GrpcServer.Models
{
    public class GPS_RefreshToken: GPS_EntityBase
    {
        public string OldClaimsJson { get; set; }

        public string OldToken { get; set; }
    }
}
