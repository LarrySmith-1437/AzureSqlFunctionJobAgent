using System;
using Microsoft.Azure.Storage;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace AzureSqlFunctionJobAgent
{
    /// <summary>
    /// Using this class as a central way to read in config from env variables and kickstart things like Serilog logging
    /// </summary>
    public static class Bootstrapper
    {
        public static IConfigurationRoot Config;
        public static ILogger Log;

        static Bootstrapper()
        {
            Config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            Log = InitializeLogger(Config);
        }

        public static string ConnectionString()
        {
            var connStr = Config["DatabaseConnectionString"];

            if (string.IsNullOrEmpty(connStr))
            {
                throw new ApplicationException("No database connection string configured. Add environment variable 'DatabaseConnectionString' ");
            }

            return connStr;
        }

        private static Logger InitializeLogger(IConfigurationRoot config)
        {

            //var logLevelSwitch = new LoggingLevelSwitch(initialMinimumLevel:LogEventLevel.Debug);
            var logEventLevel = GetLoggingLevel(config["Logging:LogLevel"]);
            var logAzurePeriod = new TimeSpan(0, 0, 0,
                Convert.ToInt32(config["Logging:AzureStorageAccount:PeriodSeconds"]));
            var logToBlobStorageAccount =
                CloudStorageAccount.Parse(config["Logging:AzureStorageAccount:ConnectionString"]);
            var logToBlobContainer = config["Logging:AzureStorageAccount:Container"];
            var logToBlobFilename = config["Logging:AzureStorageAccount:Filename"];
            var logToBlobBatchPostLimit =
                Convert.ToInt32(config["Logging:AzureStorageAccount:batchPostingLimit"]);
            var logOutputTemplate = config["Logging:OutputTemplate"];

            Logger log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.AzureBlobStorage(
                    logToBlobStorageAccount,
                    logEventLevel,
                    storageContainerName: logToBlobContainer,
                    storageFileName: logToBlobFilename,
                    period: logAzurePeriod,
                    writeInBatches: true,
                    batchPostingLimit: logToBlobBatchPostLimit,
                    outputTemplate: logOutputTemplate
                )
                .CreateLogger();
            return log;
        }

        public static LogEventLevel GetLoggingLevel(string logEventLevel)
        {
            //https://github.com/serilog/serilog/wiki/Configuration-Basics
            switch (logEventLevel.ToLower())
            {
                case "verbose":
                        return LogEventLevel.Verbose;
                case "debug":
                        return LogEventLevel.Debug;
                case "information":
                        return  LogEventLevel.Information;
                case "warning":
                        return  LogEventLevel.Warning;
                case "error":
                        return  LogEventLevel.Error;
                case "fatal":
                        return  LogEventLevel.Fatal;
                default:
                    return LogEventLevel.Debug;
            }
        }
    }
}
