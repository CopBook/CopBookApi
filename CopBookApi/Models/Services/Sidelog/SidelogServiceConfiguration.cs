using Microsoft.Extensions.Logging;

namespace CopBookApi.Models.Services.Sidelog
{
    public class SidelogServiceConfig
    {
        public string Endpoint { get; set; }

        public string ApiKey { get; set; }

        public LogLevel LogLevel { get; set; }
    }
}