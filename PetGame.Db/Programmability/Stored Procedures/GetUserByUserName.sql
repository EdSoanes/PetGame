CREATE PROCEDURE [dbo].[GetUserByUserName]
	@userName nvarchar(200)
AS
	SELECT * FROM [dbo].[User] WHERE [UserName] = @userName

	SELECT a.* FROM [dbo].[Animal] a
	INNER JOIN [dbo].[User] u ON u.[UserId] = a.[UserId]
	WHERE u.[UserName] = @userName

