using GPS.IdentityServer4Grain.Configs;
using GPS.IdentityServer4Grain.Models;
using GPS.IdentityServer4Grain.Providers;
using GPS.IdentityServer4IGrain;
using GPS.IdentityServer4IGrain.Dtos;
using GPS.IdentityServer4IGrain.Dtos.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GPS.IdentityServer4Grain
{
    public class IdentityGrainImpl : Orleans.Grain, IIdentityGrain
    {
        private readonly SymmetricSecurityKey symmetricSecurityKey;

        private readonly IOptionsMonitor<JwtOptions> jwtOptionsMonitor;

        private readonly TokenValidationParameters tokenValidationParameters;

        private readonly GPSIdentityServerDbContext gPSIdentityServerDbContext;

        public IdentityGrainImpl(GPSIdentityServerDbContext dbContext, IOptionsMonitor<JwtOptions> optionsMonitor)
        {
            gPSIdentityServerDbContext = dbContext;
            jwtOptionsMonitor = optionsMonitor;
            symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsMonitor.CurrentValue.SecretKEY));
            tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                //ValidateAudience = true,
                ValidAudience = jwtOptionsMonitor.CurrentValue.Audience,
                //ValidateIssuer = true,
                ValidIssuer = jwtOptionsMonitor.CurrentValue.Issuer,
                ValidateLifetime = true,
                RequireExpirationTime = true,
            };
        }

        public  Task<string> GenerateToken(JwtOptionsParameterDto jwtOptionsDto)
        {
            jwtOptionsDto = new JwtOptionsParameterDto();
            DateTime expires = DateTime.UtcNow.AddDays(jwtOptionsDto.Interval);
            jwtOptionsDto.Claims = new Dictionary<string, string>
            {
                { "interval",jwtOptionsDto.Interval.ToString()},
                { "username","123456" },
                { "password","123456" },
                { "email","123456@qq.com" },
                { "userid",Guid.NewGuid().ToString("N") },
            };
            var jwtPayload = new JwtPayload(jwtOptionsMonitor.CurrentValue.Issuer, jwtOptionsMonitor.CurrentValue.Audience, jwtOptionsDto.Claims.Select(s => new Claim(s.Key, s.Value)), DateTime.UtcNow, expires);
            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)), jwtPayload);
            try
            {
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                //await gPSIdentityServerDbContext.AddAsync(new GPS_Token
                //{
                //    Token = jwtToken,
                //    ClaimsJson = JsonConvert.SerializeObject(jwtOptionsDto.Claims),
                //    ClientIp = Request.GetIp()
                //});
                //await gPSIdentityServerDbContext.SaveChangesAsync();
                return Task.FromResult(jwtToken);
            }
            catch (Exception ex)
            {
                return Task.FromResult("");
            }
        }

        public Task<RefreshTokenResultDto> RefreshToken(string token)
        {
            RefreshTokenResultDto refreshTokenResultDto = new RefreshTokenResultDto();
            GPS_RefreshToken gPS_RefreshToken = new GPS_RefreshToken();
            //gPS_RefreshToken.ClientIp = Request.GetIp();
            gPS_RefreshToken.OldToken = token;
            try
            {
                var jwtSecurity2 = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
                gPS_RefreshToken.OldClaimsJson = JsonConvert.SerializeObject(jwtSecurity2.Claims.ToDictionary(key => key.Type, value => value.Value));
                List<Claim> claims = new List<Claim>();
                DateTime expires = DateTime.UtcNow.AddDays(7);
                foreach (var claim in jwtSecurity2.Claims)
                {
                    if (claim.Type == "nbf" || claim.Type == "exp" || claim.Type == "aud") continue;
                    if (claim.Type == "interval")
                    {
                        expires = DateTime.UtcNow.AddDays(int.Parse(claim.Value));
                    }
                    if (claims.Exists(e => e.Type == claim.Type)) continue;
                    claims.Add(claim);
                }
                gPS_RefreshToken.ClaimsJson = JsonConvert.SerializeObject(claims.ToDictionary(key => key.Type, value => value.Value));
                var jwtPayload = new JwtPayload(jwtOptionsMonitor.CurrentValue.Issuer, jwtOptionsMonitor.CurrentValue.Audience, claims, DateTime.UtcNow, expires);
                var jwtSecurityToken = new JwtSecurityToken(new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)), jwtPayload);
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                refreshTokenResultDto.ResultCode = JwtResultCode.Ok;
                refreshTokenResultDto.Token = jwtToken;
                gPS_RefreshToken.Token = jwtToken;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException ex)
            {
                refreshTokenResultDto.ResultCode = JwtResultCode.TokenInvalidSignature;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenNoExpirationException ex)
            {
                refreshTokenResultDto.ResultCode = JwtResultCode.TokenExpires;
            }
            catch (Exception ex)
            {
                refreshTokenResultDto.ResultCode = JwtResultCode.Error;
            }
            //await gPSIdentityServerDbContext.AddAsync(gPS_RefreshToken);
            //await gPSIdentityServerDbContext.SaveChangesAsync();
            return Task.FromResult(refreshTokenResultDto);
        }

        public Task<JwtOptionsResultDto> VerifyToken(string token)
        {
            JwtOptionsResultDto jwtOptionsResultDto = new JwtOptionsResultDto();
            try
            {
                var jwtSecurity2 = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
                jwtOptionsResultDto.ResultCode = JwtResultCode.Ok;
                jwtOptionsResultDto.Claims = jwtSecurity2.Claims.ToDictionary(key => key.Type, value => value.Value);
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException ex)
            {
                jwtOptionsResultDto.ResultCode = JwtResultCode.TokenInvalidSignature;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenNoExpirationException ex)
            {
                jwtOptionsResultDto.ResultCode = JwtResultCode.TokenExpires;
            }
            catch (Exception ex)
            {
                jwtOptionsResultDto.ResultCode = JwtResultCode.Error;
            }
            //await gPSIdentityServerDbContext.AddAsync(new GPS_VerifyToken
            //{
            //    Token = token,
            //    ClaimsJson = JsonConvert.SerializeObject(jwtOptionsResultDto.Claims),
            //    ClientIp = Request.GetIp(),
            //    ResultCode = (int)jwtOptionsResultDto.ResultCode
            //});
            //await gPSIdentityServerDbContext.SaveChangesAsync();
            return Task.FromResult(jwtOptionsResultDto);
        }
    }
}
