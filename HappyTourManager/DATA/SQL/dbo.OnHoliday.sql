CREATE TABLE [dbo].[OnHoliday](
[OnHolidayID] int IDENTITY(1,1),
[StartDate] DATE NOT NULL,
[EndDate] DATE NOT NULL,
[TourguideID] int NOT NULL, 
CONSTRAINT onholiday_pk
PRIMARY KEY CLUSTERED ([OnHolidayID] ASC),
CONSTRAINT onholiday_fk
FOREIGN KEY ([TourguideID])
REFERENCES [dbo].[Tourguide] ([PersonID]));