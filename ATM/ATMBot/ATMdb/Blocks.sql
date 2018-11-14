﻿CREATE TABLE [dbo].[Blocks]
(
	[Id] INT NOT NULL PRIMARY KEY UNIQUE IDENTITY,
	[Timestamp] DATETIME NOT NULL,
	[Proof] NVARCHAR(40),
	[PreviousHash] NVARCHAR(40)
)