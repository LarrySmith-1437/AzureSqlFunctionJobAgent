# AzureSqlFunctionJobAgent
Replacing Azure Sql Elastic Jobs with scheduled Azure Functions

Since MS has been years late in completing Elastic Jobs for Azure Sql, there was a need to have more control over the execution of sql code.  This way, you can add your own preferred logging and connection string management, without the hoops that Elastic Jobs imposes.

Further, I've had Elastic jobs get hun-up in production, requiring Microsoft intervention to get the jobs running again, which was a disaster and an outage.  Again, a solution like this will give you back the control you need in deploying or restarting jobs.

Consider this as template/sample code you can use as the basis for your own solution.

I used:
Dapper
Serilog

Configuration is via Environment Variables, which is preffered and more secure than leaving config files laying around.  You can change this to go back to config files if you want.
