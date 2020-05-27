using System;
using System.Data;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;

namespace AzureSqlFunctionJobAgent
{
    public static class FunctionEvery1Minute
    {
        /// <summary>
        /// Runs every minute, or whatever CHRON interval is declared in the Environment Variable %FUNCTION_EVERY1MINUTE_TIMER%
        /// Look in launchSettings.json, or in the project properties\Debug\Environment Variables
        /// When deployed to prod, in the Configuration blade of your Function App, set up the same environment variables there with your production values
        /// </summary>
        /// <param name="myTimer"></param>
        [FunctionName("FunctionEvery1Minute")]
        public static void Run([TimerTrigger("%FUNCTION_EVERY1MINUTE_TIMER%")]TimerInfo myTimer)
        {
            var log = Bootstrapper.Log;
            log.Information("FunctionEvery1Minute Executing");
            try
            {
                var connStr = Bootstrapper.ConnectionString();
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    conn.Execute("dbo.usp_Every1Minute", commandTimeout: 57, commandType: CommandType.StoredProcedure);
                    //Note! the command timeout of 57 seconds will preclude kicking off the same long running job concurrently.
                    //  However, if you need longer runtimes AND protection against concurrent execution then you may need to do something like 
                    //  use sp_getapplock within your target proc in Sql Server
                    //   https://docs.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-getapplock-transact-sql?view=sql-server-ver15

                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "FunctionEvery1Minute: Error while attempting to execute stored proc dbo.usp_Every1Minute");
                throw;
            }
        }
    }
}
