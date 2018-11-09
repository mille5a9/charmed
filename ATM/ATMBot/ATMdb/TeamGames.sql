﻿CREATE TABLE [dbo].[TeamGames]
(
	[Id] INT NOT NULL PRIMARY KEY UNIQUE IDENTITY,
	[TeamID] NVARCHAR(90) NOT NULL,
	[GameTime] DATETIME NOT NULL,
	[Opponent] NVARCHAR(90) NOT NULL
);
