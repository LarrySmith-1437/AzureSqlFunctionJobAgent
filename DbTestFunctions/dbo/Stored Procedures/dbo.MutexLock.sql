CREATE procedure [dbo].[MutexLock]
(
	@ResourceName nvarchar(255)
)
as
begin

	DECLARE @res INT
	EXEC @res = sp_getapplock                  
					@Resource = @ResourceName,
					@LockMode = 'Exclusive',
					@LockOwner = 'Session',
					@LockTimeout = 2147483647, --essentially, wait for 596 hours
					@DbPrincipal = 'public'
	-- 0 and 1 are valid return values
	IF @res NOT IN (0, 1)
	BEGIN		
		declare @message varchar(4000)
		set @message =  'Unable to acquire mutex lock for resource:  ' + @ResourceName ;
		RAISERROR ( @message, 16, 1 )
	END 

end