USE [master]
GO
/****** Object:  Database [ProcureEase]    Script Date: 9/19/2018 11:17:56 AM ******/
CREATE DATABASE [ProcureEase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProcureEase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProcureEase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProcureEase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProcureEase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProcureEase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProcureEase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProcureEase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProcureEase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProcureEase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProcureEase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProcureEase] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProcureEase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProcureEase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProcureEase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProcureEase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProcureEase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProcureEase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProcureEase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProcureEase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProcureEase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProcureEase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProcureEase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProcureEase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProcureEase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProcureEase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProcureEase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProcureEase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProcureEase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProcureEase] SET RECOVERY FULL 
GO
ALTER DATABASE [ProcureEase] SET  MULTI_USER 
GO
ALTER DATABASE [ProcureEase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProcureEase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProcureEase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProcureEase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProcureEase] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProcureEase', N'ON'
GO
ALTER DATABASE [ProcureEase] SET QUERY_STORE = OFF
GO
USE [ProcureEase]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ProcureEase]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 9/19/2018 11:17:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdvertCategory]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertCategory](
	[AdvertCategoryID] [uniqueidentifier] NOT NULL,
	[AdvertCategory] [varchar](150) NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdvertCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdvertCategoryNumber]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertCategoryNumber](
	[AdvertCategoryNumberID] [uniqueidentifier] NOT NULL,
	[AdvertID] [uniqueidentifier] NULL,
	[AdvertCategoryID] [uniqueidentifier] NULL,
	[ProjectCategoryID] [uniqueidentifier] NULL,
	[CategoryLotCode] [nvarchar](128) NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdvertCategoryNumberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdvertisedItems]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertisedItems](
	[ItemID] [uniqueidentifier] NOT NULL,
	[Quantity] [varchar](150) NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdvertLotNumber]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertLotNumber](
	[AdvertLotNumberID] [uniqueidentifier] NOT NULL,
	[AdvertID] [uniqueidentifier] NULL,
	[ProcurementID] [uniqueidentifier] NULL,
	[LotCode] [nvarchar](128) NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdvertLotNumberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adverts]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adverts](
	[AdvertID] [uniqueidentifier] NOT NULL,
	[BudgetYearID] [uniqueidentifier] NULL,
	[Headline] [varchar](max) NULL,
	[Introduction] [varchar](max) NULL,
	[ScopeOfWork] [varchar](max) NULL,
	[EligibiltyRequirement] [varchar](max) NULL,
	[CollectionOfTenderDocument] [varchar](max) NULL,
	[BidSubmission] [varchar](max) NULL,
	[OtherImportantInformation] [varchar](max) NULL,
	[BidOpeningDate] [datetime] NULL,
	[BidClosingDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[AdvertStatusID] [int] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdvertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdvertStatus]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertStatus](
	[Status] [varchar](50) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[AdvertStatusID] [int] NOT NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdvertStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApprovedItems]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovedItems](
	[ItemID] [uniqueidentifier] NOT NULL,
	[Quantity] [varchar](150) NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[Date_Created] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[TenantID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetYear]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetYear](
	[BudgetYearID] [uniqueidentifier] NOT NULL,
	[BudgetYear] [date] NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[BudgetYearID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog](
	[TenantID] [uniqueidentifier] NOT NULL,
	[RequestID] [uniqueidentifier] NULL,
	[SubDomain] [varchar](10) NULL,
	[OrganizationID] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SubDomain] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [uniqueidentifier] NOT NULL,
	[DepartmentName] [varchar](100) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DepartmentHeadUserID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NULL,
	[OrganisationID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemCode]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCode](
	[ItemCodeID] [uniqueidentifier] NOT NULL,
	[ItemCode] [nvarchar](50) NULL,
	[ItemName] [varchar](100) NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemID] [uniqueidentifier] NOT NULL,
	[ProcurementID] [uniqueidentifier] NULL,
	[ApprovedItemID] [uniqueidentifier] NULL,
	[AdvertisedItemID] [uniqueidentifier] NULL,
	[ItemName] [varchar](150) NULL,
	[Description] [varchar](max) NULL,
	[Quantity] [float] NULL,
	[UnitPrice] [float] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[ItemCodeID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrganizationSettings]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationSettings](
	[OrganizationID] [uniqueidentifier] NOT NULL,
	[OrganizationNameInFull] [varchar](150) NULL,
	[OrganizationNameAbbreviation] [varchar](10) NULL,
	[OrganizationEmail] [varchar](100) NULL,
	[Address] [varchar](500) NULL,
	[Country] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[AboutOrganization] [varchar](max) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[OrganizationLogoPath] [nvarchar](max) NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcurementMethod]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcurementMethod](
	[ProcurementMethodID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](300) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProcurementMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcurementMethodOrganizationSettings]    Script Date: 9/19/2018 11:17:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcurementMethodOrganizationSettings](
	[ProcurementMethodID] [uniqueidentifier] NOT NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
	[OrganizationID] [uniqueidentifier] NOT NULL,
	[EnableProcurementMethod] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ProcurementMethodOrganizationSettings] PRIMARY KEY CLUSTERED 
(
	[ProcurementMethodID] ASC,
	[TenantID] ASC,
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Procurements]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Procurements](
	[ProcurementID] [uniqueidentifier] NOT NULL,
	[BudgetYearID] [uniqueidentifier] NULL,
	[DepartmentID] [uniqueidentifier] NULL,
	[ProjectName] [varchar](150) NULL,
	[ProjectCategoryID] [uniqueidentifier] NULL,
	[ProcurementStatusID] [int] NULL,
	[ProcurementMethodID] [uniqueidentifier] NULL,
	[SourceOfFundID] [uniqueidentifier] NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
 CONSTRAINT [PK__Procurem__95B4518CA7560ADA] PRIMARY KEY CLUSTERED 
(
	[ProcurementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcurementStatus]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcurementStatus](
	[ProcurementStatusID] [int] NOT NULL,
	[Status] [varchar](50) NULL,
	[Description] [varchar](100) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[TenantID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProcurementStatus] PRIMARY KEY CLUSTERED 
(
	[ProcurementStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectCategory]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCategory](
	[ProjectCategoryID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectCategoryOrganizationSettings]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCategoryOrganizationSettings](
	[ProjectCategoryID] [uniqueidentifier] NOT NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
	[OrganizationID] [uniqueidentifier] NOT NULL,
	[EnableProjectCategory] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ProjectCategoryOrganizationSettings] PRIMARY KEY CLUSTERED 
(
	[ProjectCategoryID] ASC,
	[TenantID] ASC,
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestForDemo]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestForDemo](
	[RequestID] [uniqueidentifier] NOT NULL,
	[OrganizationFullName] [varchar](150) NULL,
	[OrganizationShortName] [varchar](10) NULL,
	[AdministratorEmail] [nvarchar](50) NULL,
	[AdministratorFirstName] [varchar](50) NULL,
	[AdministratorLastName] [varchar](50) NULL,
	[AdministratorPhoneNumber] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[OrganizationShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[OrganizationFullName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[AdministratorEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[AdministratorPhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceOfFunds]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceOfFunds](
	[SourceOfFundID] [uniqueidentifier] NOT NULL,
	[SourceOfFund] [varchar](200) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SourceOfFundID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceOfFundsOrganizationSettings]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceOfFundsOrganizationSettings](
	[SourceOfFundID] [uniqueidentifier] NOT NULL,
	[TenantID] [uniqueidentifier] NOT NULL,
	[OrganizationID] [uniqueidentifier] NOT NULL,
	[EnableSourceOFFund] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_SourceOfFundsOrganizationSettings] PRIMARY KEY CLUSTERED 
(
	[SourceOfFundID] ASC,
	[TenantID] ASC,
	[OrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TelephoneNumbers]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TelephoneNumbers](
	[TelephoneNumberID] [uniqueidentifier] NOT NULL,
	[TelephoneNumber] [varchar](50) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[OrganizationID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[TelephoneNumberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 9/19/2018 11:17:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserID] [uniqueidentifier] NOT NULL,
	[Id] [nvarchar](128) NULL,
	[DepartmentID] [uniqueidentifier] NULL,
	[UserEmail] [varchar](100) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[UserName] [varchar](50) NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[OrganizationID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/19/2018 11:17:58 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 9/19/2018 11:17:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 9/19/2018 11:17:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 9/19/2018 11:17:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 9/19/2018 11:17:58 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/19/2018 11:17:58 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Procurements] ADD  CONSTRAINT [DF_Procurements_ProcurementStatusID]  DEFAULT (N'4E844E8A-B2BB-4503-A5BA-32689AF5A2C6') FOR [ProcurementStatusID]
GO
ALTER TABLE [dbo].[AdvertCategory]  WITH CHECK ADD  CONSTRAINT [FK_AdvertCategory_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[AdvertCategory] CHECK CONSTRAINT [FK_AdvertCategory_Catalog]
GO
ALTER TABLE [dbo].[AdvertCategoryNumber]  WITH CHECK ADD FOREIGN KEY([AdvertID])
REFERENCES [dbo].[Adverts] ([AdvertID])
GO
ALTER TABLE [dbo].[AdvertCategoryNumber]  WITH CHECK ADD FOREIGN KEY([AdvertCategoryID])
REFERENCES [dbo].[AdvertCategory] ([AdvertCategoryID])
GO
ALTER TABLE [dbo].[AdvertCategoryNumber]  WITH CHECK ADD FOREIGN KEY([ProjectCategoryID])
REFERENCES [dbo].[ProjectCategory] ([ProjectCategoryID])
GO
ALTER TABLE [dbo].[AdvertCategoryNumber]  WITH CHECK ADD  CONSTRAINT [FK_AdvertCategoryNumber_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[AdvertCategoryNumber] CHECK CONSTRAINT [FK_AdvertCategoryNumber_Catalog]
GO
ALTER TABLE [dbo].[AdvertisedItems]  WITH CHECK ADD  CONSTRAINT [FK_AdvertisedItems_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[AdvertisedItems] CHECK CONSTRAINT [FK_AdvertisedItems_Catalog]
GO
ALTER TABLE [dbo].[AdvertLotNumber]  WITH CHECK ADD FOREIGN KEY([AdvertID])
REFERENCES [dbo].[Adverts] ([AdvertID])
GO
ALTER TABLE [dbo].[AdvertLotNumber]  WITH CHECK ADD  CONSTRAINT [FK__AdvertLot__Procu__403A8C7D] FOREIGN KEY([ProcurementID])
REFERENCES [dbo].[Procurements] ([ProcurementID])
GO
ALTER TABLE [dbo].[AdvertLotNumber] CHECK CONSTRAINT [FK__AdvertLot__Procu__403A8C7D]
GO
ALTER TABLE [dbo].[AdvertLotNumber]  WITH CHECK ADD  CONSTRAINT [FK_AdvertLotNumber_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[AdvertLotNumber] CHECK CONSTRAINT [FK_AdvertLotNumber_Catalog]
GO
ALTER TABLE [dbo].[Adverts]  WITH CHECK ADD FOREIGN KEY([AdvertStatusID])
REFERENCES [dbo].[AdvertStatus] ([AdvertStatusID])
GO
ALTER TABLE [dbo].[Adverts]  WITH CHECK ADD FOREIGN KEY([BudgetYearID])
REFERENCES [dbo].[BudgetYear] ([BudgetYearID])
GO
ALTER TABLE [dbo].[Adverts]  WITH CHECK ADD  CONSTRAINT [FK_Adverts_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[Adverts] CHECK CONSTRAINT [FK_Adverts_Catalog]
GO
ALTER TABLE [dbo].[AdvertStatus]  WITH CHECK ADD  CONSTRAINT [FK_AdvertStatus_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[AdvertStatus] CHECK CONSTRAINT [FK_AdvertStatus_Catalog]
GO
ALTER TABLE [dbo].[ApprovedItems]  WITH CHECK ADD  CONSTRAINT [FK_ApprovedItems_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[ApprovedItems] CHECK CONSTRAINT [FK_ApprovedItems_Catalog]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Catalog]
GO
ALTER TABLE [dbo].[BudgetYear]  WITH CHECK ADD  CONSTRAINT [FK_BudgetYear_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[BudgetYear] CHECK CONSTRAINT [FK_BudgetYear_Catalog]
GO
ALTER TABLE [dbo].[Catalog]  WITH CHECK ADD  CONSTRAINT [FK_Catalog_RequestForDemo] FOREIGN KEY([RequestID])
REFERENCES [dbo].[RequestForDemo] ([RequestID])
GO
ALTER TABLE [dbo].[Catalog] CHECK CONSTRAINT [FK_Catalog_RequestForDemo]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD FOREIGN KEY([DepartmentHeadUserID])
REFERENCES [dbo].[UserProfile] ([UserID])
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Catalog]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_OrganizationSettings] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_OrganizationSettings]
GO
ALTER TABLE [dbo].[ItemCode]  WITH CHECK ADD  CONSTRAINT [FK_ItemCode_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[ItemCode] CHECK CONSTRAINT [FK_ItemCode_Catalog]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([AdvertisedItemID])
REFERENCES [dbo].[AdvertisedItems] ([ItemID])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([ApprovedItemID])
REFERENCES [dbo].[ApprovedItems] ([ItemID])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([ItemCodeID])
REFERENCES [dbo].[ItemCode] ([ItemCodeID])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK__Items__Procureme__49C3F6B7] FOREIGN KEY([ProcurementID])
REFERENCES [dbo].[Procurements] ([ProcurementID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK__Items__Procureme__49C3F6B7]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Catalog]
GO
ALTER TABLE [dbo].[OrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationSettings_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[OrganizationSettings] CHECK CONSTRAINT [FK_OrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementMethodOrganizationSettings_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementMethodOrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementMethodOrganizationSettings_OrganizationSettings] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementMethodOrganizationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementMethodOrganizationSettings_ProcurementMethod] FOREIGN KEY([ProcurementMethodID])
REFERENCES [dbo].[ProcurementMethod] ([ProcurementMethodID])
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementMethodOrganizationSettings_ProcurementMethod]
GO
ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK__Procureme__Budge__4AB81AF0] FOREIGN KEY([BudgetYearID])
REFERENCES [dbo].[BudgetYear] ([BudgetYearID])
GO
ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK__Procureme__Budge__4AB81AF0]
GO
ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK__Procureme__Procu__4D94879B] FOREIGN KEY([ProcurementMethodID])
REFERENCES [dbo].[ProcurementMethod] ([ProcurementMethodID])
GO
ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK__Procureme__Procu__4D94879B]
GO
ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK__Procureme__Proje__4E88ABD4] FOREIGN KEY([ProjectCategoryID])
REFERENCES [dbo].[ProjectCategory] ([ProjectCategoryID])
GO
ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK__Procureme__Proje__4E88ABD4]
GO
ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK__Procureme__Sourc__4F7CD00D] FOREIGN KEY([SourceOfFundID])
REFERENCES [dbo].[SourceOfFunds] ([SourceOfFundID])
GO
ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK__Procureme__Sourc__4F7CD00D]
GO
ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK_Procurements_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK_Procurements_Catalog]
GO
ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK_Procurements_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK_Procurements_Department]
GO
ALTER TABLE [dbo].[Procurements]  WITH CHECK ADD  CONSTRAINT [FK_Procurements_ProcurementStatus] FOREIGN KEY([ProcurementStatusID])
REFERENCES [dbo].[ProcurementStatus] ([ProcurementStatusID])
GO
ALTER TABLE [dbo].[Procurements] CHECK CONSTRAINT [FK_Procurements_ProcurementStatus]
GO
ALTER TABLE [dbo].[ProcurementStatus]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementStatus_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[ProcurementStatus] CHECK CONSTRAINT [FK_ProcurementStatus_Catalog]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCategoryOrganizationSettings_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] CHECK CONSTRAINT [FK_ProjectCategoryOrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCategoryOrganizationSettings_OrganizationSettings] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] CHECK CONSTRAINT [FK_ProjectCategoryOrganizationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCategoryOrganizationSettings_ProjectCategory] FOREIGN KEY([ProjectCategoryID])
REFERENCES [dbo].[ProjectCategory] ([ProjectCategoryID])
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] CHECK CONSTRAINT [FK_ProjectCategoryOrganizationSettings_ProjectCategory]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_SourceOfFundsOrganizationSettings_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] CHECK CONSTRAINT [FK_SourceOfFundsOrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_SourceOfFundsOrganizationSettings_OrganizationSettings] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] CHECK CONSTRAINT [FK_SourceOfFundsOrganizationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_SourceOfFundsOrganizationSettings_SourceOfFunds] FOREIGN KEY([SourceOfFundID])
REFERENCES [dbo].[SourceOfFunds] ([SourceOfFundID])
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] CHECK CONSTRAINT [FK_SourceOfFundsOrganizationSettings_SourceOfFunds]
GO
ALTER TABLE [dbo].[TelephoneNumbers]  WITH CHECK ADD FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[TelephoneNumbers]  WITH CHECK ADD  CONSTRAINT [FK_TelephoneNumbers_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[TelephoneNumbers] CHECK CONSTRAINT [FK_TelephoneNumbers_Catalog]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Catalog]
GO
USE [master]
GO
ALTER DATABASE [ProcureEase] SET  READ_WRITE 
GO
