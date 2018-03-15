CREATE TABLE [dbo].[Language](
[LanguageID] int IDENTITY(1,1),
[Language] VARCHAR(15) NOT NULL,
[TourguideID] int NOT NULL, 
CONSTRAINT language_pk
PRIMARY KEY CLUSTERED ([LanguageID] ASC),
CONSTRAINT language_fk
FOREIGN KEY ([TourguideID])
REFERENCES [dbo].[Tourguide] ([PersonID]));