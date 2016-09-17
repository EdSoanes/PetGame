CREATE TABLE [dbo].[Pet]
(
	[PetId]					BIGINT			IDENTITY (1, 1) NOT NULL,
	[UserId]				BIGINT			NOT NULL,
	[PetTypeId]				BIGINT			NOT NULL,
	[Name]					NVARCHAR(200)	NOT NULL,
	[Health]				INT				NOT NULL,
	[LastFeedTime]			DATETIME2		NOT NULL,
	[Happiness]				INT				NOT NULL,
	[LastPetTime]			DATETIME2		NOT NULL,
	CONSTRAINT [PK_Pet] PRIMARY KEY CLUSTERED ([PetId] ASC),
	CONSTRAINT [FK_Pet_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
	CONSTRAINT [FK_Pet_PetType] FOREIGN KEY ([PetTypeId]) REFERENCES [dbo].[PetType] ([PetTypeId]),
)
GO

CREATE UNIQUE INDEX UX_Pet_UserId_PetTypeId ON [dbo].[Pet] (UserId, PetTypeId)
go