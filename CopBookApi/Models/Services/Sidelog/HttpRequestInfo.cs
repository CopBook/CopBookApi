using Microsoft.AspNetCore.Http;

namespace CopBookApi.Models.Services.Sidelog
{
    public class HttpRequestInfo
    {
        public string UserId { get; }
        public string ClientId { get; }
        public string TransactionId { get; }

        public HttpRequestInfo(HttpContext context)
        {
            UserId = context.User?.FindFirst("user_id")?.Value;
            ClientId = context.Request.Headers["clientId"];
            TransactionId = context.Request.Headers["transactionId"];
        }
    }
}