CREATE DATABASE UTRDB
CREATE TABLE [dbo].[ConvenienceStoreItems](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](max) NOT NULL,
	[ItemDescription] [nvarchar](max) NOT NULL,
	[ItemImage] [nvarchar](max) NOT NULL,
	[ItemCost] [decimal](10, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/09/2024 8:08:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[cusID] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](50) NOT NULL,
	[lastName] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[pass] [varchar](150) NOT NULL,
	[cardName] [varchar](100) NULL,
	[cardNum] [int] NULL,
	[cardDate] [date] NULL,
 CONSTRAINT [Customer_pk] PRIMARY KEY CLUSTERED 
(
	[cusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fuel]    Script Date: 11/09/2024 8:08:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fuel](
	[fuelID] [int] IDENTITY(1,1) NOT NULL,
	[storeID] [int] NULL,
	[fuelType] [varchar](50) NOT NULL,
	[pricePL] [float] NOT NULL,
 CONSTRAINT [Fuel_pk] PRIMARY KEY CLUSTERED 
(
	[fuelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 11/09/2024 8:08:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[storeID] [int] NULL,
	[firstName] [varchar](50) NOT NULL,
	[lastName] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[pass] [varchar](150) NOT NULL,
 CONSTRAINT [Manager_pk] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 11/09/2024 8:08:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[storeID] [int] IDENTITY(1,1) NOT NULL,
	[outlet] [varchar](50) NULL,
	[suburb] [varchar](50) NOT NULL,
	[street] [varchar](50) NOT NULL,
	[streetNum] [varchar](10) NOT NULL,
	[postcode] [int] NOT NULL,
 CONSTRAINT [Store_pk] PRIMARY KEY CLUSTERED 
(
	[storeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Fuel]  WITH CHECK ADD  CONSTRAINT [Fuel_fk] FOREIGN KEY([storeID])
REFERENCES [dbo].[Store] ([storeID])
GO
ALTER TABLE [dbo].[Fuel] CHECK CONSTRAINT [Fuel_fk]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [Manager_fk] FOREIGN KEY([storeID])
REFERENCES [dbo].[Store] ([storeID])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [Manager_fk]
GO
