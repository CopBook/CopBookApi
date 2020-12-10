namespace CopBookApi.Models.Services.Sidelog
{
    public class SidelogHttpRequest
    {
        public string Message { get; }
        public object Json { get; }
        public string Level { get; }

        public SidelogHttpRequest(string level, string message, object json)
        {
            Message = message;
            Json = json;
            Level = level;
        }
    }
}