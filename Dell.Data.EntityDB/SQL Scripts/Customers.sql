USE [DellTehnicalTest]
GO

/****** Object:  StoredProcedure [dbo].[CustomersInsert]    Script Date: 11/20/2018 12:16:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:			Vlad
-- Description:		Insert stored procedure
-- =============================================
CREATE PROCEDURE [dbo].[CustomersInsert]

@age int,
@emailAddress varchar(50),
@name varchar(50),
@id int out,
@recordVersion timestamp out

AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @retVal int
	SET @retVal = 0;
	DECLARE @rvTmp table(ID int)

	INSERT INTO [Customers]([Name], [EmailAddress], [Age])
		OUTPUT INSERTED.ID INTO @rvTmp
	VALUES(@name, @emailAddress, @age)

	IF( @@ROWCOUNT <> 0)
		BEGIN
			SELECT @id = ID FROM @rvTmp;
			SELECT @recordVersion = [RecordVersion] FROM [dbo].[Customers] WHERE [ID] = @id;
			SET @retVal = 1;
		END
	ELSE
		SET @retVal = -1;

	RETURN @retVal;
	
END
GO

-- =============================================
-- Author:	Vlad
-- Description:	Select all stored procedure
-- =============================================
CREATE PROCEDURE [dbo].[CustomersSelectAll]

AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT * FROM [dbo].[Customers]

END
GO


USE [DellTehnicalTest]
GO
/****** Object:  StoredProcedure [dbo].[CustomersUpdate]    Script Date: 11/20/2018 3:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Vlad
-- Description:	Update stored procedure
-- =============================================
ALTER PROCEDURE [dbo].[CustomersUpdate] 

@age int,
@emailAddress varchar(50),
@name varchar(50),
@id int,
@recordVersion timestamp = NULL out

AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @retVal int
	DECLARE @rvTmp timestamp
	SELECT @rvTmp = [RecordVersion] FROM [Customers] WHERE ID = @id;
	
	IF(@recordVersion IS NULL OR @rvTmp = @recordVersion )
		BEGIN 
			UPDATE [Customers]
			SET [EmailAddress] = @emailAddress, [Age] = @age, [Name] = @name
			WHERE ([ID] = @id AND  (@recordVersion IS NULL OR [RecordVersion] = @recordVersion))

			IF( @@ROWCOUNT <> 0 )
				BEGIN
					SELECT @recordVersion = [RecordVersion] FROM [Customers] WHERE [ID] = @id
					SET @retVal = 1;
				END
			ELSE
				SET @retVal = -1;
		END

	RETURN @retVal
			
END
