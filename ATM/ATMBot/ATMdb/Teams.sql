﻿CREATE TABLE [dbo].[Teams]
(
	[Id] INT NOT NULL PRIMARY KEY UNIQUE IDENTITY,
	[UsernameID] INT NOT NULL,
	[Team] NVARCHAR (90) NOT NULL
)
