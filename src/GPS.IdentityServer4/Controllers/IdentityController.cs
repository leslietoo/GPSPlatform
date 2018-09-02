using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GPS.IdentityServer4.Configs;
using GPS.IdentityServer4.Dtos;
using GPS.IdentityServer4.Dtos.Enums;
using GPS.IdentityServer4.Models;
using GPS.IdentityServer4.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using GPS.IdentityServer4.Extensions;

namespace GPS.IdentityServer4.Controllers
{
    /// <summary>
    /// Identity
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("Identity")]
    public class IdentityController : ControllerBase
    {

        private readonly SymmetricSecurityKey symmetricSecurityKey;

        private readonly JwtOptions jwtOptions;

        private readonly TokenValidationParameters tokenValidationParameters;

        private readonly GPSIdentityServerDbContext gPSIdentityServerDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="options"></param>
        public IdentityController(GPSIdentityServerDbContext dbContext, IOptions<JwtOptions> options)
        {
            gPSIdentityServerDbContext = dbContext;
            jwtOptions = options.Value;
            symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKEY));
            tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                //ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                //ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateLifetime = true,
                RequireExpirationTime = true,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtOptionsDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerateToken")]
        public async Task<string> GenerateToken([FromBody] JwtOptionsParameterDto jwtOptionsDto)
        {
            jwtOptionsDto = new JwtOptionsParameterDto();
            DateTime expires = DateTime.UtcNow.AddDays(jwtOptionsDto.Interval);
            jwtOptionsDto.Claims = new Dictionary<string,string>
            {
                { "interval",jwtOptionsDto.Interval.ToString()},
                { "username","123456" },
                { "password","123456" },
                { "email","123456@qq.com" },
                { "userid",Guid.NewGuid().ToString("N") },
            };
            var jwtPayload = new JwtPayload(jwtOptions.Issuer,jwtOptions.Audience, jwtOptionsDto.Claims.Select(s => new Claim(s.Key, s.Value)), DateTime.UtcNow, expires);
            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)), jwtPayload);
            try
            {
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                await gPSIdentityServerDbContext.AddAsync(new GPS_Token
                {
                    Token = jwtToken,
                    ClaimsJson = JsonConvert.SerializeObject(jwtOptionsDto.Claims),
                    ClientIp= Request.GetIp()
                });
                await gPSIdentityServerDbContext.SaveChangesAsync();
                return jwtToken;
            }
            catch (Exception ex)
            {
                return "";
            }  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        [HttpPost]
        [Route("VerifyToken")]
        public async Task<JwtOptionsResultDto> VerifyToken([FromBody] string token)
        {
            JwtOptionsResultDto jwtOptionsResultDto = new JwtOptionsResultDto();
            try
            {
                var jwtSecurity2 = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
                jwtOptionsResultDto.ResultCode = JwtResultCode.Ok;
                jwtOptionsResultDto.Claims = jwtSecurity2.Claims.ToDictionary(key => key.Type, value => value.Value);
            }
            catch(Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException ex)
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
            await gPSIdentityServerDbContext.AddAsync(new GPS_VerifyToken
            {
                Token = token,
                ClaimsJson = JsonConvert.SerializeObject(jwtOptionsResultDto.Claims),
                ClientIp = Request.GetIp(),
                ResultCode= (int)jwtOptionsResultDto.ResultCode
            });
            await gPSIdentityServerDbContext.SaveChangesAsync();
            return jwtOptionsResultDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<RefreshTokenResultDto> RefreshToken([FromBody] string token)
        {
            RefreshTokenResultDto refreshTokenResultDto = new RefreshTokenResultDto();
            GPS_RefreshToken gPS_RefreshToken = new GPS_RefreshToken();
            gPS_RefreshToken.ClientIp = Request.GetIp();
            gPS_RefreshToken.OldToken = token;
            try
            {
                var jwtSecurity2 = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
                gPS_RefreshToken.OldClaimsJson = JsonConvert.SerializeObject(jwtSecurity2.Claims.ToDictionary(key => key.Type, value => value.Value));
                List<Claim> claims = new List<Claim>();
                DateTime expires = DateTime.UtcNow.AddDays(7);
                foreach (var claim in jwtSecurity2.Claims)
                {
                    if (claim.Type == "nbf" || claim.Type == "exp"|| claim.Type == "aud") continue;
                    if (claim.Type == "interval")
                    {
                        expires = DateTime.UtcNow.AddDays(int.Parse(claim.Value));
                    }
                    if(claims.Exists(e=>e.Type== claim.Type)) continue;
                    claims.Add(claim);
                }
                gPS_RefreshToken.ClaimsJson = JsonConvert.SerializeObject(claims.ToDictionary(key => key.Type, value => value.Value));
                var jwtPayload = new JwtPayload(jwtOptions.Issuer, jwtOptions.Audience, claims, DateTime.UtcNow, expires);
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
            await gPSIdentityServerDbContext.AddAsync(gPS_RefreshToken);
            await gPSIdentityServerDbContext.SaveChangesAsync();
            return refreshTokenResultDto;
        }
    }
}
