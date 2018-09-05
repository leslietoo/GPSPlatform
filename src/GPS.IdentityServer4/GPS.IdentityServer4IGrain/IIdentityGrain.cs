using GPS.IdentityServer4IGrain.Dtos;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.IdentityServer4IGrain
{
    public interface IIdentityGrain: IGrainWithGuidKey
    {
        Task<string> GenerateToken(JwtOptionsParameterDto jwtOptionsDto);
        Task<JwtOptionsResultDto> VerifyToken(string token);
        Task<RefreshTokenResultDto> RefreshToken(string token);
    }
}
