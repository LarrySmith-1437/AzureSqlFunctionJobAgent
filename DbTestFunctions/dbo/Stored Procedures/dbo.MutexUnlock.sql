create procedure [dbo].[MutexUnlock]
(
	@ResourceName nvarchar(255)
)
as
begin

	begin try
		EXEC  sp_releaseapplock 
         @Resource = @ResourceName,
         @DbPrincipal = 'public',
         @LockOwner = 'Session'        

	end try
	begin catch
	end catch
end