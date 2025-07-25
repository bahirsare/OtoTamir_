USE [master]
GO
/****** Object:  Database [OtoTamirDB]    Script Date: 22.06.2025 11:29:47 ******/
CREATE DATABASE [OtoTamirDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OtoTamirDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OtoTamirDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OtoTamirDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OtoTamirDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OtoTamirDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OtoTamirDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OtoTamirDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OtoTamirDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OtoTamirDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OtoTamirDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OtoTamirDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OtoTamirDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OtoTamirDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OtoTamirDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OtoTamirDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OtoTamirDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OtoTamirDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OtoTamirDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OtoTamirDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OtoTamirDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OtoTamirDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OtoTamirDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OtoTamirDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OtoTamirDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OtoTamirDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OtoTamirDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OtoTamirDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [OtoTamirDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OtoTamirDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OtoTamirDB] SET  MULTI_USER 
GO
ALTER DATABASE [OtoTamirDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OtoTamirDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OtoTamirDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OtoTamirDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OtoTamirDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OtoTamirDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OtoTamirDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [OtoTamirDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OtoTamirDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[StoreName] [nvarchar](max) NOT NULL,
	[Skills] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[IsProfileCompleted] [bit] NOT NULL,
	[Adress] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BalanceLogs]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BalanceLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[NewBalance] [decimal](18, 2) NOT NULL,
	[OldBalance] [decimal](18, 2) NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_BalanceLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](450) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[MechanicId] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetails]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SparePartId] [int] NOT NULL,
	[SupplierName] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PurchaseDate] [datetime2](7) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PurchaseDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RepairComments]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RepairComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[AuthorName] [nvarchar](max) NOT NULL,
	[SymptomId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[AdditionalCost] [decimal](18, 2) NOT NULL,
	[AdditionalDays] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RepairComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceRecords]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[AuthorName] [nvarchar](max) NOT NULL,
	[CompletedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_ServiceRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpareParts]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpareParts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PartNumber] [nvarchar](max) NOT NULL,
	[Brand] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CompatibleVehicles] [nvarchar](max) NOT NULL,
	[SalePrice] [decimal](18, 2) NOT NULL,
	[SymptomId] [int] NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SpareParts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Symptoms]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Symptoms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ServiceRecordId] [int] NOT NULL,
	[EstimatedCost] [decimal](18, 2) NOT NULL,
	[EstimatedDaysToFix] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PossibleSolution] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Symptoms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 22.06.2025 11:29:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Plate] [nvarchar](max) NOT NULL,
	[Brand] [nvarchar](max) NOT NULL,
	[Model] [nvarchar](max) NOT NULL,
	[Year] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DeletedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250412190000_CreateDatabase', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250412204655_CreateDatabase1', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250412221204_Imageadition', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250412221834_mechanicfixation', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250417104609_deleteddatedFix', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250417122409_mechanicFixationNewProp', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250417135514_mechanicFi', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250417213235_addedAdressprop', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250418112837_mechanicfixadress', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250418222714_entityfix', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250418224824_baseentityfix', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250501110811_imageadded', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250501183227_imagefix', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250501200309_imagefix_2', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250503120326_imageremoval', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250505154412_clientnewkey', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250517214827_servicerecord', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250522150830_servicerecord-symptomRelation', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250524113258_SparePartsAdded', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250524134159_ServiceRecords-AuthorName-added', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250613103530_complateddateadded', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250613224055_additional days and price added to comments', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250620202127_statusfix', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250621204727_balancelogs-added', N'8.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250622062437_statusfix_2', N'8.0.14')
GO
INSERT [dbo].[AspNetUsers] ([Id], [StoreName], [Skills], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Status], [CreatedDate], [DeletedDate], [ModifiedDate], [IsProfileCompleted], [Adress], [ImageUrl]) VALUES (N'457d8deb-b455-4b8d-afcc-ee37894a41b2', N'admin', N'-', N'admin', N'ADMIN', N'bahirsare@gmail.com', N'BAHIRSARE@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEN4bAGl5EMiNa25ZYB7CUFv6Kc8jYjaDhIhlm0JDoz6/b8Kz18LmeZ2mXUn34WTu6g==', N'FM4T6XP6TXWIXW7GHYRJVAITFE745TPF', N'e3819d42-bd1b-4e90-85c1-ea4e37a25866', N'05436697175', 0, 0, NULL, 1, 0, 1, CAST(N'2025-05-12T23:22:53.6952128' AS DateTime2), NULL, CAST(N'2025-05-12T23:23:19.3160990' AS DateTime2), 1, N'istanbul', N'20250620224853558.png')
INSERT [dbo].[AspNetUsers] ([Id], [StoreName], [Skills], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Status], [CreatedDate], [DeletedDate], [ModifiedDate], [IsProfileCompleted], [Adress], [ImageUrl]) VALUES (N'f57341cc-e628-41b8-8e4d-8106e88ec125', N'ototamir', N'-', N'ototamir', N'OTOTAMIR', N'bahirsare@gmail.com', N'BAHIRSARE@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEGe+GHdzDBsG3vUgbhge1CyRO5F2EZfPGHVc1FdyxNspC4jrqs45RTmfTWvEBB4TRA==', N'DBQP2TSKEMTPBM6YCCLLMU2YGO6MFBIM', N'96504a9a-c6e3-4208-acae-552f91139808', N'5436697175', 0, 0, NULL, 1, 4, 1, CAST(N'2025-05-11T01:28:52.5147269' AS DateTime2), NULL, CAST(N'2025-05-11T01:29:49.8499067' AS DateTime2), 1, N'istanbul', N'avatar.png')
GO
SET IDENTITY_INSERT [dbo].[BalanceLogs] ON 

INSERT [dbo].[BalanceLogs] ([Id], [PaymentDate], [Amount], [NewBalance], [OldBalance], [ClientId]) VALUES (1, CAST(N'2025-06-22T00:25:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(18, 2)), CAST(-249.50 AS Decimal(18, 2)), CAST(1250.50 AS Decimal(18, 2)), 9)
INSERT [dbo].[BalanceLogs] ([Id], [PaymentDate], [Amount], [NewBalance], [OldBalance], [ClientId]) VALUES (2, CAST(N'2025-06-22T00:26:00.0000000' AS DateTime2), CAST(3100.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), CAST(3100.50 AS Decimal(18, 2)), 22)
INSERT [dbo].[BalanceLogs] ([Id], [PaymentDate], [Amount], [NewBalance], [OldBalance], [ClientId]) VALUES (3, CAST(N'2025-06-22T00:31:00.0000000' AS DateTime2), CAST(2650.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2650.00 AS Decimal(18, 2)), 24)
SET IDENTITY_INSERT [dbo].[BalanceLogs] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (9, N'Ahmet Yılmaz', N'05551234567', CAST(-249.50 AS Decimal(18, 2)), N'Müşteri memnun', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-05-15T10:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (10, N'Mehmet Demir', N'05321239876', CAST(850.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-06-20T14:45:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-21T09:15:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (11, N'Ayşe Kaya', N'05443215678', CAST(3200.75 AS Decimal(18, 2)), N'Ödeme bekleniyor', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-07-10T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (12, N'Fatma Şahin', N'05059876543', CAST(500.00 AS Decimal(18, 2)), N'Yeni müşteri', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-08-05T16:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (13, N'Mustafa Arslan', N'05556781234', CAST(1750.00 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-09-12T09:10:00.0000000' AS DateTime2), NULL, CAST(N'2023-09-15T13:25:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (14, N'Zeynep Koç', N'05337894561', CAST(2200.50 AS Decimal(18, 2)), N'2. el parça kullanıldı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-10-18T12:40:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (15, N'Emre Yıldız', N'05441237895', CAST(980.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-11-22T15:20:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (16, N'Seda Aydın', N'05052348765', CAST(1500.00 AS Decimal(18, 2)), N'Araç teslim edildi', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-12-05T10:15:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-06T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (17, N'Can Bulut', N'05553456789', CAST(0.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-01-10T13:45:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (18, N'Deniz Yılmaz', N'05324567891', CAST(620.50 AS Decimal(18, 2)), N'Randevu alındı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-02-14T11:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (19, N'Burak Korkmaz', N'05445678912', CAST(1850.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-03-18T09:25:00.0000000' AS DateTime2), NULL, CAST(N'2024-03-20T16:45:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (20, N'Elif Demirci', N'05056789123', CAST(2300.00 AS Decimal(18, 2)), N'Kontrol gerekiyor', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-04-22T14:10:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (21, N'Kerem Öztürk', N'05557891234', CAST(950.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-05-26T16:20:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (22, N'Aslı Güneş', N'05328912345', CAST(0.50 AS Decimal(18, 2)), N'Detaylı temizlik yapıldı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-06-30T10:35:00.0000000' AS DateTime2), NULL, CAST(N'2024-07-02T12:15:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (23, N'Umut Karaca', N'05449123456', CAST(1200.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-08-03T13:50:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (24, N'Cemre Deniz', N'05051234567', CAST(0.00 AS Decimal(18, 2)), N'Yedek parça siparişi', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-09-07T15:05:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (25, N'Berk Can', N'05552345678', CAST(780.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-10-11T09:40:00.0000000' AS DateTime2), NULL, CAST(N'2024-10-12T11:20:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (26, N'Dilara Akın', N'05323456789', CAST(1950.50 AS Decimal(18, 2)), N'Müşteri arayacak', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-11-15T12:55:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (27, N'Eren Yücel', N'05444567890', CAST(3400.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (28, N'Gizem Toprak', N'05055678901', CAST(850.00 AS Decimal(18, 2)), N'Araç alındı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-01-23T10:45:00.0000000' AS DateTime2), NULL, CAST(N'2025-01-25T15:10:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (29, N'Hakan Kılıç', N'05556789012', CAST(2250.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-02-27T16:00:00.0000000' AS DateTime2), NULL, CAST(N'2025-02-27T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (30, N'Irmak Su', N'05327890123', CAST(1500.50 AS Decimal(18, 2)), N'Randevu verildi', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-04-02T11:15:00.0000000' AS DateTime2), NULL, CAST(N'2025-04-02T11:15:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (31, N'Kaan Yıldırım', N'05448901234', CAST(2900.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-05-06T13:30:00.0000000' AS DateTime2), NULL, CAST(N'2025-05-08T09:45:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (32, N'Lale Arı', N'05059012345', CAST(1100.00 AS Decimal(18, 2)), N'Kontrol edilecek', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-06-10T15:55:00.0000000' AS DateTime2), NULL, CAST(N'2025-06-10T15:55:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (33, N'Murat Erdem', N'05550123456', CAST(2450.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-07-14T10:10:00.0000000' AS DateTime2), NULL, CAST(N'2025-07-14T10:10:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (34, N'Nazlı Gül', N'05331234567', CAST(1750.50 AS Decimal(18, 2)), N'Araç teslim alındı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-08-18T12:25:00.0000000' AS DateTime2), NULL, CAST(N'2025-08-20T14:40:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (35, N'Okan Bal', N'05442345678', CAST(3800.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-09-22T14:50:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (36, N'Pınar Işık', N'05053456789', CAST(950.00 AS Decimal(18, 2)), N'Yeni kayıt', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2025-10-26T09:05:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[RepairComments] ON 

INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays], [Status]) VALUES (40, N'ads', N'asd', N'asd', 72, CAST(N'2025-06-22T09:33:14.1465047' AS DateTime2), NULL, CAST(N'2025-06-22T09:33:10.0650027' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0, N'Devam Ediyor')
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays], [Status]) VALUES (41, N'22.06.2025 Tarihli İşlem Güncellemesi', N'içerik', N'yazar', 72, CAST(N'2025-06-22T10:08:36.7073321' AS DateTime2), NULL, CAST(N'2025-06-22T10:08:36.2039660' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0, N'Devam Ediyor')
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays], [Status]) VALUES (42, N'22.06.2025 Tarihli İşlem Güncellemesi', N'Kutu tamiri yapıldı', N'mehmet', 71, CAST(N'2025-06-22T10:39:57.9897010' AS DateTime2), NULL, CAST(N'2025-06-22T10:39:57.4326847' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0, N'Tamamlandı')
SET IDENTITY_INSERT [dbo].[RepairComments] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceRecords] ON 

INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (67, N'Direksiyon sesi: Direksiyon çevirirken ses yapıyor', CAST(600.00 AS Decimal(18, 2)), N'Devam Ediyor', 13, CAST(N'2025-06-22T00:40:23.8297197' AS DateTime2), NULL, CAST(N'2025-06-22T00:40:23.8297689' AS DateTime2), N'2025-06-22 00:40 Tarihli Servis Kaydı', N'mehmet', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (68, N'Direksiyon sesi: döndürürken ses yapıyor', CAST(500.00 AS Decimal(18, 2)), N'Devam Ediyor', 13, CAST(N'2025-06-22T00:42:24.6010603' AS DateTime2), NULL, CAST(N'2025-06-22T00:42:24.6010610' AS DateTime2), N'2025-06-22 00:42 Tarihli Servis Kaydı', N'mehmet', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (69, N'Direksiyon sesi: direksiyon dönerken ses yapıyor', CAST(500.00 AS Decimal(18, 2)), N'Devam Ediyor', 12, CAST(N'2025-06-22T00:43:11.1646967' AS DateTime2), NULL, CAST(N'2025-06-22T00:43:11.1647391' AS DateTime2), N'2025-06-22 00:43 Tarihli Servis Kaydı', N'ahmet', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (70, N'hararet: Conta yakmış', CAST(4500.00 AS Decimal(18, 2)), N'Devam Ediyor', 11, CAST(N'2025-06-22T00:47:24.5724432' AS DateTime2), NULL, CAST(N'2025-06-22T00:47:24.5724439' AS DateTime2), N'2025-06-22 00:47 Tarihli Servis Kaydı', N'mehmet', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (71, N'Direksiyon sesi: ses', CAST(500.00 AS Decimal(18, 2)), N'Tamamlandı', 17, CAST(N'2025-06-22T00:52:03.0987816' AS DateTime2), NULL, CAST(N'2025-06-22T00:52:03.0987817' AS DateTime2), N'2025-06-22 00:52 Tarihli Servis Kaydı', N'ahmet', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (72, N'dene: de', CAST(100.00 AS Decimal(18, 2)), N'Devam Ediyor', 10, CAST(N'2025-06-22T00:54:16.7579218' AS DateTime2), NULL, CAST(N'2025-06-22T00:54:16.7579219' AS DateTime2), N'2025-06-22 00:54 Tarihli Servis Kaydı', N'de', NULL)
SET IDENTITY_INSERT [dbo].[ServiceRecords] OFF
GO
SET IDENTITY_INSERT [dbo].[SpareParts] ON 

INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (1, N'Oil Pan Gasket', N'OPG-12345', N'Fel-Pro', N'Rubber gasket for oil pan', N'Toyota Corolla 2015-2020', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (2, N'Engine Oil 5W-30', N'OIL-530', N'Castrol', N'Full synthetic engine oil', N'Most gasoline engines', CAST(250.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:10:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:10:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (3, N'Main Bearing Set', N'MB-3344', N'ACL', N'Complete main bearing set', N'Various engines', CAST(450.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-13T11:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-13T11:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (4, N'Brake Pad Set', N'BP-FR234', N'Brembo', N'Front brake pads', N'Ford Focus 2017-2021', CAST(600.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (5, N'Brake Rotor', N'BR-FR556', N'Brembo', N'Front brake rotor', N'Ford Focus 2017-2021', CAST(800.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:40:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:40:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (6, N'Oxygen Sensor', N'O2S-4455', N'Bosch', N'Upstream oxygen sensor', N'VW Golf 2018-2022', CAST(350.00 AS Decimal(18, 2)), NULL, CAST(N'2023-03-13T14:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-03-13T14:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (7, N'Transmission Filter Kit', N'TFK-7788', N'Mann', N'Filter and gasket kit', N'Renault Megane 2015-2019', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2023-04-12T15:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-04-12T15:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (8, N'ATF Fluid', N'ATF-3309', N'Mobil', N'Automatic transmission fluid', N'Most European vehicles', CAST(400.00 AS Decimal(18, 2)), NULL, CAST(N'2023-04-12T15:40:00.0000000' AS DateTime2), NULL, CAST(N'2023-04-12T15:40:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (9, N'Shock Absorber', N'SA-FR334', N'KYB', N'Front shock absorber', N'Hyundai i20 2018-2022', CAST(900.00 AS Decimal(18, 2)), NULL, CAST(N'2023-05-17T09:50:00.0000000' AS DateTime2), NULL, CAST(N'2023-05-17T09:50:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (10, N'AC Compressor', N'ACC-5566', N'Denso', N'Complete AC compressor', N'Fiat Egea 2016-2020', CAST(1200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (11, N'Refrigerant R134a', N'REF-134', N'Honeywell', N'AC refrigerant', N'Most vehicles', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (12, N'Battery', N'BAT-65', N'Varta', N'65Ah car battery', N'Most medium-sized cars', CAST(850.00 AS Decimal(18, 2)), NULL, CAST(N'2023-07-15T14:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-07-15T14:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (13, N'Muffler', N'MFL-8899', N'Walker', N'Complete rear muffler', N'BMW 3 Series 2015-2019', CAST(950.00 AS Decimal(18, 2)), NULL, CAST(N'2023-08-12T10:50:00.0000000' AS DateTime2), NULL, CAST(N'2023-08-12T10:50:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (14, N'Fuel Injector Cleaner', N'FIC-100', N'Liqui Moly', N'Fuel system cleaner', N'All gasoline engines', CAST(150.00 AS Decimal(18, 2)), NULL, CAST(N'2023-10-07T12:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-10-07T12:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (15, N'Timing Belt Kit', N'TBK-9900', N'Gates', N'Timing belt with tensioners', N'Mercedes C-Class 2017-2021', CAST(2200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (16, N'Water Pump', N'WP-3344', N'Graf', N'Coolant water pump', N'Mercedes C-Class 2017-2021', CAST(750.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (17, N'Headlight Bulb', N'HLB-H7', N'Osram', N'H7 halogen bulb', N'Most European vehicles', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2024-01-12T10:45:00.0000000' AS DateTime2), NULL, CAST(N'2024-01-12T10:45:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (18, N'Power Steering Pump', N'PSP-5566', N'TRW', N'Complete power steering pump', N'Audi A3 2016-2020', CAST(1100.00 AS Decimal(18, 2)), NULL, CAST(N'2024-04-07T12:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-04-07T12:30:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (19, N'Spark Plug Set', N'SPK-4455', N'NGK', N'Set of 4 spark plugs', N'Seat Leon 2018-2022', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2024-07-15T16:35:00.0000000' AS DateTime2), NULL, CAST(N'2024-07-15T16:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (20, N'Cabin Air Filter', N'CAF-100', N'Mann', N'Activated carbon filter', N'Most vehicles', CAST(180.00 AS Decimal(18, 2)), NULL, CAST(N'2024-08-12T10:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-08-12T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (21, N'Oil Pan Gasket', N'OPG-12345', N'Fel-Pro', N'Rubber gasket for oil pan', N'Toyota Corolla 2015-2020', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (22, N'Engine Oil 5W-30', N'OIL-530', N'Castrol', N'Full synthetic engine oil', N'Most gasoline engines', CAST(250.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:10:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:10:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (23, N'Main Bearing Set', N'MB-3344', N'ACL', N'Complete main bearing set', N'Various engines', CAST(450.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-13T11:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-13T11:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (24, N'Brake Pad Set', N'BP-FR234', N'Brembo', N'Front brake pads', N'Ford Focus 2017-2021', CAST(600.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (25, N'Brake Rotor', N'BR-FR556', N'Brembo', N'Front brake rotor', N'Ford Focus 2017-2021', CAST(800.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:40:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:40:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (26, N'Oxygen Sensor', N'O2S-4455', N'Bosch', N'Upstream oxygen sensor', N'VW Golf 2018-2022', CAST(350.00 AS Decimal(18, 2)), NULL, CAST(N'2023-03-13T14:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-03-13T14:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (27, N'Transmission Filter Kit', N'TFK-7788', N'Mann', N'Filter and gasket kit', N'Renault Megane 2015-2019', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2023-04-12T15:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-04-12T15:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (28, N'ATF Fluid', N'ATF-3309', N'Mobil', N'Automatic transmission fluid', N'Most European vehicles', CAST(400.00 AS Decimal(18, 2)), NULL, CAST(N'2023-04-12T15:40:00.0000000' AS DateTime2), NULL, CAST(N'2023-04-12T15:40:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (29, N'Shock Absorber', N'SA-FR334', N'KYB', N'Front shock absorber', N'Hyundai i20 2018-2022', CAST(900.00 AS Decimal(18, 2)), NULL, CAST(N'2023-05-17T09:50:00.0000000' AS DateTime2), NULL, CAST(N'2023-05-17T09:50:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (30, N'AC Compressor', N'ACC-5566', N'Denso', N'Complete AC compressor', N'Fiat Egea 2016-2020', CAST(1200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (31, N'Refrigerant R134a', N'REF-134', N'Honeywell', N'AC refrigerant', N'Most vehicles', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (32, N'Battery', N'BAT-65', N'Varta', N'65Ah car battery', N'Most medium-sized cars', CAST(850.00 AS Decimal(18, 2)), NULL, CAST(N'2023-07-15T14:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-07-15T14:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (33, N'Muffler', N'MFL-8899', N'Walker', N'Complete rear muffler', N'BMW 3 Series 2015-2019', CAST(950.00 AS Decimal(18, 2)), NULL, CAST(N'2023-08-12T10:50:00.0000000' AS DateTime2), NULL, CAST(N'2023-08-12T10:50:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (34, N'Fuel Injector Cleaner', N'FIC-100', N'Liqui Moly', N'Fuel system cleaner', N'All gasoline engines', CAST(150.00 AS Decimal(18, 2)), NULL, CAST(N'2023-10-07T12:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-10-07T12:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (35, N'Timing Belt Kit', N'TBK-9900', N'Gates', N'Timing belt with tensioners', N'Mercedes C-Class 2017-2021', CAST(2200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (36, N'Water Pump', N'WP-3344', N'Graf', N'Coolant water pump', N'Mercedes C-Class 2017-2021', CAST(750.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (37, N'Headlight Bulb', N'HLB-H7', N'Osram', N'H7 halogen bulb', N'Most European vehicles', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2024-01-12T10:45:00.0000000' AS DateTime2), NULL, CAST(N'2024-01-12T10:45:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (38, N'Power Steering Pump', N'PSP-5566', N'TRW', N'Complete power steering pump', N'Audi A3 2016-2020', CAST(1100.00 AS Decimal(18, 2)), NULL, CAST(N'2024-04-07T12:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-04-07T12:30:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (39, N'Spark Plug Set', N'SPK-4455', N'NGK', N'Set of 4 spark plugs', N'Seat Leon 2018-2022', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2024-07-15T16:35:00.0000000' AS DateTime2), NULL, CAST(N'2024-07-15T16:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (40, N'Cabin Air Filter', N'CAF-100', N'Mann', N'Activated carbon filter', N'Most vehicles', CAST(180.00 AS Decimal(18, 2)), NULL, CAST(N'2024-08-12T10:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-08-12T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (41, N'Oil Pan Gasket', N'OPG-12345', N'Fel-Pro', N'Rubber gasket for oil pan', N'Toyota Corolla 2015-2020', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (42, N'Engine Oil 5W-30', N'OIL-530', N'Castrol', N'Full synthetic engine oil', N'Most gasoline engines', CAST(250.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:10:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:10:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (43, N'Main Bearing Set', N'MB-3344', N'ACL', N'Complete main bearing set', N'Various engines', CAST(450.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-13T11:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-13T11:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (44, N'Brake Pad Set', N'BP-FR234', N'Brembo', N'Front brake pads', N'Ford Focus 2017-2021', CAST(600.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (45, N'Brake Rotor', N'BR-FR556', N'Brembo', N'Front brake rotor', N'Ford Focus 2017-2021', CAST(800.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:40:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:40:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (46, N'Oxygen Sensor', N'O2S-4455', N'Bosch', N'Upstream oxygen sensor', N'VW Golf 2018-2022', CAST(350.00 AS Decimal(18, 2)), NULL, CAST(N'2023-03-13T14:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-03-13T14:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (47, N'Transmission Filter Kit', N'TFK-7788', N'Mann', N'Filter and gasket kit', N'Renault Megane 2015-2019', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2023-04-12T15:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-04-12T15:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (48, N'ATF Fluid', N'ATF-3309', N'Mobil', N'Automatic transmission fluid', N'Most European vehicles', CAST(400.00 AS Decimal(18, 2)), NULL, CAST(N'2023-04-12T15:40:00.0000000' AS DateTime2), NULL, CAST(N'2023-04-12T15:40:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (49, N'Shock Absorber', N'SA-FR334', N'KYB', N'Front shock absorber', N'Hyundai i20 2018-2022', CAST(900.00 AS Decimal(18, 2)), NULL, CAST(N'2023-05-17T09:50:00.0000000' AS DateTime2), NULL, CAST(N'2023-05-17T09:50:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (50, N'AC Compressor', N'ACC-5566', N'Denso', N'Complete AC compressor', N'Fiat Egea 2016-2020', CAST(1200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (51, N'Refrigerant R134a', N'REF-134', N'Honeywell', N'AC refrigerant', N'Most vehicles', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (52, N'Battery', N'BAT-65', N'Varta', N'65Ah car battery', N'Most medium-sized cars', CAST(850.00 AS Decimal(18, 2)), NULL, CAST(N'2023-07-15T14:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-07-15T14:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (53, N'Muffler', N'MFL-8899', N'Walker', N'Complete rear muffler', N'BMW 3 Series 2015-2019', CAST(950.00 AS Decimal(18, 2)), NULL, CAST(N'2023-08-12T10:50:00.0000000' AS DateTime2), NULL, CAST(N'2023-08-12T10:50:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (54, N'Fuel Injector Cleaner', N'FIC-100', N'Liqui Moly', N'Fuel system cleaner', N'All gasoline engines', CAST(150.00 AS Decimal(18, 2)), NULL, CAST(N'2023-10-07T12:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-10-07T12:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (55, N'Timing Belt Kit', N'TBK-9900', N'Gates', N'Timing belt with tensioners', N'Mercedes C-Class 2017-2021', CAST(2200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (56, N'Water Pump', N'WP-3344', N'Graf', N'Coolant water pump', N'Mercedes C-Class 2017-2021', CAST(750.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:25:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (57, N'Headlight Bulb', N'HLB-H7', N'Osram', N'H7 halogen bulb', N'Most European vehicles', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2024-01-12T10:45:00.0000000' AS DateTime2), NULL, CAST(N'2024-01-12T10:45:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (58, N'Power Steering Pump', N'PSP-5566', N'TRW', N'Complete power steering pump', N'Audi A3 2016-2020', CAST(1100.00 AS Decimal(18, 2)), NULL, CAST(N'2024-04-07T12:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-04-07T12:30:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (59, N'Spark Plug Set', N'SPK-4455', N'NGK', N'Set of 4 spark plugs', N'Seat Leon 2018-2022', CAST(300.00 AS Decimal(18, 2)), NULL, CAST(N'2024-07-15T16:35:00.0000000' AS DateTime2), NULL, CAST(N'2024-07-15T16:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (60, N'Cabin Air Filter', N'CAF-100', N'Mann', N'Activated carbon filter', N'Most vehicles', CAST(180.00 AS Decimal(18, 2)), NULL, CAST(N'2024-08-12T10:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-08-12T10:00:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (61, N'Yağ Karter Contası', N'OPG-12345', N'Fel-Pro', N'Yağ karteri için contaset', N'Toyota Corolla 2015-2020', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (62, N'Fren Balata Seti', N'BP-FR234', N'Brembo', N'Ön fren balataları', N'Ford Focus 2017-2021', CAST(600.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (63, N'Distribütör Kayış Seti', N'TBK-9900', N'Gates', N'Timing kayışı ve gergi rulmanı', N'Mercedes C-Serisi 2017-2021', CAST(2200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (64, N'Klima Kompresörü', N'ACC-5566', N'Denso', N'Tam klima kompresör seti', N'Fiat Egea 2016-2020', CAST(1200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (65, N'Yağ Karter Contası', N'OPG-12345', N'Fel-Pro', N'Yağ karteri için contaset', N'Toyota Corolla 2015-2020', CAST(120.00 AS Decimal(18, 2)), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:05:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (66, N'Fren Balata Seti', N'BP-FR234', N'Brembo', N'Ön fren balataları', N'Ford Focus 2017-2021', CAST(600.00 AS Decimal(18, 2)), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:35:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (67, N'Distribütör Kayış Seti', N'TBK-9900', N'Gates', N'Timing kayışı ve gergi rulmanı', N'Mercedes C-Serisi 2017-2021', CAST(2200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:20:00.0000000' AS DateTime2))
INSERT [dbo].[SpareParts] ([Id], [Name], [PartNumber], [Brand], [Description], [CompatibleVehicles], [SalePrice], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (68, N'Klima Kompresörü', N'ACC-5566', N'Denso', N'Tam klima kompresör seti', N'Fiat Egea 2016-2020', CAST(1200.00 AS Decimal(18, 2)), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:20:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[SpareParts] OFF
GO
SET IDENTITY_INSERT [dbo].[Symptoms] ON 

INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (68, N'döndürürken ses yapıyor', CAST(N'2025-06-22T00:42:24.6055722' AS DateTime2), NULL, CAST(N'2025-06-22T00:42:24.6055734' AS DateTime2), 68, CAST(500.00 AS Decimal(18, 2)), 1, N'Direksiyon sesi', N'motoru incelenecek', N'Devam Ediyor')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (69, N'direksiyon dönerken ses yapıyor', CAST(N'2025-06-22T00:43:11.2917896' AS DateTime2), NULL, CAST(N'2025-06-22T00:43:11.2917912' AS DateTime2), 69, CAST(500.00 AS Decimal(18, 2)), 1, N'Direksiyon sesi', N'motoru incelenecek', N'Devam Ediyor')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (70, N'Conta yakmış', CAST(N'2025-06-22T00:47:30.0536180' AS DateTime2), NULL, CAST(N'2025-06-22T00:47:30.0536189' AS DateTime2), 70, CAST(4500.00 AS Decimal(18, 2)), 7, N'hararet', N'rektefe', N'Devam Ediyor')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (71, N'ses', CAST(N'2025-06-22T00:52:03.1022602' AS DateTime2), NULL, CAST(N'2025-06-22T00:52:03.1022608' AS DateTime2), 71, CAST(500.00 AS Decimal(18, 2)), 1, N'Direksiyon sesi', N'Kutu tamiri, gerekirse değişim', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (72, N'de', CAST(N'2025-06-22T00:54:16.7612824' AS DateTime2), NULL, CAST(N'2025-06-22T00:54:16.7612829' AS DateTime2), 72, CAST(100.00 AS Decimal(18, 2)), 1, N'dene', N'ne', N'Devam Ediyor')
SET IDENTITY_INSERT [dbo].[Symptoms] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (10, N'06XYZ456', N'Ford', N'Focus', 2019, 10, CAST(N'2020-06-15T11:30:00.0000000' AS DateTime2), NULL, CAST(N'2020-12-01T16:45:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (11, N'35DEF789', N'Volkswagen', N'Golf', 2020, 11, CAST(N'2020-07-20T13:45:00.0000000' AS DateTime2), NULL, CAST(N'2021-01-05T10:10:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (12, N'16GHI012', N'Renault', N'Megane', 2017, 12, CAST(N'2020-08-25T15:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-04-22T09:30:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (13, N'07JKL345', N'Hyundai', N'i20', 2021, 13, CAST(N'2020-09-30T16:15:00.0000000' AS DateTime2), NULL, CAST(N'2021-02-18T11:25:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (14, N'34MNO678', N'Fiat', N'Egea', 2016, 14, CAST(N'2020-10-05T08:30:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-14T13:40:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (15, N'06PQR901', N'Opel', N'Astra', 2019, 15, CAST(N'2020-11-10T10:45:00.0000000' AS DateTime2), NULL, CAST(N'2021-03-28T15:55:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (16, N'35STU234', N'BMW', N'3 Serisi', 2018, 16, CAST(N'2020-12-15T12:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-06-02T08:05:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (17, N'16VWX567', N'Mercedes', N'C Serisi', 2020, 17, CAST(N'2021-01-20T14:15:00.0000000' AS DateTime2), NULL, CAST(N'2021-07-19T10:20:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (18, N'07YZA890', N'Audi', N'A3', 2019, 18, CAST(N'2021-02-25T16:30:00.0000000' AS DateTime2), NULL, CAST(N'2021-08-24T12:35:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (19, N'34BCD123', N'Peugeot', N'308', 2017, 19, CAST(N'2021-03-10T08:45:00.0000000' AS DateTime2), NULL, CAST(N'2021-09-29T14:50:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (20, N'06CDE456', N'Citroen', N'C4', 2021, 20, CAST(N'2021-04-15T11:00:00.0000000' AS DateTime2), NULL, CAST(N'2021-10-14T16:05:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (21, N'35EFG789', N'Honda', N'Civic', 2018, 21, CAST(N'2021-05-20T13:15:00.0000000' AS DateTime2), NULL, CAST(N'2021-11-19T09:10:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (22, N'16FGH012', N'Nissan', N'Qashqai', 2020, 22, CAST(N'2021-06-25T15:30:00.0000000' AS DateTime2), NULL, CAST(N'2021-12-24T11:25:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (23, N'07HIJ345', N'Kia', N'Sportage', 2019, 23, CAST(N'2021-07-30T17:45:00.0000000' AS DateTime2), NULL, CAST(N'2022-01-29T13:40:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (24, N'34JKL678', N'Skoda', N'Octavia', 2016, 24, CAST(N'2021-08-05T09:00:00.0000000' AS DateTime2), NULL, CAST(N'2022-02-03T15:55:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (25, N'06LMN901', N'Mazda', N'3', 2021, 25, CAST(N'2021-09-10T11:15:00.0000000' AS DateTime2), NULL, CAST(N'2022-03-10T08:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (26, N'35NOP234', N'Volvo', N'S60', 2018, 26, CAST(N'2021-10-15T13:30:00.0000000' AS DateTime2), NULL, CAST(N'2022-04-15T10:15:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (27, N'16PQR567', N'Seat', N'Leon', 2020, 27, CAST(N'2021-11-20T15:45:00.0000000' AS DateTime2), NULL, CAST(N'2022-05-20T12:30:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (28, N'07QRS890', N'Dacia', N'Sandero', 2019, 28, CAST(N'2021-12-25T18:00:00.0000000' AS DateTime2), NULL, CAST(N'2022-06-25T14:45:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (29, N'34RST123', N'Suzuki', N'Swift', 2017, 29, CAST(N'2022-01-10T09:15:00.0000000' AS DateTime2), NULL, CAST(N'2022-07-30T17:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (30, N'06STU456', N'Jeep', N'Renegade', 2021, 30, CAST(N'2022-02-15T11:30:00.0000000' AS DateTime2), NULL, CAST(N'2022-08-04T09:15:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (31, N'35TUV789', N'Mini', N'Cooper', 2018, 31, CAST(N'2022-03-20T13:45:00.0000000' AS DateTime2), NULL, CAST(N'2022-09-09T11:30:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (32, N'16UVW012', N'Lexus', N'IS', 2020, 32, CAST(N'2022-04-25T16:00:00.0000000' AS DateTime2), NULL, CAST(N'2022-10-14T13:45:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (33, N'07VWX345', N'Alfa Romeo', N'Giulietta', 2019, 33, CAST(N'2022-05-30T18:15:00.0000000' AS DateTime2), NULL, CAST(N'2022-11-19T16:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (34, N'34WXY678', N'Land Rover', N'Discovery Sport', 2016, 34, CAST(N'2022-06-05T09:30:00.0000000' AS DateTime2), NULL, CAST(N'2022-12-24T18:15:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (35, N'06XYZ901', N'Jaguar', N'XE', 2021, 35, CAST(N'2022-07-10T11:45:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-29T09:30:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (36, N'35YZA234', N'Subaru', N'Forester', 2018, 36, CAST(N'2022-08-15T14:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-03T11:45:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (38, N'34ABC456', N'Toyota', N'Yaris', 2019, 9, CAST(N'2020-05-15T10:20:00.0000000' AS DateTime2), NULL, CAST(N'2021-04-17T15:25:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (39, N'06DEF789', N'Ford', N'Fiesta', 2020, 10, CAST(N'2020-06-20T12:35:00.0000000' AS DateTime2), NULL, CAST(N'2021-01-10T17:40:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (40, N'35GHI012', N'Volkswagen', N'Passat', 2017, 11, CAST(N'2020-07-25T14:50:00.0000000' AS DateTime2), NULL, CAST(N'2021-02-15T09:55:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (41, N'16JKL345', N'Renault', N'Clio', 2021, 12, CAST(N'2020-08-30T17:05:00.0000000' AS DateTime2), NULL, CAST(N'2021-05-20T12:10:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (42, N'07MNO678', N'Hyundai', N'Tucson', 2016, 13, CAST(N'2020-09-05T09:20:00.0000000' AS DateTime2), NULL, CAST(N'2021-06-25T14:25:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (43, N'34PQR901', N'Fiat', N'Doblo', 2019, 14, CAST(N'2020-10-10T11:35:00.0000000' AS DateTime2), NULL, CAST(N'2021-07-30T16:40:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (44, N'06STU234', N'Opel', N'Corsa', 2020, 15, CAST(N'2020-11-15T13:50:00.0000000' AS DateTime2), NULL, CAST(N'2021-08-04T08:55:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (45, N'35VWX567', N'BMW', N'5 Serisi', 2018, 16, CAST(N'2020-12-20T16:05:00.0000000' AS DateTime2), NULL, CAST(N'2021-09-09T11:10:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (46, N'16YZA890', N'Mercedes', N'E Serisi', 2021, 17, CAST(N'2021-01-25T18:20:00.0000000' AS DateTime2), NULL, CAST(N'2021-10-14T13:35:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (47, N'07BCD123', N'Audi', N'A4', 2019, 18, CAST(N'2021-02-10T09:35:00.0000000' AS DateTime2), NULL, CAST(N'2021-11-19T15:50:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (48, N'34CDE456', N'Peugeot', N'208', 2017, 19, CAST(N'2021-03-15T11:50:00.0000000' AS DateTime2), NULL, CAST(N'2021-12-24T18:05:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (49, N'06EFG789', N'Citroen', N'C3', 2020, 20, CAST(N'2021-04-20T14:05:00.0000000' AS DateTime2), NULL, CAST(N'2022-01-29T09:20:00.0000000' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (50, N'15AAY555', N'audi', N'a3', 2015, 12, CAST(N'2025-05-19T00:41:42.3422687' AS DateTime2), NULL, CAST(N'2025-05-19T00:41:42.3424180' AS DateTime2), N'')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (51, N'34AAJ554', N'FORD', N'FOCUS', 2021, 9, CAST(N'2025-06-21T08:47:26.1005394' AS DateTime2), NULL, CAST(N'2025-06-21T08:47:26.1005738' AS DateTime2), N'34AAJ554_FORD')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (52, N'15AAY555', N'FİAT', N'LİNEA', 2013, 11, CAST(N'2025-06-21T09:26:11.8507847' AS DateTime2), NULL, CAST(N'2025-06-21T09:26:11.8508131' AS DateTime2), N'15AAY555_FİAT')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (53, N'34HTA463', N'TOYOTA', N'COROLLA', 2018, 11, CAST(N'2025-04-15T11:45:05.8000000' AS DateTime2), NULL, CAST(N'2025-04-15T11:45:05.8000000' AS DateTime2), N'34HTA463_TOYOTA')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (54, N'34NGK765', N'BMW', N'X5', 2020, 12, CAST(N'2025-03-21T09:05:12.5000000' AS DateTime2), NULL, CAST(N'2025-03-21T09:05:12.5000000' AS DateTime2), N'34NGK765_BMW')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (55, N'34MWL297', N'MERCEDES', N'C-Class', 2019, 13, CAST(N'2025-02-27T16:19:33.7000000' AS DateTime2), NULL, CAST(N'2025-02-27T16:19:33.7000000' AS DateTime2), N'34MWL297_MERCEDES')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (56, N'34JXP889', N'FORD', N'MONDEO', 2017, 14, CAST(N'2025-01-18T10:25:45.4000000' AS DateTime2), NULL, CAST(N'2025-01-18T10:25:45.4000000' AS DateTime2), N'34JXP889_FORD')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (57, N'34QEZ554', N'FİAT', N'500', 2021, 15, CAST(N'2025-06-10T13:40:22.8000000' AS DateTime2), NULL, CAST(N'2025-06-10T13:40:22.8000000' AS DateTime2), N'34QEZ554_FİAT')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (58, N'34AKT213', N'TOYOTA', N'YARIS', 2015, 16, CAST(N'2025-05-02T09:12:00.1000000' AS DateTime2), NULL, CAST(N'2025-05-02T09:12:00.1000000' AS DateTime2), N'34AKT213_TOYOTA')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (59, N'34LWR984', N'BMW', N'3 SERIES', 2020, 17, CAST(N'2025-04-10T17:00:36.3000000' AS DateTime2), NULL, CAST(N'2025-04-10T17:00:36.3000000' AS DateTime2), N'34LWR984_BMW')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (60, N'34BJF443', N'MERCEDES', N'E-Class', 2018, 18, CAST(N'2025-03-28T12:25:55.9000000' AS DateTime2), NULL, CAST(N'2025-03-28T12:25:55.9000000' AS DateTime2), N'34BJF443_MERCEDES')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (61, N'34HDC623', N'FORD', N'FOCUS', 2017, 19, CAST(N'2025-02-15T07:18:44.4000000' AS DateTime2), NULL, CAST(N'2025-02-15T07:18:44.4000000' AS DateTime2), N'34HDC623_FORD')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (62, N'34VPT114', N'FİAT', N'LİNEA', 2016, 20, CAST(N'2025-01-30T19:50:55.1000000' AS DateTime2), NULL, CAST(N'2025-01-30T19:50:55.1000000' AS DateTime2), N'34VPT114_FİAT')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (63, N'34BNR565', N'TOYOTA', N'AVENSIS', 2019, 21, CAST(N'2025-05-21T13:02:10.5000000' AS DateTime2), NULL, CAST(N'2025-05-21T13:02:10.5000000' AS DateTime2), N'34BNR565_TOYOTA')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (64, N'34LPK358', N'BMW', N'X3', 2021, 22, CAST(N'2025-06-02T16:29:17.7000000' AS DateTime2), NULL, CAST(N'2025-06-02T16:29:17.7000000' AS DateTime2), N'34LPK358_BMW')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (65, N'34TXA643', N'MERCEDES', N'C-Class', 2015, 23, CAST(N'2025-02-13T18:24:11.6000000' AS DateTime2), NULL, CAST(N'2025-02-13T18:24:11.6000000' AS DateTime2), N'34TXA643_MERCEDES')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (66, N'34JHF212', N'FORD', N'FOCUS', 2018, 24, CAST(N'2025-03-19T10:44:23.2000000' AS DateTime2), NULL, CAST(N'2025-03-19T10:44:23.2000000' AS DateTime2), N'34JHF212_FORD')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (67, N'34DLX471', N'FİAT', N'500', 2020, 25, CAST(N'2025-06-17T14:53:01.4000000' AS DateTime2), NULL, CAST(N'2025-06-17T14:53:01.4000000' AS DateTime2), N'34DLX471_FİAT')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (68, N'34RKS775', N'TOYOTA', N'COROLLA', 2020, 26, CAST(N'2025-03-22T09:37:59.5000000' AS DateTime2), NULL, CAST(N'2025-03-22T09:37:59.5000000' AS DateTime2), N'34RKS775_TOYOTA')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (69, N'34PGK124', N'BMW', N'X5', 2021, 27, CAST(N'2025-01-10T11:50:47.3000000' AS DateTime2), NULL, CAST(N'2025-01-10T11:50:47.3000000' AS DateTime2), N'34PGK124_BMW')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (70, N'34FSK587', N'MERCEDES', N'E-Class', 2018, 28, CAST(N'2025-06-08T15:41:32.1000000' AS DateTime2), NULL, CAST(N'2025-06-08T15:41:32.1000000' AS DateTime2), N'34FSK587_MERCEDES')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (71, N'34GJL924', N'FORD', N'MONDEO', 2016, 29, CAST(N'2025-04-26T12:31:22.6000000' AS DateTime2), NULL, CAST(N'2025-04-26T12:31:22.6000000' AS DateTime2), N'34GJL924_FORD')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (72, N'34CDB703', N'FİAT', N'PANDA', 2019, 30, CAST(N'2025-02-23T10:15:44.9000000' AS DateTime2), NULL, CAST(N'2025-02-23T10:15:44.9000000' AS DateTime2), N'34CDB703_FİAT')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (73, N'34MGT864', N'TOYOTA', N'YARIS', 2020, 31, CAST(N'2025-05-28T08:09:18.3000000' AS DateTime2), NULL, CAST(N'2025-05-28T08:09:18.3000000' AS DateTime2), N'34MGT864_TOYOTA')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (74, N'34WHE527', N'BMW', N'3 SERIES', 2021, 32, CAST(N'2025-01-14T13:55:28.2000000' AS DateTime2), NULL, CAST(N'2025-01-14T13:55:28.2000000' AS DateTime2), N'34WHE527_BMW')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (75, N'34JKF231', N'MERCEDES', N'C-Class', 2017, 33, CAST(N'2025-03-03T09:12:09.8000000' AS DateTime2), NULL, CAST(N'2025-03-03T09:12:09.8000000' AS DateTime2), N'34JKF231_MERCEDES')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (76, N'34KVG847', N'FORD', N'FOCUS', 2015, 34, CAST(N'2025-05-06T16:43:11.5000000' AS DateTime2), NULL, CAST(N'2025-05-06T16:43:11.5000000' AS DateTime2), N'34KVG847_FORD')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (77, N'34ZRM734', N'FORD', N'FOCUS', 2021, 9, CAST(N'2025-05-12T08:12:33.1000000' AS DateTime2), NULL, CAST(N'2025-05-12T08:12:33.1000000' AS DateTime2), N'34ZRM734_FORD')
INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (78, N'34VQJ912', N'FİAT', N'PANDA', 2016, 10, CAST(N'2025-06-05T14:33:21.2000000' AS DateTime2), NULL, CAST(N'2025-06-05T14:33:21.2000000' AS DateTime2), N'34VQJ912_FİAT')
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 22.06.2025 11:29:47 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 22.06.2025 11:29:47 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BalanceLogs_ClientId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_BalanceLogs_ClientId] ON [dbo].[BalanceLogs]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Clients_MechanicId_PhoneNumber]    Script Date: 22.06.2025 11:29:47 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Clients_MechanicId_PhoneNumber] ON [dbo].[Clients]
(
	[MechanicId] ASC,
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseDetails_SparePartId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseDetails_SparePartId] ON [dbo].[PurchaseDetails]
(
	[SparePartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RepairComments_SymptomId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_RepairComments_SymptomId] ON [dbo].[RepairComments]
(
	[SymptomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ServiceRecords_VehicleId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_ServiceRecords_VehicleId] ON [dbo].[ServiceRecords]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SpareParts_SymptomId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_SpareParts_SymptomId] ON [dbo].[SpareParts]
(
	[SymptomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Symptoms_ServiceRecordId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_Symptoms_ServiceRecordId] ON [dbo].[Symptoms]
(
	[ServiceRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_ClientId]    Script Date: 22.06.2025 11:29:47 ******/
CREATE NONCLUSTERED INDEX [IX_Vehicles_ClientId] ON [dbo].[Vehicles]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Status]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsProfileCompleted]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [ImageUrl]
GO
ALTER TABLE [dbo].[RepairComments] ADD  DEFAULT ((0.0)) FOR [AdditionalCost]
GO
ALTER TABLE [dbo].[RepairComments] ADD  DEFAULT ((0)) FOR [AdditionalDays]
GO
ALTER TABLE [dbo].[RepairComments] ADD  DEFAULT (N'') FOR [Status]
GO
ALTER TABLE [dbo].[ServiceRecords] ADD  DEFAULT (N'') FOR [Status]
GO
ALTER TABLE [dbo].[ServiceRecords] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[ServiceRecords] ADD  DEFAULT (N'') FOR [AuthorName]
GO
ALTER TABLE [dbo].[Symptoms] ADD  DEFAULT ((0)) FOR [ServiceRecordId]
GO
ALTER TABLE [dbo].[Symptoms] ADD  DEFAULT ((0.0)) FOR [EstimatedCost]
GO
ALTER TABLE [dbo].[Symptoms] ADD  DEFAULT ((0)) FOR [EstimatedDaysToFix]
GO
ALTER TABLE [dbo].[Symptoms] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[Symptoms] ADD  DEFAULT (N'') FOR [PossibleSolution]
GO
ALTER TABLE [dbo].[Vehicles] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BalanceLogs]  WITH CHECK ADD  CONSTRAINT [FK_BalanceLogs_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BalanceLogs] CHECK CONSTRAINT [FK_BalanceLogs_Clients_ClientId]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_AspNetUsers_MechanicId] FOREIGN KEY([MechanicId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_AspNetUsers_MechanicId]
GO
ALTER TABLE [dbo].[PurchaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetails_SpareParts_SparePartId] FOREIGN KEY([SparePartId])
REFERENCES [dbo].[SpareParts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseDetails] CHECK CONSTRAINT [FK_PurchaseDetails_SpareParts_SparePartId]
GO
ALTER TABLE [dbo].[RepairComments]  WITH CHECK ADD  CONSTRAINT [FK_RepairComments_Symptoms_SymptomId] FOREIGN KEY([SymptomId])
REFERENCES [dbo].[Symptoms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RepairComments] CHECK CONSTRAINT [FK_RepairComments_Symptoms_SymptomId]
GO
ALTER TABLE [dbo].[ServiceRecords]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRecords_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceRecords] CHECK CONSTRAINT [FK_ServiceRecords_Vehicles_VehicleId]
GO
ALTER TABLE [dbo].[SpareParts]  WITH CHECK ADD  CONSTRAINT [FK_SpareParts_Symptoms_SymptomId] FOREIGN KEY([SymptomId])
REFERENCES [dbo].[Symptoms] ([Id])
GO
ALTER TABLE [dbo].[SpareParts] CHECK CONSTRAINT [FK_SpareParts_Symptoms_SymptomId]
GO
ALTER TABLE [dbo].[Symptoms]  WITH CHECK ADD  CONSTRAINT [FK_Symptoms_ServiceRecords_ServiceRecordId] FOREIGN KEY([ServiceRecordId])
REFERENCES [dbo].[ServiceRecords] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Symptoms] CHECK CONSTRAINT [FK_Symptoms_ServiceRecords_ServiceRecordId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Clients_ClientId]
GO
USE [master]
GO
ALTER DATABASE [OtoTamirDB] SET  READ_WRITE 
GO
