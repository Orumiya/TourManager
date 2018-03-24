CREATE TABLE [dbo].[PRTCON](
[PRTCONID] int IDENTITY(1,1),
[TourID] int NOT NULL,
[ProgramID] int NOT NULL,
CONSTRAINT prtcon_pk
PRIMARY KEY ([PRTCONID]),    
CONSTRAINT prtcon_fk1
FOREIGN KEY ([TourID])
REFERENCES [dbo].[Tour]([TourID]),
CONSTRAINT prtcon_fk2
FOREIGN KEY ([ProgramID])
REFERENCES [dbo].[Program] ([ProgramID]));
