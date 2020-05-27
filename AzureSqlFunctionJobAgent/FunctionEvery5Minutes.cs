using System;
using System.Data;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;

namespace AzureSqlFunctionJobAgent
{
    public static class FunctionEvery5Minutes
    {
        [FunctionName("FunctionEvery5Minutes")]
        public static void Run([TimerTrigger("%FUNCTION_EVERY5MINUTE_TIMER%")]TimerInfo myTimer)
        {
            var log = Bootstrapper.Log;
            log.Information("FunctionEvery5Minutes Executing");
            try
            {
                var connStr = Bootstrapper.ConnectionString();
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    conn.Execute("dbo.usp_Every5Minutes", commandTimeout: 57, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "FunctionEvery5Minutes: Error while attempting to execute stored proc dbo.usp_Every5Minutes");
                throw;
            }
        }
    }
}
