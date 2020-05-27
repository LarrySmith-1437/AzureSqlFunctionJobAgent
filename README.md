# AzureSqlFunctionJobAgent
Replacing Azure Sql Elastic Jobs with scheduled Azure Functions

Since MS has been years late in completing Elastic Jobs for Azure Sql, there was a need to have more control over the scheduled execution of sql code.  This way, you can add your own custom code, preferred logging, and connection string management, or anything else you need above and beyond the limitations that Elastic Jobs imposes.

Further, I've had Elastic jobs get hung-up in production, requiring Microsoft intervention to get the jobs running again, which was a disaster and an outage.  Again, a solution like this will give you back the control you need in deploying or restarting jobs.

Consider this as template/sample code you can use as the basis for your own solution.

And hey, this is an on-ramp to having jobs that you can kick off via WebHook or any other method of job start that Azure Functions gives you.

I used these 3rd party libraries:
Dapper, 
Serilog

Configuration is via Environment Variables, which is preffered and more secure than leaving config files laying around.  You can change this to go back to config files if you want.

Host this in a paid-for App Service Plan if you intend to host long-running jobs, else your serverless-compute will cut your jobs short on time.
