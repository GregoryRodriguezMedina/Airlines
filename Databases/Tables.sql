 USE mASTER 
 GO
 drop  DATABASE [Airlines]
 GO
 CREATE DATABASE [Airlines]
 GO
 USE [Airlines]
 GO
 CREATE TABLE [dbo].[AirPorts](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](50) NOT NULL,
	CONSTRAINT [PKAirPort] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Flights](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Code] [NVARCHAR](6) NOT NULL,
	[FromIdAirPort]  INT NOT NULL,
	[ToIdAirPort]  INT NOT NULL,
	[Price] [NVARCHAR] NOT NULL,
	[PriceChildren] [NVARCHAR] NOT NULL,
	[LimitAgeChildren] INT NOT NULL,
	[Date] DATETIME NOT NULL,
	[MinutesToArrive] INT NOT NULL,
	[DepartTime] DATETIME NOT NULL,
	[BoardingTime] DATETIME NOT NULL,
	[ArrivalTime] DATETIME NOT NULL,
	[ArriveConfirmed]DATETIME NOT NULL,
	[Status] [INT] NOT NULL,	
	CONSTRAINT FKFlightFrom FOREIGN KEY([FromIdAirPort]) REFERENCES [dbo].[AirPorts],
	CONSTRAINT FKFlightTo FOREIGN KEY([ToIdAirPort]) REFERENCES [dbo].[AirPorts],
	CONSTRAINT [PKFlight] PRIMARY KEY CLUSTERED([Id] ASC)
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Passagers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,	
	[BirthDate] [datetime] NOT NULL,
	CONSTRAINT [PKPassager] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] INT NOT NULL,
	[PassagerId] INT NOT NULL,	
	[Date] DATETIME NOT NULL,
	[CheckIn] DATETIME NULL,
	[CheckOut] DATETIME NULL,		
	CONSTRAINT FKPassager FOREIGN KEY([PassagerId]) REFERENCES [dbo].[Passagers],
	CONSTRAINT FKFlight FOREIGN KEY([FlightId]) REFERENCES [dbo].[Flights],
	CONSTRAINT [PKBook] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]
GO