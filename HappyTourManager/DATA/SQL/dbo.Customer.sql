CREATE TABLE [dbo].[Customer](
[PersonID] int,
[LoyaltyCard] CHAR(1), 
CONSTRAINT customer_pk
PRIMARY KEY CLUSTERED ([PersonID] ASC),
CONSTRAINT customer_fk
FOREIGN KEY ([PersonID])
REFERENCES [dbo].[Person] ([PersonID]));
