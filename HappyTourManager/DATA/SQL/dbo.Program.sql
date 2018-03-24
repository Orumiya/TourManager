CREATE TABLE [dbo].[Program](
[ProgramID] int IDENTITY(1,1),
[ProgramType] VARCHAR(50) NOT NULL,
CONSTRAINT program_pk
PRIMARY KEY CLUSTERED ([ProgramID] ASC));