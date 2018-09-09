using GPS.GrpcServiceBase.Extensions;
using GPS.IdentityServer4GrpcServiceBase;
using Grpc.Core;
using System;
using System.Security.Claims;
using Xunit;

namespace GPS.IdentityServer4GrpcClient
{
    public class UnitTest1
    {


        IdentityServer4ServiceGrpc.IdentityServer4ServiceGrpcClient identityServer4ServiceGrpcClient;


        public UnitTest1()
        {
            Grpc.Core.Channel channel = new Grpc.Core.Channel("127.0.0.1", 15500, ChannelCredentials.Insecure);
            identityServer4ServiceGrpcClient = new IdentityServer4ServiceGrpc.IdentityServer4ServiceGrpcClient(channel);
        }

        [Fact]
        public void GenerateTokenTest1()
        {
            var request = new GenerateTokenRequest();
            request.Claims.Add("username", "123456");
            request.Claims.Add("password", "123456");
            request.Claims.Add("email", "123456@qq.com");
            request.Claims.Add("userid", "afashjkf");
            var result=identityServer4ServiceGrpcClient.GenerateToken(request);
            if (result.ResultReply.IsSuccess())
            {
                string token = result.Token;
            }
        }

        [Fact]
        public void VerifyTokenTest1()
        {
            var result = identityServer4ServiceGrpcClient.VerifyToken(new VerifyTokenRequest
            {
                Token= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IjEyMzQ1NiIsInBhc3N3b3JkIjoiMTIzNDU2IiwiZW1haWwiOiIxMjM0NTZAcXEuY29tIiwidXNlcmlkIjoiYWZhc2hqa2YiLCJpbnRlcnZhbCI6IjciLCJuYmYiOjE1MzY0OTk1ODAsImV4cCI6MTUzNzEwNDM4MCwiaXNzIjoic21hbGxjaGkiLCJhdWQiOiJzbWFsbGNoaSJ9.-uOzW0oYCWl6m5zzHyqUOO9v4FtLwd8brUIpM0w1Dzc"
            });
            if (result.ResultReply.IsSuccess())
            {
                Assert.Equal(JwtResultCode.Ok, result.ResultCode);
                Assert.Equal("123456", result.Claims["username"]);
                Assert.Equal("123456", result.Claims["password"]);
                Assert.Equal("123456@qq.com", result.Claims[ClaimTypes.Email]);
                Assert.Equal("afashjkf", result.Claims["userid"]);
                Assert.Equal("7", result.Claims["interval"]);
            }

        }

        [Fact]
        public void RefreshTokenTest1()
        {
            var result = identityServer4ServiceGrpcClient.RefreshToken(new RefreshTokenRequest
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IjEyMzQ1NiIsInBhc3N3b3JkIjoiMTIzNDU2IiwiZW1haWwiOiIxMjM0NTZAcXEuY29tIiwidXNlcmlkIjoiYWZhc2hqa2YiLCJpbnRlcnZhbCI6IjciLCJuYmYiOjE1MzY0OTk1ODAsImV4cCI6MTUzNzEwNDM4MCwiaXNzIjoic21hbGxjaGkiLCJhdWQiOiJzbWFsbGNoaSJ9.-uOzW0oYCWl6m5zzHyqUOO9v4FtLwd8brUIpM0w1Dzc"
            });
            if (result.ResultReply.IsSuccess())
            {
                Assert.Equal(JwtResultCode.Ok, result.ResultCode);
            }
        }
    }
}
