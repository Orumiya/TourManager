CREATE TABLE [dbo].[Report](
[ReportID] int IDENTITY(1,1),
[ReportDate] DATE NOT NULL,
[ReportType] VARCHAR(10) NOT NULL, 
[ReportText] VARCHAR(50) NOT NULL, 
CONSTRAINT report_pk
PRIMARY KEY CLUSTERED ([ReportID] ASC));
