using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace API.Helper
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
            (hostingContext, loggerConfiguration) =>
            {
                var env = hostingContext.HostingEnvironment;
                loggerConfiguration.MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName",env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .Enrich.WithExceptionDetails()
                    .MinimumLevel.Override("Default", LogEventLevel.Information)
                    .MinimumLevel.Override("System", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .WriteTo.Console();

                if (hostingContext.HostingEnvironment.IsDevelopment())
                {
                    loggerConfiguration.MinimumLevel.Override("Application", LogEventLevel.Debug);
                }

                var elasticUrl = hostingContext.Configuration.GetValue<string>("Logging:ElasticUrl");
                if (!string.IsNullOrEmpty(elasticUrl))
                {
                    loggerConfiguration.WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(elasticUrl))
                        {
                            AutoRegisterTemplate = true,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                            IndexFormat = "pakshya-logs-{0:yyyy.MM.dd}",
                            MinimumLogEventLevel = LogEventLevel.Debug
                        });
                }
            };
        
    }
}
