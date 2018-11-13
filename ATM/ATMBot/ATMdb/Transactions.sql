CREATE TABLE [dbo].[Transactions]
(
	[Id] INT NOT NULL PRIMARY KEY UNIQUE IDENTITY,
	[Block_Id] INT NOT NULL,
	[Sender] NVARCHAR(40),
	[Recipient] NVARCHAR(40),
	[Amount] DECIMAL(38, 19) 
)
