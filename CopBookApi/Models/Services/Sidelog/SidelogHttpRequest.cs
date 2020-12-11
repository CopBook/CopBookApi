using Microsoft.AspNetCore.Http;

namespace CopBookApi.Models.Services.Sidelog
{
    public class SidelogHttpRequest
    {
        public string Message { get; }
        public SidelogHttpRequestJson Json { get; }
        public string Level { get; }

        public SidelogHttpRequest(string level, string message, object json, HttpContext context)
        {
            Message = message;
            Json = new SidelogHttpRequestJson(json, context);
            Level = level;
        }
    }
}