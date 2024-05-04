CREATE TABLE [dbo].[Basket]
(
	UserId UNIQUEIDENTIFIER NOT NULL,
	ArticleReference INT NOT NULL,
	Quantity INT NOT NULL DEFAULT 1,
	DateAddedBasket DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_Basket PRIMARY KEY (UserId, ArticleReference),
    CONSTRAINT FK_Basket_User FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id),
    CONSTRAINT FK_Basket_Article FOREIGN KEY (ArticleReference) REFERENCES [dbo].[Article](Reference)
)
