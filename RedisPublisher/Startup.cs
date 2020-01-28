using System;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Polly;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Outcomes;
using Polly.Extensions.Http;
using PollyResilience.Service;
using StackExchange.Redis;

namespace RedisPublisher
{
    public class Startup
    {
        protected static string baseDir = Directory.GetCurrentDirectory();

        protected IConfigurationRoot _configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(baseDir);
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }


        public ServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);

            services.AddSingleton<IPublisherConfiguration, PublisherConfiguration>();

            services.AddSingleton<IRedisClient, RedisClient>();

            services.AddTransient<IPollyResilienceService, PollyResilienceService>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog(_configuration["NLogConfig"]);
            });

            services.AddTransient<ConsoleApp>();

            var logger = services.BuildServiceProvider().GetService<ILogger<ConsoleApp>>();

            var retryPolicy = Policy.Handle<RedisConnectionException>()
                .Or<SocketException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(1), (exception, timeSpan, retryCount, context) =>
                {
                    logger.Log(LogLevel.Error, $"Publisher error on retry {retryCount} for {context.PolicyKey}", exception);
                });

            var fault = new SocketException(errorCode: 10013);

            double faultRate = 0;
            
            Double.TryParse(_configuration["FaultRate"], out faultRate);
            
            var chaosPolicy = MonkeyPolicy.InjectExceptionAsync(with =>
                with.Fault(fault)
                    .InjectionRate(faultRate)
                    .Enabled()
                );

            var policy = retryPolicy.WrapAsync(chaosPolicy);

            services.AddSingleton<IAsyncPolicy>(policy);

            return services.BuildServiceProvider();
        }
    }
}
