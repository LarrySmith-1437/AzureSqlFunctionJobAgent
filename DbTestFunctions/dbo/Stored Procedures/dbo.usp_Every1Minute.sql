CREATE PROCEDURE [dbo].[usp_Every1Minute]
AS
begin
	exec dbo.MutexLock 'Every1Minute'

    insert into dbo.JobLog(JobName)
    values ('usp_Every1Minute')

    exec dbo.MutexUnlock 'Every1Minute'
end