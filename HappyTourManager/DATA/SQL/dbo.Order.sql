CREATE TABLE [dbo].[Order](
[OrderID] int IDENTITY(1,1),
[OrderDate] DATE NOT NULL,
[PersonCount] NUMERIC(3) NOT NULL,
[TotalSum] NUMERIC(10) NOT NULL,
[IsLoyalty] CHAR(1) DEFAULT 'N',
[IsPayed] CHAR(1) DEFAULT 'N',
[IsCancelled] CHAR(1) DEFAULT 'N',
[TourID] int NOT NULL,
[CustomerID] int NOT NULL
CONSTRAINT order_pk
PRIMARY KEY CLUSTERED ([OrderID] ASC),
CONSTRAINT order_fk1
FOREIGN KEY ([CustomerID])
REFERENCES [dbo].[Customer] ([PersonID]),
CONSTRAINT order_fk2
FOREIGN KEY ([TourID])
REFERENCES [dbo].[Tour] ([TourID]));