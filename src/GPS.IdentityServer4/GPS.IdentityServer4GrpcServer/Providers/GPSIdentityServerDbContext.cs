using GPS.IdentityServer4GrpcServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPS.IdentityServer4GrpcServer.Providers
{
    /// <summary>
    /// 
    /// Add-Migration Init
    /// Update-Database
    /// </summary>
    public class GPSIdentityServerDbContext:DbContext
    {
        public DbSet<GPS_Token> GPS_Token { get; set; }

        public DbSet<GPS_RefreshToken> GPS_RefreshToken { get; set; }

        public DbSet<GPS_VerifyToken> GPS_VerifyToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//../GPS.IdentityServer4GrainServer
            optionsBuilder.UseSqlite("Data Source=../GPS.IdentityServer4GrainServer/data/datagps_identity_server.db");
            // 测试使用内存数据库
            //optionsBuilder.UseInMemoryDatabase("datagps_identity_server");
        }
    }
}
