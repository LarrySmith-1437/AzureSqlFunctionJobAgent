CREATE TABLE [dbo].[JobLog]
(
    [Id] INT NOT NULL PRIMARY KEY identity(1,1),
    JobName varchar(50),
    ExecutionTime datetime constraint df_exectime default getdate()
)
