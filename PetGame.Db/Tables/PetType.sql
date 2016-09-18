﻿CREATE TABLE [dbo].[PetType]
(
	[PetTypeId]					BIGINT			IDENTITY (1, 1) NOT NULL,
	[Name]						NVARCHAR(200)	NOT NULL,
	[MaxHunger]					INT				NOT NULL,
	[HungerIncreasePerMin]		INT				NOT NULL,
	[HungerDecreasePerFeed]		INT				NOT NULL,
	[FeedInterval]				INT				NOT NULL,
	[MaxHappiness]				INT				NOT NULL,
	[HappinessDecreasePerMin]	INT				NOT NULL,
	[HappinessIncreasePerPet]	INT				NOT NULL,
	[PettingInterval]			INT				NOT NULL

	CONSTRAINT [PK_PetType] PRIMARY KEY CLUSTERED ([PetTypeId] ASC)	
)
