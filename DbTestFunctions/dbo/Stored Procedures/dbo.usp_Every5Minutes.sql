CREATE PROCEDURE dbo.usp_Every5Minutes
AS
    insert into dbo.JobLog(JobName)
    values ('usp_Every5Minutes')
