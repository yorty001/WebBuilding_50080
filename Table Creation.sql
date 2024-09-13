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

INSERT INTO Customer (firstName, lastName, email, password)
VALUES ('John', 'Doe', 'johndoe@example.com', 'Pass1234'),
	('Jane', 'Smith', 'janesmith@example.com', 'Smithy987'),
	('Michael', 'Johnson', 'mjohnson@example.com', 'MikeJ2023!');

Insert data into the ConvenienceStoreItems table
INSERT INTO [dbo].[ConvenienceStoreItems] ([ItemId], [ItemName], [ItemDescription], [ItemImage], [ItemCost])
VALUES 
('Prime Hydration Blue Raspberry', 'PRIME HYDRATION BLUE RASPBERRY. PRIME was developed to fill the void where great taste meets function. With bold, thirst-quenching flavours to help you refresh, replenish, and refuel, PRIME is the perfect boost for any endeavour.', 'https://assets.woolworths.com.au/images/1005/275804.jpg?impolicy=wowsmkqiema&w=600&h=600', 4.00),
( 'Pura Full Cream Milk 2l', 'PURA Full Cream milk is Australian Owned, made & loved', 'https://assets.woolworths.com.au/images/1005/62636.jpg?impolicy=wowsmkqiema&w=600&h=600', 5.50),
('Red Rock Deli Potato Chips Sea Salt Natural 165g', 'Wholesome Potato Chips Carefully Crafted with Subtle Flavours. Red Rock Deli Sea Salt Potato Chips keeps things simple yet sensational with the careful coating of classic sea salt.', 'https://assets.woolworths.com.au/images/1005/781396.jpg?impolicy=wowsmkqiema&w=600&h=600', 6.00),
('Lindt Lindor Assorted Chocolate Box 333g', 'Wherever and whenever you take a LINDOR moment, it just seems to make life feel so much more sublime.', 'https://assets.woolworths.com.au/images/1005/114683.jpg?impolicy=wowsmkqiema&w=600&h=600', 26.00),
('Cadbury Dairy Milk Vanilla Sticks 4 Pack', 'Vanilla with a unique choc swirl, coated in a generous layer of Cadbury Dairy Milk milk chocolate. Its deliciously smooth and creamy.', 'https://assets.woolworths.com.au/images/1005/122672.jpg?impolicy=wowsmkqiema&w=600&h=600', 10.50),
('Pump Spring Water Bottle 750ml', 'Pump Water will help you stay hydrated throughout the day. This purified water comes with an easy to use sipper cap making it perfect for drinking on the go.', 'https://assets.woolworths.com.au/images/1005/153622.jpg?impolicy=wowsmkqiema&w=600&h=600', 3.00),
('Colgate Travel Toothpaste Total Original 40g', 'Colgate Total Original Antibacterial Fluoride Toothpaste reduces bacteria on teeth, tongue, cheeks and gums.', 'https://assets.woolworths.com.au/images/1005/819772.jpg?impolicy=wowsmkqiema&w=600&h=600', 2.50),
( 'Sour Patch Kids Lollies 190g', 'Whats Sour then Sweet but always delicious? You already know: SOUR PATCH KIDS lollies!', 'https://assets.woolworths.com.au/images/1005/234699.jpg?impolicy=wowsmkqiema&w=600&h=600', 5.00);

