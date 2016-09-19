CREATE TABLE [dbo].[Animal]
(
	[AnimalId]				BIGINT			IDENTITY (1, 1) NOT NULL,
	[UserId]				BIGINT			NOT NULL,
	[AnimalTypeId]			BIGINT			NOT NULL,
	[Name]					NVARCHAR(200)	NOT NULL,
	[Hunger]				INT				NOT NULL,
	[LastFeedTime]			DATETIME2		NOT NULL,
	[Happiness]				INT				NOT NULL,
	[LastPetTime]			DATETIME2		NOT NULL,
	[LastUpdatedTime]		DATETIME2		NOT NULL, 
    CONSTRAINT [PK_Animal] PRIMARY KEY CLUSTERED ([AnimalId] ASC),
	CONSTRAINT [FK_Animal_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
	CONSTRAINT [FK_Animal_AnimalType] FOREIGN KEY ([AnimalTypeId]) REFERENCES [dbo].[AnimalType] ([AnimalTypeId]),
)
GO
