﻿CREATE TABLE [dbo].[User]
(
	Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Email VARCHAR(100) NOT NULL,
	Nom VARCHAR(100) NOT NULL,
	Prenom VARCHAR(100) NOT NULL,
	MotDePasse VARCHAR(MAX) NOT NULL,
	DateCreation DATETIME NOT NULL DEFAULT GETDATE(),
	IsAdmin BIT NOT NULL DEFAULT 0
)
