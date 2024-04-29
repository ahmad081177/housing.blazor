using Microsoft.Extensions.Configuration;

namespace HousingWebApp.Services
{
    public class SQLGPTService
    {
        private readonly IConfiguration configuration;
        public SQLGPTService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private string? GetGKey()
        {
            var section = configuration.GetSection("AppSettings");
            if (section != null)
            {
                return section["OpenAI:ApiKey"];
            }
            return null;
        }
    }
}
