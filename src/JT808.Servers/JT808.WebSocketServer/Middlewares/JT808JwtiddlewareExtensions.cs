using Microsoft.AspNetCore.Builder;


namespace JT808.WebSocketServer.Middlewares
{
    public static class JT808JwtiddlewareExtensions
    {
        public static IApplicationBuilder UseJT808JwtVerify(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JT808Jwtiddleware>();
        }
    }
}