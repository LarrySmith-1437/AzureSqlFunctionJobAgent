CREATE PROCEDURE [dbo].[usp_Every1Minute]
AS
    insert into dbo.JobLog(JobName)
    values ('usp_Every1Minute')
