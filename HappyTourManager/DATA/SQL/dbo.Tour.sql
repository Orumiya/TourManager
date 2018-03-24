CREATE TABLE [dbo].[Tour](
[TourID] int IDENTITY(1,1),
[TravelName] VARCHAR(50) NOT NULL,
[StartDate] DATE NOT NULL,
[EndDate] DATE NOT NULL,
["Description"] VARCHAR(250), 
[Transport] VARCHAR(10) NOT NULL, 
[MinNumber] NUMERIC(3) NOT NULL,
[MaxNumber] NUMERIC(3) NOT NULL,
[AdultPrice] MONEY NOT NULL,
[ChildPrice] MONEY NOT NULL,
[TourguideID] int, 
CONSTRAINT tour_pk
PRIMARY KEY CLUSTERED ([TourID] ASC),
CONSTRAINT tour_fk
FOREIGN KEY ([TourguideID])
REFERENCES [dbo].[Tourguide] ([PersonID]));
