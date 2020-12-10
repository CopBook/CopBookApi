using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CopBookApi.Interfaces.Services.Logging;
using CopBookApi.Models.Services.Sidelog;
using Microsoft.Extensions.Logging;

namespace CopBookApi.Services.Sidelog
{
    public class SidelogService : ILoggingService
    {
        private readonly SidelogServiceConfig config;
        private HttpClient http;
        private Dictionary<LogLevel, string> ApiLogLevelNames;
        public SidelogService(SidelogServiceConfig config, IHttpClientFactory httpFactory)
        {
            this.config = config;
            http = httpFactory.CreateClient();
            ApiLogLevelNames = new Dictionary<LogLevel, string>();
            ApiLogLevelNames.Add(LogLevel.Trace, "trace");
            ApiLogLevelNames.Add(LogLevel.Debug, "debug");
            ApiLogLevelNames.Add(LogLevel.Information, "info");
            ApiLogLevelNames.Add(LogLevel.Warning, "warn");
            ApiLogLevelNames.Add(LogLevel.Error, "error");
            ApiLogLevelNames.Add(LogLevel.Critical, "fatal");
        }

        public void Log(LogLevel level, string message, object logObject = null)
        {
            if (level >= config.LogLevel)
            {
                SendLog(new SidelogHttpRequest(
                    ApiLogLevelNames.GetValueOrDefault(level),
                    message,
                    logObject
                ));
            }
        }

        private async void SendLog(SidelogHttpRequest request)
        {
            try
            {
                string stringifiedPayload = JSON.Stringify(request);
                var httpRequest = new HttpRequestMessage
                {
                    Content = new StringContent(stringifiedPayload, Encoding.UTF8, "application/json"),
                    Headers =
                    {
                        { "clientId", config.ApiKey }
                    },
                    RequestUri = new Uri(config.Endpoint),
                    Method = HttpMethod.Post
                };
                await http.SendAsync(httpRequest);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}