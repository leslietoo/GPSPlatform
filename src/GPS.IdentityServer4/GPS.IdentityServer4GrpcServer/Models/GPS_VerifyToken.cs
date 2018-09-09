using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4GrpcServer.Models
{
    public class GPS_VerifyToken: GPS_EntityBase
    {
        public int ResultCode { get; set; }
    }
}
