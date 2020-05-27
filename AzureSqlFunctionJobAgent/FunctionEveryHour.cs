using System;
using System.Data;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;

namespace AzureSqlFunctionJobAgent
{
    public static class FunctionEveryHour
    {
        [FunctionName("FunctionEveryHour")]
        public static void Run([TimerTrigger("%FUNCTION_EVERYHOUR_TIMER%")]TimerInfo myTimer)
        {
            var log = Bootstrapper.Log;
            log.Information("FunctionEveryHour Executing");
            try
            {
                var connStr = Bootstrapper.ConnectionString();
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    conn.Execute("dbo.usp_EveryHour", commandTimeout: 57, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "FunctionEveryHour: Error while attempting to execute stored proc dbo.usp_EveryHour");
                throw;
            }
        }
    }
}
