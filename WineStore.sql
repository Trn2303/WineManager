USE [master]
GO
/****** Object:  Database [WineStore]    Script Date: 30/10/2024 5:41:58 CH ******/
CREATE DATABASE [WineStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WineStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WineStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WineStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WineStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [WineStore] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WineStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WineStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WineStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WineStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WineStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WineStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [WineStore] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [WineStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WineStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WineStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WineStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WineStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WineStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WineStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WineStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WineStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WineStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WineStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WineStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WineStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WineStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WineStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WineStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WineStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WineStore] SET  MULTI_USER 
GO
ALTER DATABASE [WineStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WineStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WineStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WineStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WineStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WineStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WineStore] SET QUERY_STORE = ON
GO
ALTER DATABASE [WineStore] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [WineStore]
GO
/****** Object:  Table [dbo].[Catalogy]    Script Date: 30/10/2024 5:41:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalogy](
	[CatalogyID] [nchar](10) NOT NULL,
	[CatalogyName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_Catalogies] PRIMARY KEY CLUSTERED 
(
	[CatalogyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 30/10/2024 5:41:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[Description] [text] NULL,
	[PurchasePrice] [numeric](8, 2) NOT NULL,
	[Price] [numeric](8, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Vintage] [nchar](20) NULL,
	[CatalogyID] [nchar](10) NOT NULL,
	[Image] [text] NULL,
	[Region] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 30/10/2024 5:41:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Products_Catalogy] FOREIGN KEY([CatalogyID])
REFERENCES [dbo].[Catalogy] ([CatalogyID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Products_Catalogy]
GO
USE [master]
GO
ALTER DATABASE [WineStore] SET  READ_WRITE 
GO
