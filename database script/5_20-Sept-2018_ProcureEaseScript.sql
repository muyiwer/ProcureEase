USE [master]
GO
/****** Object:  Database [ProcureEase]    Script Date: 20/09/2018 08:00:54 ******/
CREATE DATABASE [ProcureEase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProcureEase', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProcureEase.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProcureEase_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProcureEase_log.ldf' , SIZE = 1856KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
USE [ProcureEase]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AdvertCategory]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AdvertCategoryNumber]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AdvertisedItems]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AdvertLotNumber]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[Adverts]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AdvertStatus]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[ApprovedItems]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[BudgetYear]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[Catalog]    Script Date: 20/09/2018 08:00:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[ItemCode]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[Items]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[OrganizationSettings]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[ProcurementMethod]    Script Date: 20/09/2018 08:00:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcurementMethod](
	[ProcurementMethodID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](300) NULL,
	[EnableProcurementMethod] [bit] NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK__Procurem__72F37E38C775944D] PRIMARY KEY CLUSTERED 
(
	[ProcurementMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcurementMethodOrganizationSettings]    Script Date: 20/09/2018 08:00:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcurementMethodOrganizationSettings](
	[ProcurementMethodID] [uniqueidentifier] NOT NULL,
	[OrganizationID] [uniqueidentifier] NULL,
	[TenantID] [uniqueidentifier] NULL,
	[EnableProcurementMethod] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ProcurementMethodOrganisationsettings] PRIMARY KEY CLUSTERED 
(
	[ProcurementMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Procurements]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[ProcurementStatus]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[ProjectCategory]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[ProjectCategoryOrganizationSettings]    Script Date: 20/09/2018 08:00:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCategoryOrganizationSettings](
	[ProjectCategoryID] [uniqueidentifier] NOT NULL,
	[OrganisationID] [uniqueidentifier] NULL,
	[EnableProjectCategory] [bit] NULL,
	[TenantID] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_ProcurementCategoryOrganisationSettings] PRIMARY KEY CLUSTERED 
(
	[ProjectCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestForDemo]    Script Date: 20/09/2018 08:00:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceOfFunds]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[SourceOfFundsOrganizationSettings]    Script Date: 20/09/2018 08:00:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceOfFundsOrganizationSettings](
	[SourceOfFundID] [uniqueidentifier] NOT NULL,
	[OrganisationID] [uniqueidentifier] NOT NULL,
	[EnableSourceOfFund] [bit] NULL,
	[TenantID] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_SourceOFFundsOrganisationSettings] PRIMARY KEY CLUSTERED 
(
	[SourceOfFundID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TelephoneNumbers]    Script Date: 20/09/2018 08:00:54 ******/
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
/****** Object:  Table [dbo].[UserProfile]    Script Date: 20/09/2018 08:00:54 ******/
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
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4217878b-4bd7-4bf0-904f-c5c874a96184', N'Employee')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b2d97f08-3e42-44ed-a9e7-1434e9a7aa2c', N'Head of Department')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'75816c21-6fdd-41ce-bc6f-5b5b1758774c', N'MDA Administrator')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a9a0b6a9-4426-46fd-bc20-83aeec34049f', N'Procurement Head')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9e8a17d0-4e17-41a6-b97b-b6cd72be43f8', N'Procurement Officer')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Date_Created]) VALUES (N'ab1dba18-abfa-401d-9031-d08a268c0a81', N'4217878b-4bd7-4bf0-904f-c5c874a96184', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'ab1dba18-abfa-401d-9031-d08a268c0a81', N'oaro@techspecialistlimited.com', 1, N'AOjE2AoHiafvTm3xZB5Ikxb0VYL6P6J0WYCCH+CatojOboWS8z3YgX1AABF0ZvUyQw==', N'17f66f88-ae9a-41b2-b894-9d32fe6aa572', NULL, 0, 0, NULL, 0, 0, N'oaro@techspecialistlimited.com', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'cced49d4-1f71-4087-b0fd-9f0c9477ffc3', N'muyiweraro@gmail.com', 1, N'AKQn0vTBHsE7ATKY/6fcapV4Uq/khoIVx6pREqL4hu6Lz4JtBnurIBJ1W0h3PFKfuw==', N'33d400b3-8704-43e8-a20b-9d208e7370fa', NULL, 0, 0, NULL, 0, 0, N'muyiweraro@gmail.com', NULL)
INSERT [dbo].[BudgetYear] ([BudgetYearID], [BudgetYear], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'724d9571-b1ed-4c56-abc5-987ab75d0f03', CAST(N'2018-01-01' AS Date), NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[BudgetYear] ([BudgetYearID], [BudgetYear], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'16747906-808a-419c-a9b7-b156121af97c', CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[Catalog] ([TenantID], [RequestID], [SubDomain], [OrganizationID], [DateCreated], [DateModified]) VALUES (N'5879c501-ba3a-4d8f-a02d-83e1b9c583c8', NULL, N'nitda', N'0a3de4a2-2df2-4a0a-a631-339717004c40', NULL, NULL)
INSERT [dbo].[Catalog] ([TenantID], [RequestID], [SubDomain], [OrganizationID], [DateCreated], [DateModified]) VALUES (N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6', NULL, N'localhost', N'a9a513fc-9f55-40ea-8bef-cd0a217094a2', NULL, NULL)
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'8defc11d-5595-41dd-87a9-2a0ef5fe04b8', N'Information Technology', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6', N'a9a513fc-9f55-40ea-8bef-cd0a217094a2')
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'09c9de6e-5d1b-4e77-bf80-a44af73d7e7a', N'Procurement', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6', N'a9a513fc-9f55-40ea-8bef-cd0a217094a2')
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'0b6a0615-f453-4e8c-af68-b799117a8b1a', N'Human Resources', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6', N'a9a513fc-9f55-40ea-8bef-cd0a217094a2')
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'57011490-001a-482a-8afe-3e445c8fc9f0', N'DW0100', N'Building demolition and wrecking work and earthmoving work.', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'a7eeaa33-0f9a-43bb-98bb-b9b999191b2c', N'IT0401', N'Consultant', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'05b5775d-6400-4faa-8ac3-bd1edcab1cc0', N'IT0100', N'Hardware', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[Items] ([ItemID], [ProcurementID], [ApprovedItemID], [AdvertisedItemID], [ItemName], [Description], [Quantity], [UnitPrice], [CreatedBy], [DateCreated], [DateModified], [ItemCodeID], [TenantID]) VALUES (N'fd1597b6-e857-4530-8b32-16fd6592cfc8', N'd4c85763-a4e7-43d4-aa1a-4d1c1e1e1eaf', NULL, NULL, N'brazillian wig', NULL, 13, 200000, NULL, CAST(N'2018-09-07T00:00:00.000' AS DateTime), CAST(N'2018-09-11T00:00:00.000' AS DateTime), N'05b5775d-6400-4faa-8ac3-bd1edcab1cc0', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[Items] ([ItemID], [ProcurementID], [ApprovedItemID], [AdvertisedItemID], [ItemName], [Description], [Quantity], [UnitPrice], [CreatedBy], [DateCreated], [DateModified], [ItemCodeID], [TenantID]) VALUES (N'848b0648-22e8-489c-8e60-9976325861ba', N'd4c85763-a4e7-43d4-aa1a-4d1c1e1e1eaf', NULL, NULL, N'brazillian wig', NULL, 13, 200000, NULL, CAST(N'2018-09-07T00:00:00.000' AS DateTime), CAST(N'2018-09-11T00:00:00.000' AS DateTime), N'05b5775d-6400-4faa-8ac3-bd1edcab1cc0', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[OrganizationSettings] ([OrganizationID], [OrganizationNameInFull], [OrganizationNameAbbreviation], [OrganizationEmail], [Address], [Country], [State], [AboutOrganization], [DateModified], [CreatedBy], [DateCreated], [OrganizationLogoPath], [TenantID]) VALUES (N'0a3de4a2-2df2-4a0a-a631-339717004c40', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'5879c501-ba3a-4d8f-a02d-83e1b9c583c8')
INSERT [dbo].[OrganizationSettings] ([OrganizationID], [OrganizationNameInFull], [OrganizationNameAbbreviation], [OrganizationEmail], [Address], [Country], [State], [AboutOrganization], [DateModified], [CreatedBy], [DateCreated], [OrganizationLogoPath], [TenantID]) VALUES (N'a9a513fc-9f55-40ea-8bef-cd0a217094a2', N'National Communication Commission', N'NCC', N'ncc@gmail.com', N'ssssss', N'sssss', N'sssssss', NULL, CAST(N'2018-09-03T14:06:36.360' AS DateTime), N'Admin', CAST(N'2018-09-03T14:06:36.360' AS DateTime), NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [EnableProcurementMethod], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'4aaf793e-965b-4c0a-8cc3-9957ae52eacc', N'Direct procurement', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [EnableProcurementMethod], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'822b9a65-7228-4152-8d14-9a50754e64c4', N'Selective tendering', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [EnableProcurementMethod], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'e6b9ae63-8c88-443b-a7f7-c98c89c9d08f', N'open competitive method', NULL, NULL, NULL, NULL)
INSERT [dbo].[Procurements] ([ProcurementID], [BudgetYearID], [DepartmentID], [ProjectName], [ProjectCategoryID], [ProcurementStatusID], [ProcurementMethodID], [SourceOfFundID], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'd4c85763-a4e7-43d4-aa1a-4d1c1e1e1eaf', N'724d9571-b1ed-4c56-abc5-987ab75d0f03', N'8defc11d-5595-41dd-87a9-2a0ef5fe04b8', N'Procurement of Android phones', N'c4a8c27a-e46e-4b59-ad0f-aa44761248f0', 1, N'4aaf793e-965b-4c0a-8cc3-9957ae52eacc', NULL, CAST(N'2018-09-11T00:00:00.000' AS DateTime), NULL, CAST(N'2018-09-07T00:00:00.000' AS DateTime), N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (1, N'Draft', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (2, N'Pending', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (3, N'NotApproved', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (4, N'Approved', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (5, N'Attested', NULL, NULL, NULL, NULL, N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'321b715e-c947-4458-8426-051f911fb8c6', N'Consultancy', NULL, NULL, NULL)
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'1e5de2ca-c79f-4a79-a03c-9cdb18a6ba86', N'Works', NULL, NULL, NULL)
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'c4a8c27a-e46e-4b59-ad0f-aa44761248f0', N'Goods', NULL, NULL, NULL)
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'2e0b33ec-59ec-4286-9279-d723e060a253', N'Services', NULL, NULL, NULL)
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'6bd968e4-1a30-44f6-b8c6-aa5e39924aa9', NULL, NULL, N'aajekuko@techspecialistlimited.com', NULL, NULL, NULL, NULL, NULL, NULL, N'a9a513fc-9f55-40ea-8bef-cd0a217094a2', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'472bad81-ff85-4be6-9238-bc8306493cb9', N'cced49d4-1f71-4087-b0fd-9f0c9477ffc3', N'0b6a0615-f453-4e8c-af68-b799117a8b1a', N'muyiweraro@gmail.com', N'Femi', N'Aro', NULL, NULL, NULL, NULL, N'a9a513fc-9f55-40ea-8bef-cd0a217094a2', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'c300ee65-9425-445f-b557-d0ee9dff55df', NULL, N'8defc11d-5595-41dd-87a9-2a0ef5fe04b8', N'email-1258815247@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, N'a9a513fc-9f55-40ea-8bef-cd0a217094a2', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'48bc21eb-3888-4ecc-9857-e447d157c46d', N'ab1dba18-abfa-401d-9031-d08a268c0a81', N'0b6a0615-f453-4e8c-af68-b799117a8b1a', N'oaro@techspecialistlimited.com', N'Olumuyiwa', N'Aro', NULL, NULL, NULL, NULL, N'a9a513fc-9f55-40ea-8bef-cd0a217094a2', N'8fcbe91f-0afd-4431-bce9-9bc9c75b35f6')
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 20/09/2018 08:00:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 08:00:55 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 08:00:55 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 20/09/2018 08:00:55 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 08:00:55 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 20/09/2018 08:00:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Catalog__7499EE10BB558AAF]    Script Date: 20/09/2018 08:00:55 ******/
ALTER TABLE [dbo].[Catalog] ADD UNIQUE NONCLUSTERED 
(
	[SubDomain] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__1736EFFEA3F55419]    Script Date: 20/09/2018 08:00:55 ******/
ALTER TABLE [dbo].[RequestForDemo] ADD UNIQUE NONCLUSTERED 
(
	[OrganizationShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__2356C68A987F276A]    Script Date: 20/09/2018 08:00:55 ******/
ALTER TABLE [dbo].[RequestForDemo] ADD UNIQUE NONCLUSTERED 
(
	[OrganizationFullName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__6E2AFC2A7FFC8446]    Script Date: 20/09/2018 08:00:55 ******/
ALTER TABLE [dbo].[RequestForDemo] ADD UNIQUE NONCLUSTERED 
(
	[AdministratorEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__A86C39D76D82B373]    Script Date: 20/09/2018 08:00:55 ******/
ALTER TABLE [dbo].[RequestForDemo] ADD UNIQUE NONCLUSTERED 
(
	[AdministratorPhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE [dbo].[Catalog]  WITH CHECK ADD FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[Catalog]  WITH CHECK ADD FOREIGN KEY([RequestID])
REFERENCES [dbo].[RequestForDemo] ([RequestID])
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
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementMethodOrganisationsettings_OrganizationSettings] FOREIGN KEY([OrganizationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementMethodOrganisationsettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementMethodOrganisationsettings_ProcurementMethod] FOREIGN KEY([ProcurementMethodID])
REFERENCES [dbo].[ProcurementMethod] ([ProcurementMethodID])
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementMethodOrganisationsettings_ProcurementMethod]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementMethodOrganizationSettings_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementMethodOrganizationSettings_Catalog]
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
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementCategoryOrganisationSettings_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementCategoryOrganisationSettings_Catalog]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementCategoryOrganisationSettings_OrganizationSettings] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementCategoryOrganisationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_ProcurementCategoryOrganisationSettings_ProjectCategory] FOREIGN KEY([ProjectCategoryID])
REFERENCES [dbo].[ProjectCategory] ([ProjectCategoryID])
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] CHECK CONSTRAINT [FK_ProcurementCategoryOrganisationSettings_ProjectCategory]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_SourceOFFundsOrganisationSettings_Catalog] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Catalog] ([TenantID])
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] CHECK CONSTRAINT [FK_SourceOFFundsOrganisationSettings_Catalog]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_SourceOFFundsOrganisationSettings_OrganizationSettings] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[OrganizationSettings] ([OrganizationID])
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] CHECK CONSTRAINT [FK_SourceOFFundsOrganisationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings]  WITH CHECK ADD  CONSTRAINT [FK_SourceOFFundsOrganisationSettings_SourceOfFunds] FOREIGN KEY([SourceOfFundID])
REFERENCES [dbo].[SourceOfFunds] ([SourceOfFundID])
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] CHECK CONSTRAINT [FK_SourceOFFundsOrganisationSettings_SourceOfFunds]
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
