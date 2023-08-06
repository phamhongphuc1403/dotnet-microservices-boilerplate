using Microsoft.Extensions.Configuration;
using Serilog;

namespace TinyCRM.Infrastructure.Logger
{
    public static class LoggerService
    {
        public static void ConfigureLogger(ConfigurationManager configurations)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurations)
                .CreateLogger();
        }

        public static void LogError(string message)
        {
            Log.Error(message);
        }
    }
}