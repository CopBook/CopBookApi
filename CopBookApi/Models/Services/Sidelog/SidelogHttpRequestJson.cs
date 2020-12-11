using Microsoft.AspNetCore.Http;

namespace CopBookApi.Models.Services.Sidelog
{
    public class SidelogHttpRequestJson
    {
        public object ObjectFromLogStatement { get; }
        public HttpRequestInfo HttpRequestInfo { get; }

        public SidelogHttpRequestJson(object objectFromLogStatement, HttpContext context)
        {
            ObjectFromLogStatement = objectFromLogStatement;
            HttpRequestInfo = new HttpRequestInfo(context);
        }
    }
}