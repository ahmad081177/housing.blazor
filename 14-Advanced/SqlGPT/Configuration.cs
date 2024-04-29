using System;

using System.Text.Json;

namespace SqlGPT
{
    public class OpenAIServiceOptions
    {
        public string? ApiKey { get; set; }
    }
    public class AppConfig
    {
        public OpenAIServiceOptions? OpenAIServiceOptions { get; set; }
    }

    internal class Configuration
    {
        internal static string? ReadAPIKey()
        {
            var c = SqlGPTUtils.ReadFromConfig<AppConfig>("secrets.json");
            string? key = c?.OpenAIServiceOptions?.ApiKey;
            return key;
        }
    }
}
