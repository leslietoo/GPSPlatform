using GPS.GrpcServiceBase.Extensions;
using GPS.IdentityServer4GrpcServer.Configs;
using GPS.IdentityServer4GrpcServiceBase;
using Grpc.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace GPS.IdentityServer4GrpcServer
{
    public class IdentityServer4ServiceImpl: GPS.IdentityServer4GrpcServiceBase.IdentityServer4ServiceGrpc.IdentityServer4ServiceGrpcBase
    {
        private readonly IServiceProvider ServiceProvider;

        private readonly SymmetricSecurityKey symmetricSecurityKey;

        private readonly IOptionsMonitor<JwtOptions> jwtOptionsMonitor;

        private readonly TokenValidationParameters tokenValidationParameters;
        /// <summary>
        /// 默认7天
        /// </summary>
        private const int DefaultInterval = 7;

        public IdentityServer4ServiceImpl(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            jwtOptionsMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<JwtOptions>>();
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

        public override Task<GenerateTokenReply> GenerateToken(GenerateTokenRequest request, ServerCallContext context)
        {
            GenerateTokenReply generateTokenReply = new GenerateTokenReply();
            request.Interval = request.Interval > 0 ? request.Interval : DefaultInterval;
            DateTime expires = DateTime.UtcNow.AddDays(request.Interval);
            request.Claims.Add("interval", request.Interval.ToString());
            var jwtPayload = new JwtPayload(jwtOptionsMonitor.CurrentValue.Issuer, jwtOptionsMonitor.CurrentValue.Audience, request.Claims.Select(s => new Claim(s.Key, s.Value)), DateTime.UtcNow, expires);
            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)), jwtPayload);
            try
            {
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                //await gPSIdentityServerDbContext.AddAsync(new GPS_Token
                //{
                //    Token = jwtToken,
                //    ClaimsJson = JsonConvert.SerializeObject(jwtOptionsDto.Claims),
                //});
                //await gPSIdentityServerDbContext.SaveChangesAsync();
                generateTokenReply.Token = jwtToken;
                generateTokenReply.ResultReply = ResultReplyExtensions.Success();
            }
            catch (Exception ex)
            {
                generateTokenReply.ResultReply = ResultReplyExtensions.InnerError();
            }
            return Task.FromResult(generateTokenReply);
        }

        public override Task<VerifyTokenReply> VerifyToken(VerifyTokenRequest request, ServerCallContext context)
        {
            VerifyTokenReply verifyTokenReply = new VerifyTokenReply();
            try
            {
                var jwtSecurity2 = new JwtSecurityTokenHandler().ValidateToken(request.Token, tokenValidationParameters, out var securityToken);
                verifyTokenReply.ResultCode = JwtResultCode.Ok;
                verifyTokenReply.ResultReply= ResultReplyExtensions.Success();
                foreach (var claim in jwtSecurity2.Claims)
                {
                    verifyTokenReply.Claims.Add(claim.Type, claim.Value);
                }
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException ex)
            {
                verifyTokenReply.ResultReply = ResultReplyExtensions.Success();
                verifyTokenReply.ResultCode = JwtResultCode.TokenInvalidSignature;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenNoExpirationException ex)
            {
                verifyTokenReply.ResultReply = ResultReplyExtensions.Success();
                verifyTokenReply.ResultCode = JwtResultCode.TokenExpires;
            }
            catch (Exception ex)
            {
                verifyTokenReply.ResultReply = ResultReplyExtensions.InnerError();
                verifyTokenReply.ResultCode = JwtResultCode.Error;
            }
            //await gPSIdentityServerDbContext.AddAsync(new GPS_VerifyToken
            //{
            //    Token = token,
            //    ClaimsJson = JsonConvert.SerializeObject(jwtOptionsResultDto.Claims),
            //    ResultCode = (int)jwtOptionsResultDto.ResultCode
            //});
            //await gPSIdentityServerDbContext.SaveChangesAsync();
            return Task.FromResult(verifyTokenReply);
        }

        public override Task<RefreshTokenReply> RefreshToken(RefreshTokenRequest request, ServerCallContext context)
        {
            RefreshTokenReply refreshTokenReply = new RefreshTokenReply();
            //GPS_RefreshToken gPS_RefreshToken = new GPS_RefreshToken();
            //gPS_RefreshToken.OldToken = token;
            try
            {
                var jwtSecurity2 = new JwtSecurityTokenHandler().ValidateToken(request.Token, tokenValidationParameters, out var securityToken);
                //gPS_RefreshToken.OldClaimsJson = JsonConvert.SerializeObject(jwtSecurity2.Claims.ToDictionary(key => key.Type, value => value.Value));
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
                //gPS_RefreshToken.ClaimsJson = JsonConvert.SerializeObject(claims.ToDictionary(key => key.Type, value => value.Value));
                var jwtPayload = new JwtPayload(jwtOptionsMonitor.CurrentValue.Issuer, jwtOptionsMonitor.CurrentValue.Audience, claims, DateTime.UtcNow, expires);
                var jwtSecurityToken = new JwtSecurityToken(new JwtHeader(new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)), jwtPayload);
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                refreshTokenReply.ResultReply = ResultReplyExtensions.Success();
                refreshTokenReply.ResultCode = JwtResultCode.Ok;
                refreshTokenReply.Token = jwtToken;
                //gPS_RefreshToken.Token = jwtToken;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException ex)
            {
                refreshTokenReply.ResultReply = ResultReplyExtensions.Success();
                refreshTokenReply.ResultCode = JwtResultCode.TokenInvalidSignature;
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenNoExpirationException ex)
            {
                refreshTokenReply.ResultReply = ResultReplyExtensions.Success();
                refreshTokenReply.ResultCode = JwtResultCode.TokenExpires;
            }
            catch (Exception ex)
            {
                refreshTokenReply.ResultReply = ResultReplyExtensions.InnerError();
                refreshTokenReply.ResultCode = JwtResultCode.Error;
            }
            //await gPSIdentityServerDbContext.AddAsync(gPS_RefreshToken);
            //await gPSIdentityServerDbContext.SaveChangesAsync();
            return Task.FromResult(refreshTokenReply);
        }
    }
}
