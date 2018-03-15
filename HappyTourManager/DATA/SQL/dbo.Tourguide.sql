CREATE TABLE [dbo].[Tourguide](
[PersonID] int,
[Taxidentification] NUMERIC(15) NOT NULL,
[Dailyallowance] NUMERIC(15) NOT NULL, 
CONSTRAINT tourguide_pk
PRIMARY KEY CLUSTERED ([PersonID] ASC),
CONSTRAINT tourguide_fk
FOREIGN KEY ([PersonID])
REFERENCES [dbo].[Person] ([PersonID]));
