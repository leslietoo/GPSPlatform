using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GPS.IdentityServer4.Dtos;
using GPS.IdentityServer4.Dtos.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GPS.IdentityServer4.Controllers
{
    /// <summary>
    /// Identity
    /// </summary>
    [Route("Identity")]
    public class IdentityController : IdentityControllerBase
    {

        private readonly SymmetricSecurityKey symmetricSecurityKey;

        public IdentityController(IConfiguration configuration)
        {
            string key = configuration.GetValue<string>("SECRET_KEY");
            if (key.Length < 16)
            {
                throw new NotSupportedException("加密至少要16字符");
            }
            symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtOptionsDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerateToken")]
        public string GenerateToken([FromBody] JwtOptionsParameterDto jwtOptionsDto)
        {
            jwtOptionsDto = new JwtOptionsParameterDto();
            //DateTime expires = DateTime.UtcNow.AddDays(jwtOptionsDto.Interval);
            DateTime expires = DateTime.UtcNow.AddSeconds(30);
            //jwtOptionsDto.Issuer = "smallchi";
            //jwtOptionsDto.Audience = "Web";
            //jwtOptionsDto.ValidateAudience = true;
            //jwtOptionsDto.ValidateIssuer = true;
            jwtOptionsDto.Claims = new Dictionary<string,string>
            {
                { "interval",jwtOptionsDto.Interval.ToString()},
                { "username","123456" },
                { "password","123456" },
                { "email","123456@qq.com" },
                { "userid",Guid.NewGuid().ToString("N") },
            };
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                //ValidateAudience = jwtOptionsDto.ValidateAudience,
                //ValidAudience = jwtOptionsDto.Audience,
                //ValidateIssuer = jwtOptionsDto.ValidateIssuer,
                //ValidIssuer = jwtOptionsDto.Issuer,
                ValidateLifetime = true,
                RequireExpirationTime = true
            };
            var jwtPayload = new JwtPayload("xiaochi", "xiaochi", jwtOptionsDto.Claims.Select(s => new Claim(s.Key, s.Value)).ToArray(), DateTime.UtcNow, expires);
            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)), jwtPayload);
            try
            {
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
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
        public JwtOptionsResultDto VerifyToken([FromBody] string token)
        {
            // 正确token
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IjEyMzQ1NiIsInBhc3N3b3JkIjoiMTIzNDU2IiwiZW1haWwiOiIxMjM0NTZAcXEuY29tIiwidXNlcmlkIjoiMTg0ZmQyN2U4MmZiNGY5NmEwYzUxZmY5YWY4MTA2MzMiLCJuYmYiOjE1MzU4MDUwNTEsImV4cCI6MTUzNTg5MTQ0NywiaXNzIjoic21hbGxjaGkiLCJhdWQiOiJXZWIifQ.LDlDQ7ZcZUonJtE9pJcA8c1w7ZexU3rbD9vJtFTgrfU";
            // 篡改token
            //token = "eyJhbGciOfdddfdfIsInR5cCI6IkpXVCJ9.eyJ1c2VybmsdfdsffnBhc3N3b3JkIjoiMTIzNDU2IiwiZW1haWwiOiIxMjM0NTZAcXEuY29tIiwidXNlcmlkIjoiMTg0ZmQyN2U4MmZiNGY5NmEwYzUxZmY5YWY4MTA2MzMiLCJuYmYiOjE1MzU4MDUwNTEsImV4cCI6MTUzNTg5MTQ0NywiaXNzIjoic21hbGxjaGkiLCJhdWQiOiJXZWIifQ.LDlDQ7ZcZUonJtE9pJcA8c1w7ZexU3rbD";
            // 验证过期时间30s
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IjEyMzQ1NiIsInBhc3N3b3JkIjoiMTIzNDU2IiwiZW1haWwiOiIxMjM0NTZAcXEuY29tIiwidXNlcmlkIjoiMGFlZTcyNTA2MDI1NDU0ZTgzM2Q2MTkwYTcyMTViYmQiLCJuYmYiOjE1MzU4MDkxMDYsImV4cCI6MTUzNTgwOTEzNiwiaXNzIjoic21hbGxjaGkiLCJhdWQiOiJXZWIifQ.jMURs7gc3xeOPmvuUmAYeDFxDvQrmiI_d3ZxEApFJ8k";
            //
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IjEyMzQ1NiIsInBhc3N3b3JkIjoiMTIzNDU2IiwiZW1haWwiOiIxMjM0NTZAcXEuY29tIiwidXNlcmlkIjoiMDJjZGM4ZjNjODcwNDI0YWE5MWY1OTQ4YjhiODFlMjciLCJuYmYiOjE1MzU4MTAwODMsImV4cCI6MTUzNTg5NjQ4MywiaXNzIjoieGlhb2NoaSIsImF1ZCI6InhpYW9jaGkifQ.oMC6qHhKzHqeS9f6KwquXZa-zFfdQFWWFWftaO77ZKo";
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                //ValidateAudience = true,
                ValidAudience = "xiaochi",
                //ValidateIssuer = true,
                ValidIssuer = "xiaochi",
                ValidateLifetime = true,
                RequireExpirationTime=true,
            };
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
            return jwtOptionsResultDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RefreshToken")]
        public RefreshTokenResultDto RefreshToken([FromBody] string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                //ValidateAudience = true,
                ValidAudience = "xiaochi",
                //ValidateIssuer = true,
                ValidIssuer = "xiaochi",
                ValidateLifetime = true,
                RequireExpirationTime = true,
            };
            RefreshTokenResultDto refreshTokenResultDto = new RefreshTokenResultDto();
            try
            {
                var jwtSecurity2 = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
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
                var jwtPayload = new JwtPayload("xiaochi", "xiaochi", claims, DateTime.UtcNow, expires);
                var jwtSecurityToken = new JwtSecurityToken(new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)), jwtPayload);
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                refreshTokenResultDto.ResultCode = JwtResultCode.Ok;
                refreshTokenResultDto.Token = jwtToken;
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
            return refreshTokenResultDto;
        }
    }
}
