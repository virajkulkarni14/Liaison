USE [master]
GO
/****** Object:  Database [Liaison]    Script Date: 07/01/2020 22:17:14 ******/
CREATE DATABASE [Liaison]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Liaison', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Liaison.mdf' , SIZE = 12288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Liaison_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Liaison_log.ldf' , SIZE = 52416KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Liaison] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Liaison].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Liaison] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Liaison] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Liaison] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Liaison] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Liaison] SET ARITHABORT OFF 
GO
ALTER DATABASE [Liaison] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Liaison] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Liaison] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Liaison] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Liaison] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Liaison] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Liaison] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Liaison] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Liaison] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Liaison] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Liaison] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Liaison] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Liaison] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Liaison] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Liaison] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Liaison] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Liaison] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Liaison] SET RECOVERY FULL 
GO
ALTER DATABASE [Liaison] SET  MULTI_USER 
GO
ALTER DATABASE [Liaison] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Liaison] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Liaison] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Liaison] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Liaison] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Liaison', N'ON'
GO
USE [Liaison]
GO
/****** Object:  User [NT AUTHORITY\IUSR]    Script Date: 07/01/2020 22:17:15 ******/
CREATE USER [NT AUTHORITY\IUSR] FOR LOGIN [NT AUTHORITY\IUSR]
GO
/****** Object:  User [IIS APPPOOL\orbat.local]    Script Date: 07/01/2020 22:17:15 ******/
CREATE USER [IIS APPPOOL\orbat.local] FOR LOGIN [IIS APPPOOL\orbat.local] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\orbat.local]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\orbat.local]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\orbat.local]
GO
/****** Object:  Table [dbo].[AdminCorps]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminCorps](
	[AdminCorpsId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[SortName] [nvarchar](255) NULL,
	[DisplayName] [nvarchar](255) NULL,
	[UnitDisplayName] [nvarchar](255) NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ParentUnitId] [int] NULL,
	[Lookup] [nvarchar](5) NULL,
	[ParentAdminCorpsId] [int] NULL,
 CONSTRAINT [PK_AdminCorps] PRIMARY KEY CLUSTERED 
(
	[AdminCorpsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Aircraft]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aircraft](
	[AircraftId] [int] NULL,
	[FirstDate] [date] NULL,
	[AircraftGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [DF_Aircraft_AircraftId]  DEFAULT (newid()),
	[SeriesCode] [nvarchar](50) NULL,
	[SeriesCodeSort] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SortName] [nvarchar](50) NULL,
	[Mark] [nvarchar](50) NOT NULL,
	[Sort] [nvarchar](50) NULL,
	[AltCode] [nvarchar](50) NULL,
	[AltName] [nvarchar](50) NULL,
	[AltSort] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[DoNotUse] [bit] NULL,
 CONSTRAINT [PK_Aircraft] PRIMARY KEY CLUSTERED 
(
	[AircraftGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AltCode]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AltCode](
	[AltCodeId] [nvarchar](50) NOT NULL,
	[AltCodeUse] [nvarchar](50) NULL,
	[IndexCode10] [nvarchar](50) NULL,
	[IndexCode20] [nvarchar](50) NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Service] [nchar](10) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_AltCode] PRIMARY KEY CLUSTERED 
(
	[AltCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Base]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Base](
	[BaseId] [int] IDENTITY(1,1) NOT NULL,
	[SortName] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Prefix] [nvarchar](50) NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Suffix] [nvarchar](50) NULL,
	[Decommissioned] [bit] NULL,
	[CommissionedName] [nvarchar](255) NULL,
	[AltName] [nvarchar](255) NULL,
	[IATACode] [nvarchar](3) NULL,
	[ICAOCode] [nvarchar](4) NULL,
	[FAACode] [nvarchar](5) NULL,
	[AFDCode] [nvarchar](4) NULL,
	[ParentBaseId] [int] NULL,
	[ReplacementBaseId] [int] NULL,
	[ShipId] [uniqueidentifier] NULL,
	[City] [nvarchar](255) NULL,
	[ISO3166] [nvarchar](10) NULL,
 CONSTRAINT [PK_Base] PRIMARY KEY CLUSTERED 
(
	[BaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConfigSettings]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigSettings](
	[ConfigSetting] [nvarchar](255) NOT NULL,
	[ConfigValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_ConfigSettings] PRIMARY KEY CLUSTERED 
(
	[ConfigSetting] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dictionary](
	[Key] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Dictionary] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Document]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[DocumentId] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [DF_Document_DocumentId]  DEFAULT (newid()),
	[UnitId] [int] NULL,
	[Folder] [nvarchar](50) NOT NULL,
	[Filename] [nvarchar](255) NOT NULL,
	[FileType] [nvarchar](5) NOT NULL,
	[Date] [date] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Publisher] [nvarchar](255) NOT NULL,
	[Reference] [nvarchar](50) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentLink]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentLink](
	[DocumentId] [uniqueidentifier] NOT NULL,
	[LinkedId] [uniqueidentifier] NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_DocumentLink] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC,
	[LinkedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentReference]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentReference](
	[DocumentId] [uniqueidentifier] NOT NULL,
	[Reference] [nvarchar](255) NOT NULL,
	[Source] [nvarchar](255) NOT NULL,
	[CurrentRef] [bit] NOT NULL CONSTRAINT [DF_DocumentReference_CurrentRef]  DEFAULT ((1)),
 CONSTRAINT [PK_DocumentReference] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC,
	[Reference] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentOwner]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentOwner](
	[EquipmentOwnerId] [int] IDENTITY(1,1) NOT NULL,
	[UnitId] [int] NOT NULL,
	[Quantity] [decimal](5, 1) NULL,
	[Notes] [nvarchar](max) NULL,
	[IsOwner] [bit] NULL,
	[AircraftId] [uniqueidentifier] NULL,
	[GroundEquipmentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EquipmentOwner] PRIMARY KEY CLUSTERED 
(
	[EquipmentOwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroundEquipment]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroundEquipment](
	[GroundEquipmentId] [int] NULL,
	[EquipmentGuid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_GroundEquipment_EquipmentGuid]  DEFAULT (newid()),
	[Type] [nvarchar](10) NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[PrintName] [nvarchar](50) NULL,
	[FamilyDesignation] [nvarchar](50) NULL,
	[SortName] [nvarchar](255) NULL,
	[AltCode] [nvarchar](50) NULL,
	[AltName] [nvarchar](50) NULL,
	[AltSort] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[DoNotUse] [bit] NULL,
 CONSTRAINT [PK_GroundEquipment] PRIMARY KEY CLUSTERED 
(
	[EquipmentGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroundEquipmentType]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroundEquipmentType](
	[GroundEquipmentTypeCode] [nvarchar](10) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_GroundEquipmentType] PRIMARY KEY CLUSTERED 
(
	[GroundEquipmentTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mission]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mission](
	[MissionId] [int] IDENTITY(1,1) NOT NULL,
	[SortOrder] [nvarchar](255) NULL,
	[Structure] [nvarchar](50) NULL,
	[MainMission] [nvarchar](255) NULL,
	[MissionVariant] [nvarchar](255) NULL,
	[DisplayName] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[ShortForm] [nvarchar](50) NULL,
	[AltShortForm] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mission] PRIMARY KEY CLUSTERED 
(
	[MissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MissionUnit]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MissionUnit](
	[MissionId] [int] NOT NULL,
	[UnitId] [int] NOT NULL,
	[IsAssociate] [bit] NOT NULL CONSTRAINT [DF_MissionUnit_IsAssociate]  DEFAULT ((0)),
	[MissionVariant] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_MissionUnit] PRIMARY KEY CLUSTERED 
(
	[MissionId] ASC,
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organisation](
	[OrganisationId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Navy] [bit] NOT NULL,
	[Army] [bit] NOT NULL,
	[AirForce] [bit] NOT NULL,
	[Marines] [bit] NOT NULL,
	[Joint] [bit] NOT NULL,
	[Rank] [int] NOT NULL,
 CONSTRAINT [PK__Organisation] PRIMARY KEY CLUSTERED 
(
	[OrganisationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rank]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rank](
	[Symbol] [nvarchar](1) NOT NULL,
	[Civil] [nvarchar](255) NULL,
	[Army] [nvarchar](255) NULL,
	[Navy] [nvarchar](255) NULL,
	[AirForce] [nvarchar](255) NULL,
	[Joint] [nvarchar](255) NULL,
	[Rank] [nvarchar](8) NULL,
	[RankLevel] [int] NULL,
	[InUse] [bit] NULL,
	[CivilRank] [nvarchar](50) NULL,
	[CivilRankAbbrev] [nvarchar](50) NULL,
	[NavyRank] [nvarchar](50) NULL,
	[NavyRankAbbrev] [nvarchar](50) NULL,
	[ArmyRank] [nvarchar](50) NULL,
	[ArmyRankAbbrev] [nvarchar](50) NULL,
	[AirForceRank] [nvarchar](50) NULL,
	[AirForceRankAbbrev] [nvarchar](50) NULL,
	[MarineRank] [nvarchar](50) NULL,
	[MarineRankAbbrev] [nvarchar](50) NULL,
 CONSTRAINT [PK__Symbol] PRIMARY KEY CLUSTERED 
(
	[Symbol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Relationship]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relationship](
	[RelationshipId] [int] IDENTITY(1,1) NOT NULL,
	[RelationshipGuid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Relationship2_RelationshipGuid]  DEFAULT (newid()),
	[RelFrom] [uniqueidentifier] NULL,
	[RelTo] [uniqueidentifier] NULL,
	[RelTypeIdx] [int] NOT NULL,
	[RelFromUnitId] [int] NOT NULL,
	[RelToUnitId] [int] NOT NULL,
	[DoNotUse] [bit] NOT NULL,
 CONSTRAINT [PK_Relationship] PRIMARY KEY CLUSTERED 
(
	[RelFromUnitId] ASC,
	[RelToUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RelationshipType]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationshipType](
	[RelationshipTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_RelationshipType] PRIMARY KEY CLUSTERED 
(
	[RelationshipTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Service]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[ServiceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[ServiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ship]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ship](
	[ShipId] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Ship_ShipGuid]  DEFAULT (newid()),
	[UnitId] [int] NULL,
	[ShipPrefixId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[HCS] [nvarchar](50) NULL,
	[HCSNumber] [int] NULL,
	[PennantCode] [nvarchar](5) NULL,
	[PennantNumber] [int] NULL,
	[IsBase] [bit] NOT NULL,
	[AltName] [nvarchar](255) NULL,
	[AltHCS] [nvarchar](50) NULL,
	[AltHCSNumber] [int] NULL,
	[IsInactive] [bit] NOT NULL,
	[NewShipId] [uniqueidentifier] NULL,
	[Commissioned] [date] NULL,
	[Decommissioned] [date] NULL,
	[ShipClassId] [int] NULL,
 CONSTRAINT [PK_Ship] PRIMARY KEY CLUSTERED 
(
	[ShipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipClass]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipClass](
	[ShipClassId] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](255) NOT NULL,
	[ClassCodeHCS] [nvarchar](50) NOT NULL,
	[ClassCodeNumber] [int] NOT NULL,
	[ModFrom] [int] NULL,
	[ModName] [nvarchar](255) NULL,
 CONSTRAINT [PK_ShipClass] PRIMARY KEY CLUSTERED 
(
	[ShipClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipClassMember]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipClassMember](
	[ShipId] [uniqueidentifier] NOT NULL,
	[ShipClassId] [int] NOT NULL,
	[IsLeadBoat] [bit] NOT NULL,
 CONSTRAINT [PK_ShipClassMember] PRIMARY KEY CLUSTERED 
(
	[ShipId] ASC,
	[ShipClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipPrefix]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipPrefix](
	[ShipPrefixId] [int] IDENTITY(1,1) NOT NULL,
	[ShipPrefix] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ShipPrefix] PRIMARY KEY CLUSTERED 
(
	[ShipPrefixId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SortOrder]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SortOrder](
	[SortOrderId] [int] IDENTITY(1,1) NOT NULL,
	[SearchTerm] [nvarchar](255) NOT NULL,
	[SortOrderRank] [int] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_SortOrder] PRIMARY KEY CLUSTERED 
(
	[SortOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_UniqueSearchTerm] UNIQUE NONCLUSTERED 
(
	[SearchTerm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskForce]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskForce](
	[UnitId] [int] NOT NULL,
	[TaskForceName] [nvarchar](50) NULL,
	[TaskForceNo] [int] NULL,
	[TaskGroup] [int] NULL,
	[TaskUnit] [int] NULL,
	[TaskElement] [int] NULL,
	[SortName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_TaskForce] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tennant]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tennant](
	[TennancyId] [int] IDENTITY(1,1) NOT NULL,
	[BaseId] [int] NOT NULL,
	[UnitId] [int] NOT NULL,
	[IsHost] [bit] NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tennant] PRIMARY KEY CLUSTERED 
(
	[BaseId] ASC,
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Unit]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unit](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NULL,
	[UseOrdinal] [bit] NOT NULL CONSTRAINT [DF__Unit__UseOrdinal__2A4B4B5E]  DEFAULT ((0)),
	[Letter] [char](1) NULL,
	[NickName] [nvarchar](255) NULL,
	[LegacyMissionName] [nvarchar](255) NULL,
	[MissionName] [nvarchar](255) NULL,
	[UniqueName] [nvarchar](255) NULL,
	[CommandName] [nvarchar](255) NULL,
	[UnitTypeVariant] [nvarchar](255) NULL,
	[ServiceIdx] [int] NOT NULL,
	[ServiceTypeIdx] [int] NOT NULL,
	[TerritorialDesignation] [nvarchar](255) NULL,
	[UnitGuid] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Unit__UnitGuid__276EDEB3]  DEFAULT (newid()),
	[RankSymbol] [nvarchar](1) NOT NULL,
	[AdminCorpsId] [int] NULL,
	[CanHide] [bit] NOT NULL,
	[Decommissioned] [bit] NULL,
	[Language] [nvarchar](5) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UnitIndex]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitIndex](
	[UnitIndexId] [int] IDENTITY(1,1) NOT NULL,
	[IndexCode] [nvarchar](255) NOT NULL,
	[UnitGuid] [uniqueidentifier] NULL,
	[UnitId] [int] NOT NULL,
	[IsSortIndex] [bit] NOT NULL,
	[IsDisplayIndex] [bit] NOT NULL,
	[IsAlt] [bit] NOT NULL,
	[IsPlaceholder] [bit] NOT NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [PK_UnitIndex] PRIMARY KEY CLUSTERED 
(
	[UnitIndexId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_IndexCode] UNIQUE NONCLUSTERED 
(
	[IndexCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[MissionUnit_plus_UnitNumber]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MissionUnit_plus_UnitNumber]
AS
SELECT dbo.Unit.Number, dbo.MissionUnit.MissionId, dbo.MissionUnit.UnitId, dbo.MissionUnit.IsAssociate, dbo.MissionUnit.MissionVariant, dbo.MissionUnit.Notes
FROM     dbo.MissionUnit INNER JOIN
                  dbo.Unit ON dbo.MissionUnit.UnitId = dbo.Unit.UnitId
WHERE  (dbo.MissionUnit.UnitId IN
                      (SELECT RelToUnitId
                       FROM      dbo.Relationship
                       WHERE   (RelFromUnitId IN (27174, 27175, 27176))))

GO
/****** Object:  View [dbo].[Tennant_plus_UnitNumber]    Script Date: 07/01/2020 22:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Tennant_plus_UnitNumber]
AS
SELECT dbo.Unit.Number, dbo.Tennant.IsHost, dbo.Tennant.Notes
FROM     dbo.Tennant INNER JOIN
                  dbo.Unit ON dbo.Tennant.UnitId = dbo.Unit.UnitId
WHERE  (dbo.Tennant.UnitId IN
                      (SELECT RelToUnitId
                       FROM      dbo.Relationship
                       WHERE   (RelFromUnitId IN (27174, 27175, 27176))))

GO
ALTER TABLE [dbo].[AdminCorps]  WITH CHECK ADD  CONSTRAINT [FK_AdminCorps_AdminCorps] FOREIGN KEY([ParentUnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[AdminCorps] CHECK CONSTRAINT [FK_AdminCorps_AdminCorps]
GO
ALTER TABLE [dbo].[Base]  WITH CHECK ADD  CONSTRAINT [FK_Base_Base] FOREIGN KEY([ParentBaseId])
REFERENCES [dbo].[Base] ([BaseId])
GO
ALTER TABLE [dbo].[Base] CHECK CONSTRAINT [FK_Base_Base]
GO
ALTER TABLE [dbo].[Base]  WITH CHECK ADD  CONSTRAINT [FK_Base_Ship] FOREIGN KEY([ShipId])
REFERENCES [dbo].[Ship] ([ShipId])
GO
ALTER TABLE [dbo].[Base] CHECK CONSTRAINT [FK_Base_Ship]
GO
ALTER TABLE [dbo].[EquipmentOwner]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentOwner_Aircraft] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircraft] ([AircraftGuid])
GO
ALTER TABLE [dbo].[EquipmentOwner] CHECK CONSTRAINT [FK_EquipmentOwner_Aircraft]
GO
ALTER TABLE [dbo].[EquipmentOwner]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentOwner_GroundEquipment] FOREIGN KEY([GroundEquipmentId])
REFERENCES [dbo].[GroundEquipment] ([EquipmentGuid])
GO
ALTER TABLE [dbo].[EquipmentOwner] CHECK CONSTRAINT [FK_EquipmentOwner_GroundEquipment]
GO
ALTER TABLE [dbo].[EquipmentOwner]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentOwner_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[EquipmentOwner] CHECK CONSTRAINT [FK_EquipmentOwner_Unit]
GO
ALTER TABLE [dbo].[GroundEquipment]  WITH CHECK ADD  CONSTRAINT [FK_GroundEquipment_GroundEquipment] FOREIGN KEY([Type])
REFERENCES [dbo].[GroundEquipmentType] ([GroundEquipmentTypeCode])
GO
ALTER TABLE [dbo].[GroundEquipment] CHECK CONSTRAINT [FK_GroundEquipment_GroundEquipment]
GO
ALTER TABLE [dbo].[MissionUnit]  WITH CHECK ADD  CONSTRAINT [FK_MissionUnit_Mission] FOREIGN KEY([MissionId])
REFERENCES [dbo].[Mission] ([MissionId])
GO
ALTER TABLE [dbo].[MissionUnit] CHECK CONSTRAINT [FK_MissionUnit_Mission]
GO
ALTER TABLE [dbo].[MissionUnit]  WITH CHECK ADD  CONSTRAINT [FK_MissionUnit_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[MissionUnit] CHECK CONSTRAINT [FK_MissionUnit_Unit]
GO
ALTER TABLE [dbo].[Relationship]  WITH CHECK ADD  CONSTRAINT [FK_Relationship_RelationshipType] FOREIGN KEY([RelTypeIdx])
REFERENCES [dbo].[RelationshipType] ([RelationshipTypeId])
GO
ALTER TABLE [dbo].[Relationship] CHECK CONSTRAINT [FK_Relationship_RelationshipType]
GO
ALTER TABLE [dbo].[Relationship]  WITH CHECK ADD  CONSTRAINT [FK_RelationshipFrom_Unit] FOREIGN KEY([RelFromUnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Relationship] CHECK CONSTRAINT [FK_RelationshipFrom_Unit]
GO
ALTER TABLE [dbo].[Relationship]  WITH CHECK ADD  CONSTRAINT [FK_RelationshipTo_Unit] FOREIGN KEY([RelToUnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Relationship] CHECK CONSTRAINT [FK_RelationshipTo_Unit]
GO
ALTER TABLE [dbo].[Ship]  WITH CHECK ADD  CONSTRAINT [FK_Ship_NewShip] FOREIGN KEY([NewShipId])
REFERENCES [dbo].[Ship] ([ShipId])
GO
ALTER TABLE [dbo].[Ship] CHECK CONSTRAINT [FK_Ship_NewShip]
GO
ALTER TABLE [dbo].[Ship]  WITH CHECK ADD  CONSTRAINT [FK_Ship_ShipPrefix] FOREIGN KEY([ShipPrefixId])
REFERENCES [dbo].[ShipPrefix] ([ShipPrefixId])
GO
ALTER TABLE [dbo].[Ship] CHECK CONSTRAINT [FK_Ship_ShipPrefix]
GO
ALTER TABLE [dbo].[Ship]  WITH CHECK ADD  CONSTRAINT [FK_Ship_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Ship] CHECK CONSTRAINT [FK_Ship_Unit]
GO
ALTER TABLE [dbo].[ShipClass]  WITH CHECK ADD  CONSTRAINT [FK_ShipClass_Mod] FOREIGN KEY([ModFrom])
REFERENCES [dbo].[ShipClass] ([ShipClassId])
GO
ALTER TABLE [dbo].[ShipClass] CHECK CONSTRAINT [FK_ShipClass_Mod]
GO
ALTER TABLE [dbo].[ShipClassMember]  WITH CHECK ADD  CONSTRAINT [FK_ShipClassMember_Ship] FOREIGN KEY([ShipId])
REFERENCES [dbo].[Ship] ([ShipId])
GO
ALTER TABLE [dbo].[ShipClassMember] CHECK CONSTRAINT [FK_ShipClassMember_Ship]
GO
ALTER TABLE [dbo].[ShipClassMember]  WITH CHECK ADD  CONSTRAINT [FK_ShipClassMember_ShipClass] FOREIGN KEY([ShipClassId])
REFERENCES [dbo].[ShipClass] ([ShipClassId])
GO
ALTER TABLE [dbo].[ShipClassMember] CHECK CONSTRAINT [FK_ShipClassMember_ShipClass]
GO
ALTER TABLE [dbo].[TaskForce]  WITH CHECK ADD  CONSTRAINT [FK_TaskForce_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[TaskForce] CHECK CONSTRAINT [FK_TaskForce_Unit]
GO
ALTER TABLE [dbo].[Tennant]  WITH CHECK ADD  CONSTRAINT [FK_Tennant_Base] FOREIGN KEY([BaseId])
REFERENCES [dbo].[Base] ([BaseId])
GO
ALTER TABLE [dbo].[Tennant] CHECK CONSTRAINT [FK_Tennant_Base]
GO
ALTER TABLE [dbo].[Tennant]  WITH CHECK ADD  CONSTRAINT [FK_Tennant_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Tennant] CHECK CONSTRAINT [FK_Tennant_Unit]
GO
ALTER TABLE [dbo].[Unit]  WITH CHECK ADD  CONSTRAINT [FK_Unit_AdminCorps] FOREIGN KEY([AdminCorpsId])
REFERENCES [dbo].[AdminCorps] ([AdminCorpsId])
GO
ALTER TABLE [dbo].[Unit] CHECK CONSTRAINT [FK_Unit_AdminCorps]
GO
ALTER TABLE [dbo].[UnitIndex]  WITH CHECK ADD  CONSTRAINT [FK_UnitIndex_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[UnitIndex] CHECK CONSTRAINT [FK_UnitIndex_Unit]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Unit"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 288
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MissionUnit"
            Begin Extent = 
               Top = 7
               Left = 336
               Bottom = 170
               Right = 530
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'MissionUnit_plus_UnitNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'MissionUnit_plus_UnitNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Tennant"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Unit"
            Begin Extent = 
               Top = 7
               Left = 290
               Bottom = 170
               Right = 530
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Tennant_plus_UnitNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Tennant_plus_UnitNumber'
GO
USE [master]
GO
ALTER DATABASE [Liaison] SET  READ_WRITE 
GO
