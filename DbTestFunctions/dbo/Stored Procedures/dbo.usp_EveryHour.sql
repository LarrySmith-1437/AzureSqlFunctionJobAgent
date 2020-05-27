CREATE PROCEDURE [dbo].usp_EveryHour
AS
    insert into dbo.JobLog(JobName)
    values ('usp_EveryHour')
