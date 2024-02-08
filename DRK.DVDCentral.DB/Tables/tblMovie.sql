﻿CREATE TABLE [dbo].[tblMovie]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [FormatId] INT NOT NULL, 
    [DirectorId] INT NOT NULL, 
    [RatingId] INT NOT NULL, 
    [Cost] FLOAT NOT NULL, 
    [IntStkQty] INT NOT NULL, 
    [ImagePath] VARCHAR(MAX) NOT NULL
)
