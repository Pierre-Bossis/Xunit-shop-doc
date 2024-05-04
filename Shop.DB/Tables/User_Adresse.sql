CREATE TABLE [dbo].[User_Adresse]
(
	UserId UNIQUEIDENTIFIER NOT NULL,
	AdresseId INT NOT NULL,

	CONSTRAINT PK_User_Adresse PRIMARY KEY (UserId, AdresseId),
    CONSTRAINT FK_User_Adresse_User FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id),
    CONSTRAINT FK_User_Adresse_Adresse FOREIGN KEY (AdresseId) REFERENCES [dbo].[Adresse](Id)
)
