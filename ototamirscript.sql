USE [master]
GO
/****** Object:  Database [OtoTamirDB]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[Clients]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[PurchaseDetails]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[RepairComments]    Script Date: 21.06.2025 10:44:06 ******/
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
 CONSTRAINT [PK_RepairComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceRecords]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[SpareParts]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[Symptoms]    Script Date: 21.06.2025 10:44:06 ******/
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
/****** Object:  Table [dbo].[Vehicles]    Script Date: 21.06.2025 10:44:06 ******/
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
GO
INSERT [dbo].[AspNetUsers] ([Id], [StoreName], [Skills], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Status], [CreatedDate], [DeletedDate], [ModifiedDate], [IsProfileCompleted], [Adress], [ImageUrl]) VALUES (N'457d8deb-b455-4b8d-afcc-ee37894a41b2', N'admin', N'-', N'admin', N'ADMIN', N'bahirsare@gmail.com', N'BAHIRSARE@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEO7RKxw+7yyZ2KXz45LKsbyKwPnSQnDZsJ59/4MEj0SitMHxeVGRlBcCPaHJNuL7ng==', N'OPNLL6QYP34IIVTFPPOO4HX5AXILWDRG', N'ca268648-c325-4c42-8807-ec1af1340dd2', N'05436697175', 0, 0, NULL, 1, 0, 1, CAST(N'2025-05-12T23:22:53.6952128' AS DateTime2), NULL, CAST(N'2025-05-12T23:23:19.3160990' AS DateTime2), 1, N'istanbul', N'20250620224853558.png')
INSERT [dbo].[AspNetUsers] ([Id], [StoreName], [Skills], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Status], [CreatedDate], [DeletedDate], [ModifiedDate], [IsProfileCompleted], [Adress], [ImageUrl]) VALUES (N'f57341cc-e628-41b8-8e4d-8106e88ec125', N'ototamir', N'-', N'ototamir', N'OTOTAMIR', N'bahirsare@gmail.com', N'BAHIRSARE@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEGe+GHdzDBsG3vUgbhge1CyRO5F2EZfPGHVc1FdyxNspC4jrqs45RTmfTWvEBB4TRA==', N'DBQP2TSKEMTPBM6YCCLLMU2YGO6MFBIM', N'96504a9a-c6e3-4208-acae-552f91139808', N'5436697175', 0, 0, NULL, 1, 4, 1, CAST(N'2025-05-11T01:28:52.5147269' AS DateTime2), NULL, CAST(N'2025-05-11T01:29:49.8499067' AS DateTime2), 1, N'istanbul', N'avatar.png')
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (9, N'Ahmet Yılmaz', N'05551234567', CAST(1250.50 AS Decimal(18, 2)), N'Müşteri memnun', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-05-15T10:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (10, N'Mehmet Demir', N'05321239876', CAST(850.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-06-20T14:45:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-21T09:15:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (11, N'Ayşe Kaya', N'05443215678', CAST(3200.75 AS Decimal(18, 2)), N'Ödeme bekleniyor', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-07-10T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (12, N'Fatma Şahin', N'05059876543', CAST(500.00 AS Decimal(18, 2)), N'Yeni müşteri', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-08-05T16:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (13, N'Mustafa Arslan', N'05556781234', CAST(1750.00 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-09-12T09:10:00.0000000' AS DateTime2), NULL, CAST(N'2023-09-15T13:25:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (14, N'Zeynep Koç', N'05337894561', CAST(2200.50 AS Decimal(18, 2)), N'2. el parça kullanıldı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-10-18T12:40:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (15, N'Emre Yıldız', N'05441237895', CAST(980.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-11-22T15:20:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (16, N'Seda Aydın', N'05052348765', CAST(1500.00 AS Decimal(18, 2)), N'Araç teslim edildi', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2023-12-05T10:15:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-06T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (17, N'Can Bulut', N'05553456789', CAST(2750.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-01-10T13:45:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (18, N'Deniz Yılmaz', N'05324567891', CAST(620.50 AS Decimal(18, 2)), N'Randevu alındı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-02-14T11:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (19, N'Burak Korkmaz', N'05445678912', CAST(1850.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-03-18T09:25:00.0000000' AS DateTime2), NULL, CAST(N'2024-03-20T16:45:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (20, N'Elif Demirci', N'05056789123', CAST(2300.00 AS Decimal(18, 2)), N'Kontrol gerekiyor', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-04-22T14:10:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (21, N'Kerem Öztürk', N'05557891234', CAST(950.25 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-05-26T16:20:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (22, N'Aslı Güneş', N'05328912345', CAST(3100.50 AS Decimal(18, 2)), N'Detaylı temizlik yapıldı', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-06-30T10:35:00.0000000' AS DateTime2), NULL, CAST(N'2024-07-02T12:15:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (23, N'Umut Karaca', N'05449123456', CAST(1200.75 AS Decimal(18, 2)), NULL, N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-08-03T13:50:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[Clients] ([Id], [Name], [PhoneNumber], [Balance], [Notes], [MechanicId], [CreatedDate], [DeletedDate], [ModifiedDate]) VALUES (24, N'Cemre Deniz', N'05051234567', CAST(2650.00 AS Decimal(18, 2)), N'Yedek parça siparişi', N'457d8deb-b455-4b8d-afcc-ee37894a41b2', CAST(N'2024-09-07T15:05:00.0000000' AS DateTime2), NULL, CAST(N'2024-12-19T14:30:00.0000000' AS DateTime2))
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

INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (29, N'Ön Muayene', N'Yağ kaçağı tespit edildi', N'Mehmet Usta', 57, CAST(N'2023-01-10T09:15:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-10T09:15:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (30, N'Parça Siparişi', N'Yeni yağ karter contası siparişi', N'Ahmet Usta', 57, CAST(N'2023-01-11T10:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-11T10:20:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (31, N'Tamamlandı', N'Conta değişimi ve yağ yenileme', N'Mustafa', 57, CAST(N'2023-01-12T14:30:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T14:30:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (32, N'Fren Kontrolü', N'Balata aşınması tespit edildi', N'Mehmet Usta', 60, CAST(N'2023-02-05T10:30:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-05T10:30:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (33, N'Parça Tedarik', N'Brembo balatalar temin edildi', N'Mehmet Usta', 60, CAST(N'2023-02-06T11:45:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-06T11:45:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (34, N'Onarım Başladı', N'Fren sistemi sökümü yapıldı', N'Mehmet Usta', 60, CAST(N'2023-02-07T09:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:00:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (35, N'Klima Testi', N'Gaz basıncı düşük bulundu', N'Ahmet Usta', 65, CAST(N'2023-06-08T11:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-08T11:00:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (36, N'Arıza Tespiti', N'Kompresör contası arızalı', N'Ahmet Usta', 65, CAST(N'2023-06-09T14:15:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-09T14:15:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[RepairComments] ([Id], [Title], [Content], [AuthorName], [SymptomId], [CreatedDate], [DeletedDate], [ModifiedDate], [AdditionalCost], [AdditionalDays]) VALUES (37, N'Parça Siparişi', N'Yeni kompresör siparişi verildi', N'Ahmet Usta', 65, CAST(N'2023-06-10T09:30:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T09:30:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0)
SET IDENTITY_INSERT [dbo].[RepairComments] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceRecords] ON 

INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (1, N'Direksiyon sesi: Direksiyon çevirirken ses geliyor', CAST(4500.00 AS Decimal(18, 2)), N'Devam Ediyor', 14, CAST(N'2025-05-24T15:20:22.7968301' AS DateTime2), NULL, CAST(N'2025-05-24T15:20:22.7968676' AS DateTime2), N'2025-05-24 15:20 Tarihli Servis Kaydı', N'', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (2, N'Direksiyon sesi: Direksiyon çevirirken ses geliyor', CAST(1220.00 AS Decimal(18, 2)), N'Devam Ediyor', 19, CAST(N'2025-05-24T15:23:07.0329212' AS DateTime2), NULL, CAST(N'2025-05-24T15:23:07.0329230' AS DateTime2), N'2025-05-24 15:21 Tarihli Servis Kaydı', N'', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (3, N'Direksiyon sesi: Direksiyon çevirirken ses geliyor', CAST(123123.00 AS Decimal(18, 2)), N'Devam Ediyor', 11, CAST(N'2025-05-24T15:35:55.6104687' AS DateTime2), NULL, CAST(N'2025-05-24T15:35:55.6105122' AS DateTime2), N'2025-05-24 15:35 Tarihli Servis Kaydı', N'', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (4, N'tekersesi: sağ arka tekerlekten tıkırtı geliyor', CAST(500.00 AS Decimal(18, 2)), N'Devam Ediyor', 11, CAST(N'2025-05-24T15:47:17.9999566' AS DateTime2), NULL, CAST(N'2025-05-24T15:47:18.0000080' AS DateTime2), N'2025-05-24 15:47 Tarihli Servis Kaydı', N'', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (5, N'hararet: conta yakmış | yağ değişimi: 15000 km yağ değişimi ', CAST(8450.00 AS Decimal(18, 2)), N'Devam Ediyor', 12, CAST(N'2025-05-24T15:56:59.7662042' AS DateTime2), NULL, CAST(N'2025-05-24T15:56:59.7662579' AS DateTime2), N'2025-05-24 15:56 Tarihli Servis Kaydı', N'', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (7, N'asd: asd', CAST(2312.00 AS Decimal(18, 2)), N'Devam Ediyor', 10, CAST(N'2025-05-24T20:45:19.8458375' AS DateTime2), NULL, CAST(N'2025-05-24T20:45:19.8458837' AS DateTime2), N'2025-05-24 20:45 Tarihli Servis Kaydı', N'Admin', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (8, N'Direksiyon sesi: as', CAST(121.00 AS Decimal(18, 2)), N'Devam Ediyor', 12, CAST(N'2025-06-12T22:31:08.7550282' AS DateTime2), NULL, CAST(N'2025-06-12T22:31:08.7550596' AS DateTime2), N'2025-06-12 22:31 Tarihli Servis Kaydı', N'Admin', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (47, N'Yağ ve filtre değişimi', CAST(1200.00 AS Decimal(18, 2)), N'Tamamlandı', 21, CAST(N'2023-01-10T09:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-15T15:00:00.0000000' AS DateTime2), N'Periyodik Bakım', N'Mehmet Usta', CAST(N'2023-01-15T14:30:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (48, N'Fren balataları ve disk kontrolü', CAST(2500.00 AS Decimal(18, 2)), N'Devam Ediyor', 22, CAST(N'2023-02-05T10:15:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-10T11:20:00.0000000' AS DateTime2), N'Fren Sistemi Kontrolü', N'Ahmet Tekniker', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (49, N'Motor arıza lambası incelemesi', CAST(1800.00 AS Decimal(18, 2)), N'İptal Edildi', 23, CAST(N'2023-03-12T11:30:00.0000000' AS DateTime2), NULL, CAST(N'2023-03-12T11:30:00.0000000' AS DateTime2), N'Motor Arıza Tespiti', N'Ayşe Mekanik', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (50, N'Şanzıman yağı değişimi', CAST(3200.00 AS Decimal(18, 2)), N'Tamamlandı', 24, CAST(N'2023-04-10T14:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-04-18T17:00:00.0000000' AS DateTime2), N'Şanzıman Bakımı', N'Mehmet Usta', CAST(N'2023-04-18T16:45:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (51, N'Amortisör değişimi', CAST(4100.00 AS Decimal(18, 2)), N'Tamamlandı', 25, CAST(N'2023-05-15T08:30:00.0000000' AS DateTime2), NULL, CAST(N'2023-05-22T14:00:00.0000000' AS DateTime2), N'Süspansiyon Tamiri', N'Ali Teknisyen', CAST(N'2023-05-22T13:20:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (52, N'Kompresör değişimi ve gaz dolumu', CAST(2750.00 AS Decimal(18, 2)), N'Devam Ediyor', 26, CAST(N'2023-06-08T10:45:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-15T09:30:00.0000000' AS DateTime2), N'Klima Tamiri', N'Ahmet Tekniker', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (53, N'Akü ve alternatör testi', CAST(950.00 AS Decimal(18, 2)), N'İptal Edildi', 27, CAST(N'2023-07-14T13:15:00.0000000' AS DateTime2), NULL, CAST(N'2023-07-14T13:15:00.0000000' AS DateTime2), N'Elektrik Sistemi Kontrolü', N'Ayşe Mekanik', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (54, N'Muffler değişimi', CAST(1850.00 AS Decimal(18, 2)), N'Tamamlandı', 28, CAST(N'2023-08-10T09:45:00.0000000' AS DateTime2), NULL, CAST(N'2023-08-19T12:00:00.0000000' AS DateTime2), N'Egzoz Tamiri', N'Mehmet Usta', CAST(N'2023-08-19T11:10:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (55, N'Lastik rotasyonu ve basınç kontrolü', CAST(600.00 AS Decimal(18, 2)), N'Tamamlandı', 9, CAST(N'2023-09-20T10:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-09-25T16:00:00.0000000' AS DateTime2), N'Lastik Rotasyonu', N'Ali Teknisyen', CAST(N'2023-09-25T15:30:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (56, N'Enjektör temizliği ve filtre değişimi', CAST(1350.00 AS Decimal(18, 2)), N'Devam Ediyor', 10, CAST(N'2023-10-05T11:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-10-12T10:15:00.0000000' AS DateTime2), N'Yakıt Sistemi Temizliği', N'Ahmet Tekniker', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (57, N'Ön düzen ayarı', CAST(800.00 AS Decimal(18, 2)), N'İptal Edildi', 11, CAST(N'2023-11-08T14:45:00.0000000' AS DateTime2), NULL, CAST(N'2023-11-08T14:45:00.0000000' AS DateTime2), N'Rot Ayarı', N'Ayşe Mekanik', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (58, N'Timing kayışı ve su pompası değişimi', CAST(5200.00 AS Decimal(18, 2)), N'Tamamlandı', 12, CAST(N'2023-12-01T08:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-14T13:30:00.0000000' AS DateTime2), N'Distribütör Kayışı Değişimi', N'Mehmet Usta', CAST(N'2023-12-14T12:40:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (59, N'Far cilalama ve ampul değişimi', CAST(750.00 AS Decimal(18, 2)), N'Tamamlandı', 13, CAST(N'2024-01-10T09:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-01-18T11:00:00.0000000' AS DateTime2), N'Far Bakımı', N'Ali Teknisyen', CAST(N'2024-01-18T10:15:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (60, N'Tüm sistem taraması', CAST(1100.00 AS Decimal(18, 2)), N'Devam Ediyor', 14, CAST(N'2024-02-05T13:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-02-10T14:20:00.0000000' AS DateTime2), N'Diagnostik Tarama', N'Ahmet Tekniker', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (61, N'Radyatör temizliği ve termostat değişimi', CAST(1950.00 AS Decimal(18, 2)), N'İptal Edildi', 15, CAST(N'2024-03-12T10:45:00.0000000' AS DateTime2), NULL, CAST(N'2024-03-12T10:45:00.0000000' AS DateTime2), N'Soğutma Sistemi Bakımı', N'Ayşe Mekanik', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (62, N'Hidrolik pompa değişimi', CAST(2300.00 AS Decimal(18, 2)), N'Tamamlandı', 16, CAST(N'2024-04-05T11:15:00.0000000' AS DateTime2), NULL, CAST(N'2024-04-16T17:00:00.0000000' AS DateTime2), N'Direksiyon Tamiri', N'Mehmet Usta', CAST(N'2024-04-16T16:20:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (63, N'Yeni akü montajı', CAST(1450.00 AS Decimal(18, 2)), N'Tamamlandı', 17, CAST(N'2024-05-15T09:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-05-22T15:00:00.0000000' AS DateTime2), N'Akü Değişimi', N'Ali Teknisyen', CAST(N'2024-05-22T14:10:00.0000000' AS DateTime2))
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (64, N'Silecek lastiği değişimi', CAST(400.00 AS Decimal(18, 2)), N'Devam Ediyor', 18, CAST(N'2024-06-07T10:30:00.0000000' AS DateTime2), NULL, CAST(N'2024-06-12T11:45:00.0000000' AS DateTime2), N'Silecek Değişimi', N'Ahmet Tekniker', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (65, N'Tüm bujilerin değişimi', CAST(1250.00 AS Decimal(18, 2)), N'İptal Edildi', 19, CAST(N'2024-07-14T15:20:00.0000000' AS DateTime2), NULL, CAST(N'2024-07-14T15:20:00.0000000' AS DateTime2), N'Buji Değişimi', N'Ayşe Mekanik', NULL)
INSERT [dbo].[ServiceRecords] ([Id], [Description], [Price], [Status], [VehicleId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name], [AuthorName], [CompletedDate]) VALUES (66, N'Motor ve kabin hava filtresi değişimi', CAST(650.00 AS Decimal(18, 2)), N'Tamamlandı', 20, CAST(N'2024-08-10T08:45:00.0000000' AS DateTime2), NULL, CAST(N'2024-08-19T12:30:00.0000000' AS DateTime2), N'Hava Filtresi Değişimi', N'Mehmet Usta', CAST(N'2024-08-19T11:45:00.0000000' AS DateTime2))
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

INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (1, N'Direksiyon çevirirken ses geliyor', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(4500.00 AS Decimal(18, 2)), 3, N'Direksiyon sesi', N'Kutu tamiri, gerekirse değişim', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (2, N'Direksiyon çevirirken ses geliyor', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2, CAST(1220.00 AS Decimal(18, 2)), 3, N'Direksiyon sesi', N'Kutu tamiri, gerekirse değişim', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (4, N'Direksiyon çevirirken ses geliyor', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 3, CAST(123123.00 AS Decimal(18, 2)), 10, N'Direksiyon sesi', N'Kutu tamiri, gerekirse değişim', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (5, N'sağ arka tekerlekten tıkırtı geliyor', CAST(N'2025-05-24T15:47:36.4733066' AS DateTime2), NULL, CAST(N'2025-05-24T15:47:36.4733081' AS DateTime2), 4, CAST(500.00 AS Decimal(18, 2)), 1, N'tekersesi', N'tekerlek bilyaları kontrol edilecek', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (6, N'conta yakmış', CAST(N'2025-05-24T15:57:08.2052327' AS DateTime2), NULL, CAST(N'2025-05-24T15:57:08.2052347' AS DateTime2), 5, CAST(7800.00 AS Decimal(18, 2)), 7, N'hararet', N'kapak rektefe+ yeni conta', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (7, N'15000 km yağ değişimi ', CAST(N'2025-05-24T15:57:17.4077622' AS DateTime2), NULL, CAST(N'2025-05-24T15:57:17.4077638' AS DateTime2), 5, CAST(650.00 AS Decimal(18, 2)), 1, N'yağ değişimi', N'yağ değişimi', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (8, N'asd', CAST(N'2025-05-24T20:45:19.9490566' AS DateTime2), NULL, CAST(N'2025-05-24T20:45:19.9490593' AS DateTime2), 7, CAST(2312.00 AS Decimal(18, 2)), 12, N'asd', N'asd', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (9, N'as', CAST(N'2025-06-12T22:31:08.8712043' AS DateTime2), NULL, CAST(N'2025-06-12T22:31:08.8712061' AS DateTime2), 8, CAST(121.00 AS Decimal(18, 2)), 1, N'Direksiyon sesi', N'12', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (57, N'Yağ karter contasından sızıntı', CAST(N'2023-01-10T09:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-12T10:00:00.0000000' AS DateTime2), 47, CAST(500.00 AS Decimal(18, 2)), 1, N'Yağ Kaçağı', N'Yağ karter contası değişimi', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (58, N'Yüksek devirde tıkırtı sesi', CAST(N'2023-01-10T09:10:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-13T11:00:00.0000000' AS DateTime2), 47, CAST(800.00 AS Decimal(18, 2)), 2, N'Motor Gürültüsü', N'Yatak boşlukları kontrolü', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (59, N'Fren yaparken tiz ses', CAST(N'2023-02-05T10:20:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-07T09:30:00.0000000' AS DateTime2), 48, CAST(1200.00 AS Decimal(18, 2)), 1, N'Fren Gıcırtısı', N'Fren balata ve disk değişimi', N'Devam Ediyor')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (60, N'Fren pedalının fazla esnemesi', CAST(N'2023-02-05T10:25:00.0000000' AS DateTime2), NULL, CAST(N'2023-02-08T10:45:00.0000000' AS DateTime2), 48, CAST(1800.00 AS Decimal(18, 2)), 2, N'Yumuşak Fren Pedalı', N'Fren hidroliği değişimi', N'İptal Edildi')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (61, N'P0172 hatası - Zengin karışım', CAST(N'2023-03-12T11:35:00.0000000' AS DateTime2), NULL, CAST(N'2023-03-13T14:20:00.0000000' AS DateTime2), 49, CAST(950.00 AS Decimal(18, 2)), 1, N'Motor Arıza Lambası', N'Oksijen sensörü kontrolü', N'Devam Ediyor')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (62, N'Zayıf hava akışı ve ılık hava', CAST(N'2023-06-08T10:50:00.0000000' AS DateTime2), NULL, CAST(N'2023-06-10T11:15:00.0000000' AS DateTime2), 52, CAST(1750.00 AS Decimal(18, 2)), 2, N'Klima Soğutmuyor', N'Kompresör değişimi', N'Devam Ediyor')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (65, N'Timing kapağından gelen ses', CAST(N'2023-12-01T08:05:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T09:15:00.0000000' AS DateTime2), 58, CAST(3500.00 AS Decimal(18, 2)), 2, N'Distribütör Gürültüsü', N'Timing kayışı değişimi', N'Tamamlandı')
INSERT [dbo].[Symptoms] ([Id], [Description], [CreatedDate], [DeletedDate], [ModifiedDate], [ServiceRecordId], [EstimatedCost], [EstimatedDaysToFix], [Name], [PossibleSolution], [Status]) VALUES (66, N'Su pompası altında su birikmesi', CAST(N'2023-12-01T08:10:00.0000000' AS DateTime2), NULL, CAST(N'2023-12-03T10:00:00.0000000' AS DateTime2), 58, CAST(1700.00 AS Decimal(18, 2)), 1, N'Su Kaçağı', N'Su pompası değişimi', N'Tamamlandı')
SET IDENTITY_INSERT [dbo].[Symptoms] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([Id], [Plate], [Brand], [Model], [Year], [ClientId], [CreatedDate], [DeletedDate], [ModifiedDate], [Name]) VALUES (9, N'34ABC123', N'Toyota', N'Corolla', 2018, 9, CAST(N'2020-05-10T09:15:00.0000000' AS DateTime2), NULL, CAST(N'2021-03-12T14:20:00.0000000' AS DateTime2), N'')
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
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 21.06.2025 10:44:06 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 21.06.2025 10:44:06 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Clients_MechanicId_PhoneNumber]    Script Date: 21.06.2025 10:44:06 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Clients_MechanicId_PhoneNumber] ON [dbo].[Clients]
(
	[MechanicId] ASC,
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseDetails_SparePartId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseDetails_SparePartId] ON [dbo].[PurchaseDetails]
(
	[SparePartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RepairComments_SymptomId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_RepairComments_SymptomId] ON [dbo].[RepairComments]
(
	[SymptomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ServiceRecords_VehicleId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_ServiceRecords_VehicleId] ON [dbo].[ServiceRecords]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SpareParts_SymptomId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_SpareParts_SymptomId] ON [dbo].[SpareParts]
(
	[SymptomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Symptoms_ServiceRecordId]    Script Date: 21.06.2025 10:44:06 ******/
CREATE NONCLUSTERED INDEX [IX_Symptoms_ServiceRecordId] ON [dbo].[Symptoms]
(
	[ServiceRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_ClientId]    Script Date: 21.06.2025 10:44:06 ******/
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
