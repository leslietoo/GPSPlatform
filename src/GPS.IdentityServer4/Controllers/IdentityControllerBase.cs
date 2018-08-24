using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4.Controllers
{
    [ApiController]
    public class IdentityControllerBase : ControllerBase
    {
        protected static List<string> Tokens = new List<string>();
    }
}
