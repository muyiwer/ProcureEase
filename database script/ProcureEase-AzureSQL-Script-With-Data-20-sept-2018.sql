USE [ProcureEase]
GO
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_Catalog]
GO
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK__UserProfile__Id__3864608B]
GO
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK__UserProfi__Organ__37703C52]
GO
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK__UserProfi__Depar__367C1819]
GO
ALTER TABLE [dbo].[TelephoneNumbers] DROP CONSTRAINT [FK_TelephoneNumbers_Catalog]
GO
ALTER TABLE [dbo].[TelephoneNumbers] DROP CONSTRAINT [FK__Telephone__Organ__3493CFA7]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] DROP CONSTRAINT [FK_SourceOfFundsOrganizationSettings_SourceOfFunds]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] DROP CONSTRAINT [FK_SourceOfFundsOrganizationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[SourceOfFundsOrganizationSettings] DROP CONSTRAINT [FK_SourceOfFundsOrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] DROP CONSTRAINT [FK_ProjectCategoryOrganizationSettings_ProjectCategory]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] DROP CONSTRAINT [FK_ProjectCategoryOrganizationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[ProjectCategoryOrganizationSettings] DROP CONSTRAINT [FK_ProjectCategoryOrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[ProcurementStatus] DROP CONSTRAINT [FK_ProcurementStatus_Catalog]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK_Procurements_ProcurementStatus]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK_Procurements_Department]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK_Procurements_Catalog]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK__Procureme__Sourc__4F7CD00D]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK__Procureme__Proje__4E88ABD4]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK__Procureme__Procu__4D94879B]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [FK__Procureme__Budge__4AB81AF0]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] DROP CONSTRAINT [FK_ProcurementMethodOrganizationSettings_ProcurementMethod]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] DROP CONSTRAINT [FK_ProcurementMethodOrganizationSettings_OrganizationSettings]
GO
ALTER TABLE [dbo].[ProcurementMethodOrganizationSettings] DROP CONSTRAINT [FK_ProcurementMethodOrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[OrganizationSettings] DROP CONSTRAINT [FK_OrganizationSettings_Catalog]
GO
ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK_Items_Catalog]
GO
ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK__Items__Procureme__49C3F6B7]
GO
ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK__Items__ItemCodeI__208CD6FA]
GO
ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK__Items__ApprovedI__1F98B2C1]
GO
ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK__Items__Advertise__1EA48E88]
GO
ALTER TABLE [dbo].[ItemCode] DROP CONSTRAINT [FK_ItemCode_Catalog]
GO
ALTER TABLE [dbo].[Department] DROP CONSTRAINT [FK_Department_OrganizationSettings]
GO
ALTER TABLE [dbo].[Department] DROP CONSTRAINT [FK_Department_Catalog]
GO
ALTER TABLE [dbo].[Department] DROP CONSTRAINT [FK__Departmen__Depar__1AD3FDA4]
GO
ALTER TABLE [dbo].[Catalog] DROP CONSTRAINT [FK_Catalog_RequestForDemo]
GO
ALTER TABLE [dbo].[BudgetYear] DROP CONSTRAINT [FK_BudgetYear_Catalog]
GO
ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Catalog]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ApprovedItems] DROP CONSTRAINT [FK_ApprovedItems_Catalog]
GO
ALTER TABLE [dbo].[AdvertStatus] DROP CONSTRAINT [FK_AdvertStatus_Catalog]
GO
ALTER TABLE [dbo].[Adverts] DROP CONSTRAINT [FK_Adverts_Catalog]
GO
ALTER TABLE [dbo].[Adverts] DROP CONSTRAINT [FK__Adverts__BudgetY__10566F31]
GO
ALTER TABLE [dbo].[Adverts] DROP CONSTRAINT [FK__Adverts__AdvertS__0F624AF8]
GO
ALTER TABLE [dbo].[AdvertLotNumber] DROP CONSTRAINT [FK_AdvertLotNumber_Catalog]
GO
ALTER TABLE [dbo].[AdvertLotNumber] DROP CONSTRAINT [FK__AdvertLot__Procu__403A8C7D]
GO
ALTER TABLE [dbo].[AdvertLotNumber] DROP CONSTRAINT [FK__AdvertLot__Adver__0C85DE4D]
GO
ALTER TABLE [dbo].[AdvertisedItems] DROP CONSTRAINT [FK_AdvertisedItems_Catalog]
GO
ALTER TABLE [dbo].[AdvertCategoryNumber] DROP CONSTRAINT [FK_AdvertCategoryNumber_Catalog]
GO
ALTER TABLE [dbo].[AdvertCategoryNumber] DROP CONSTRAINT [FK__AdvertCat__Proje__09A971A2]
GO
ALTER TABLE [dbo].[AdvertCategoryNumber] DROP CONSTRAINT [FK__AdvertCat__Adver__08B54D69]
GO
ALTER TABLE [dbo].[AdvertCategoryNumber] DROP CONSTRAINT [FK__AdvertCat__Adver__07C12930]
GO
ALTER TABLE [dbo].[AdvertCategory] DROP CONSTRAINT [FK_AdvertCategory_Catalog]
GO
ALTER TABLE [dbo].[Procurements] DROP CONSTRAINT [DF_Procurements_ProcurementStatusID]
GO
/****** Object:  Index [UQ__RequestF__A86C39D7895676DC]    Script Date: 20/09/2018 8:35:08 AM ******/
ALTER TABLE [dbo].[RequestForDemo] DROP CONSTRAINT [UQ__RequestF__A86C39D7895676DC]
GO
/****** Object:  Index [UQ__RequestF__6E2AFC2AB8DEE445]    Script Date: 20/09/2018 8:35:08 AM ******/
ALTER TABLE [dbo].[RequestForDemo] DROP CONSTRAINT [UQ__RequestF__6E2AFC2AB8DEE445]
GO
/****** Object:  Index [UQ__RequestF__2356C68A90C6B57E]    Script Date: 20/09/2018 8:35:08 AM ******/
ALTER TABLE [dbo].[RequestForDemo] DROP CONSTRAINT [UQ__RequestF__2356C68A90C6B57E]
GO
/****** Object:  Index [UQ__RequestF__1736EFFE00311490]    Script Date: 20/09/2018 8:35:08 AM ******/
ALTER TABLE [dbo].[RequestForDemo] DROP CONSTRAINT [UQ__RequestF__1736EFFE00311490]
GO
/****** Object:  Index [UQ__Catalog__7499EE10FCF426BA]    Script Date: 20/09/2018 8:35:08 AM ******/
ALTER TABLE [dbo].[Catalog] DROP CONSTRAINT [UQ__Catalog__7499EE10FCF426BA]
GO
/****** Object:  Index [UserNameIndex]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[UserProfile]
GO
/****** Object:  Table [dbo].[TelephoneNumbers]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[TelephoneNumbers]
GO
/****** Object:  Table [dbo].[SourceOfFundsOrganizationSettings]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[SourceOfFundsOrganizationSettings]
GO
/****** Object:  Table [dbo].[SourceOfFunds]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[SourceOfFunds]
GO
/****** Object:  Table [dbo].[RequestForDemo]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[RequestForDemo]
GO
/****** Object:  Table [dbo].[ProjectCategoryOrganizationSettings]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[ProjectCategoryOrganizationSettings]
GO
/****** Object:  Table [dbo].[ProjectCategory]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[ProjectCategory]
GO
/****** Object:  Table [dbo].[ProcurementStatus]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[ProcurementStatus]
GO
/****** Object:  Table [dbo].[Procurements]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[Procurements]
GO
/****** Object:  Table [dbo].[ProcurementMethodOrganizationSettings]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[ProcurementMethodOrganizationSettings]
GO
/****** Object:  Table [dbo].[ProcurementMethod]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[ProcurementMethod]
GO
/****** Object:  Table [dbo].[OrganizationSettings]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[OrganizationSettings]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[Items]
GO
/****** Object:  Table [dbo].[ItemCode]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[ItemCode]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[Catalog]
GO
/****** Object:  Table [dbo].[BudgetYear]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[BudgetYear]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[ApprovedItems]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[ApprovedItems]
GO
/****** Object:  Table [dbo].[AdvertStatus]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AdvertStatus]
GO
/****** Object:  Table [dbo].[Adverts]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[Adverts]
GO
/****** Object:  Table [dbo].[AdvertLotNumber]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AdvertLotNumber]
GO
/****** Object:  Table [dbo].[AdvertisedItems]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AdvertisedItems]
GO
/****** Object:  Table [dbo].[AdvertCategoryNumber]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AdvertCategoryNumber]
GO
/****** Object:  Table [dbo].[AdvertCategory]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[AdvertCategory]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO
USE [master]
GO
/****** Object:  Database [ProcureEase]    Script Date: 20/09/2018 8:35:08 AM ******/
DROP DATABASE [ProcureEase]
GO
/****** Object:  Database [ProcureEase]    Script Date: 20/09/2018 8:35:08 AM ******/
CREATE DATABASE [ProcureEase]
GO
ALTER DATABASE [ProcureEase] SET COMPATIBILITY_LEVEL = 140
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
ALTER DATABASE [ProcureEase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProcureEase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProcureEase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProcureEase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProcureEase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProcureEase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProcureEase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProcureEase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProcureEase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProcureEase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProcureEase] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [ProcureEase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProcureEase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ProcureEase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProcureEase] SET  MULTI_USER 
GO
ALTER DATABASE [ProcureEase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProcureEase] SET ENCRYPTION ON
GO
ALTER DATABASE [ProcureEase] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProcureEase] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [ProcureEase]
GO
ALTER DATABASE SCOPED CONFIGURATION SET DISABLE_BATCH_MODE_ADAPTIVE_JOINS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET DISABLE_INTERLEAVED_EXECUTION_TVF = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET GLOBAL_TEMPORARY_TABLE_AUTO_DROP = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ISOLATE_SECURITY_POLICY_CARDINALITY = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_PROCEDURE_EXECUTION_STATISTICS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_QUERY_EXECUTION_STATISTICS = OFF;
GO
USE [ProcureEase]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 20/09/2018 8:35:12 AM ******/
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
/****** Object:  Table [dbo].[AdvertCategory]    Script Date: 20/09/2018 8:35:13 AM ******/
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
/****** Object:  Table [dbo].[AdvertCategoryNumber]    Script Date: 20/09/2018 8:35:13 AM ******/
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
/****** Object:  Table [dbo].[AdvertisedItems]    Script Date: 20/09/2018 8:35:13 AM ******/
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
/****** Object:  Table [dbo].[AdvertLotNumber]    Script Date: 20/09/2018 8:35:14 AM ******/
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
/****** Object:  Table [dbo].[Adverts]    Script Date: 20/09/2018 8:35:14 AM ******/
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
/****** Object:  Table [dbo].[AdvertStatus]    Script Date: 20/09/2018 8:35:14 AM ******/
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
/****** Object:  Table [dbo].[ApprovedItems]    Script Date: 20/09/2018 8:35:14 AM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 20/09/2018 8:35:15 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 20/09/2018 8:35:15 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 20/09/2018 8:35:15 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 20/09/2018 8:35:16 AM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 20/09/2018 8:35:16 AM ******/
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
/****** Object:  Table [dbo].[BudgetYear]    Script Date: 20/09/2018 8:35:16 AM ******/
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
/****** Object:  Table [dbo].[Catalog]    Script Date: 20/09/2018 8:35:16 AM ******/
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
/****** Object:  Table [dbo].[Department]    Script Date: 20/09/2018 8:35:17 AM ******/
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
/****** Object:  Table [dbo].[ItemCode]    Script Date: 20/09/2018 8:35:17 AM ******/
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
/****** Object:  Table [dbo].[Items]    Script Date: 20/09/2018 8:35:17 AM ******/
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
/****** Object:  Table [dbo].[OrganizationSettings]    Script Date: 20/09/2018 8:35:18 AM ******/
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
/****** Object:  Table [dbo].[ProcurementMethod]    Script Date: 20/09/2018 8:35:18 AM ******/
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
/****** Object:  Table [dbo].[ProcurementMethodOrganizationSettings]    Script Date: 20/09/2018 8:35:18 AM ******/
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
/****** Object:  Table [dbo].[Procurements]    Script Date: 20/09/2018 8:35:19 AM ******/
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
/****** Object:  Table [dbo].[ProcurementStatus]    Script Date: 20/09/2018 8:35:19 AM ******/
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
/****** Object:  Table [dbo].[ProjectCategory]    Script Date: 20/09/2018 8:35:19 AM ******/
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
/****** Object:  Table [dbo].[ProjectCategoryOrganizationSettings]    Script Date: 20/09/2018 8:35:20 AM ******/
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
/****** Object:  Table [dbo].[RequestForDemo]    Script Date: 20/09/2018 8:35:20 AM ******/
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
/****** Object:  Table [dbo].[SourceOfFunds]    Script Date: 20/09/2018 8:35:20 AM ******/
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
/****** Object:  Table [dbo].[SourceOfFundsOrganizationSettings]    Script Date: 20/09/2018 8:35:20 AM ******/
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
/****** Object:  Table [dbo].[TelephoneNumbers]    Script Date: 20/09/2018 8:35:21 AM ******/
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
/****** Object:  Table [dbo].[UserProfile]    Script Date: 20/09/2018 8:35:21 AM ******/
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
INSERT [dbo].[AdvertCategory] ([AdvertCategoryID], [AdvertCategory], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'3c8b2333-516d-407a-9deb-292185e065ed', N'Works', N'MDA Administrator', CAST(N'2018-08-14T14:31:49.843' AS DateTime), CAST(N'2018-08-14T14:31:49.843' AS DateTime), NULL)
GO
INSERT [dbo].[AdvertCategory] ([AdvertCategoryID], [AdvertCategory], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'b0159106-7ee9-449e-a7a0-4c9e2957f53f', N'Goods', N'MDA Administrator', CAST(N'2018-08-14T14:31:43.117' AS DateTime), CAST(N'2018-08-14T14:31:43.117' AS DateTime), NULL)
GO
INSERT [dbo].[AdvertCategory] ([AdvertCategoryID], [AdvertCategory], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'e699fbfc-8ce4-4fc2-94a3-d8a064ee20e6', N'Services', N'MDA Administrator', CAST(N'2018-08-14T14:32:20.560' AS DateTime), CAST(N'2018-08-14T14:32:20.560' AS DateTime), NULL)
GO
INSERT [dbo].[AdvertCategory] ([AdvertCategoryID], [AdvertCategory], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'6864fc21-bee1-440c-8770-df326eb8fd75', N'Commodities', N'MDA Administrator', CAST(N'2018-08-14T14:31:59.233' AS DateTime), CAST(N'2018-08-14T14:31:59.233' AS DateTime), NULL)
GO
INSERT [dbo].[AdvertCategoryNumber] ([AdvertCategoryNumberID], [AdvertID], [AdvertCategoryID], [ProjectCategoryID], [CategoryLotCode], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'79d58b15-be5b-4ae2-97ea-72cdbda1d437', N'b9571bd0-bcbe-4817-a22e-41d1fc9e82fd', N'3c8b2333-516d-407a-9deb-292185e065ed', N'dffa4e85-ea93-4958-bee5-f1b97349625e', N'W1', N'Employee', CAST(N'2018-08-27T16:43:51.313' AS DateTime), CAST(N'2018-08-30T16:14:20.667' AS DateTime), NULL)
GO
INSERT [dbo].[AdvertCategoryNumber] ([AdvertCategoryNumberID], [AdvertID], [AdvertCategoryID], [ProjectCategoryID], [CategoryLotCode], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'990ea758-d00a-4160-b808-b521f9ba85e9', N'b9571bd0-bcbe-4817-a22e-41d1fc9e82fd', N'b0159106-7ee9-449e-a7a0-4c9e2957f53f', N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', N'G1', N'Employee', CAST(N'2018-08-27T16:43:51.313' AS DateTime), CAST(N'2018-08-30T16:14:20.667' AS DateTime), NULL)
GO
INSERT [dbo].[AdvertLotNumber] ([AdvertLotNumberID], [AdvertID], [ProcurementID], [LotCode], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'0a53fc1f-d026-4644-819d-191cdd7b5af4', N'b9571bd0-bcbe-4817-a22e-41d1fc9e82fd', N'bf1ea654-8bfc-4841-8448-74676a348c88', N'W1', N'Employee', CAST(N'2018-08-27T16:43:51.313' AS DateTime), CAST(N'2018-08-30T16:14:20.667' AS DateTime), NULL)
GO
INSERT [dbo].[AdvertLotNumber] ([AdvertLotNumberID], [AdvertID], [ProcurementID], [LotCode], [CreatedBy], [DateCreated], [DateModified], [TenantID]) VALUES (N'fc85d1b5-0834-4fa8-8dc8-5b65b167df40', N'b9571bd0-bcbe-4817-a22e-41d1fc9e82fd', N'57e10d3f-9e42-4abf-99f4-789f422d65bd', N'G1', N'Employee', CAST(N'2018-08-27T16:43:51.313' AS DateTime), CAST(N'2018-08-30T16:14:20.667' AS DateTime), NULL)
GO
INSERT [dbo].[Adverts] ([AdvertID], [BudgetYearID], [Headline], [Introduction], [ScopeOfWork], [EligibiltyRequirement], [CollectionOfTenderDocument], [BidSubmission], [OtherImportantInformation], [BidOpeningDate], [BidClosingDate], [CreatedBy], [DateCreated], [DateModified], [AdvertStatusID], [TenantID]) VALUES (N'b9571bd0-bcbe-4817-a22e-41d1fc9e82fd', N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', N'Invitation to tender for 2018 capital project', N'SUPPLY AND DELIVER 3000 HARD DRIVES (HDD) FOR MEERKAT ARCHIVE', N'5pieces of hard drive', N'Quality Hard drives', N'Tender document will be collected on 24th', N'Bid submission is on 31st', N'Bid submission is on 31st', CAST(N'2018-02-10T00:00:00.000' AS DateTime), CAST(N'2018-02-15T00:00:00.000' AS DateTime), N'Employee', CAST(N'2018-08-16T13:03:20.953' AS DateTime), CAST(N'2018-08-30T16:14:20.667' AS DateTime), 2, NULL)
GO
INSERT [dbo].[AdvertStatus] ([Status], [DateModified], [CreatedBy], [DateCreated], [AdvertStatusID], [TenantID]) VALUES (N'Draft', CAST(N'2018-08-14T00:00:00.000' AS DateTime), N'MDA Administrator', CAST(N'2018-08-14T00:00:00.000' AS DateTime), 1, NULL)
GO
INSERT [dbo].[AdvertStatus] ([Status], [DateModified], [CreatedBy], [DateCreated], [AdvertStatusID], [TenantID]) VALUES (N'Published', CAST(N'2018-08-14T00:00:00.000' AS DateTime), N'MDA Administrator', CAST(N'2018-08-14T00:00:00.000' AS DateTime), 2, NULL)
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd734d84d-3236-48d3-9704-749c656096f8', N'Employee')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'dcaae29b-f768-4272-b3df-b902336a39a7', N'Head of Department')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6c95e856-8b23-4b10-9d72-13022861e50f', N'MDA Administrator')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ab704e94-67f8-4c79-a28f-7332b488a689', N'Procurement Head')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6f029334-6803-4cd3-89f1-2f51c9751e7e', N'Procurement Officer')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Date_Created]) VALUES (N'4ad260d6-74f8-49a9-991b-6f01d8d0f48a', N'6f029334-6803-4cd3-89f1-2f51c9751e7e', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Date_Created]) VALUES (N'664a833e-2c6e-47dd-bbc6-ffdae71576c7', N'6f029334-6803-4cd3-89f1-2f51c9751e7e', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Date_Created]) VALUES (N'c59b1c88-d6c6-47f0-8290-28c9dc610e33', N'd734d84d-3236-48d3-9704-749c656096f8', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Date_Created]) VALUES (N'd3508442-df6e-4f46-ae26-abec281a3e96', N'6f029334-6803-4cd3-89f1-2f51c9751e7e', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'231da76e-91e5-4080-bfee-8e5b3f034a0b', N'oaro@techspecialistlimited.com', 1, N'AP0G5Q8teXUz2w28+Z6y7nKSUWCJyEOe7tlJiRmdTSMh9k+Q/jlqaJzB4gQwYEFFbw==', N'7ab8cd70-7b5a-464c-b07d-1faa16c15b3b', NULL, 0, 0, NULL, 0, 0, N'oaro@techspecialistlimited.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'2fcb9a3a-a7e9-40be-b1c6-e41a50101484', N'yolalere@techspecialistlimited.com', 1, N'ADuj86haR+MeiKixK79pbnR1jtKntek+y0aY0s/fsTChiZgHVQrn2+FifEJFQh43hg==', N'6305366f-2343-4381-b502-12897bc4da97', NULL, 0, 0, NULL, 0, 0, N'yolalere@techspecialistlimited.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'47cf4505-a349-4b6a-9ae7-081609365fff', N'annieajeks@gmail.com', 1, N'AJJDsMjVNv150vOPUyPyj8rQj1+kKAPSb56dp/JwOqx9qVZmYViASNdMXYq4Uv0CTQ==', N'ea57927c-e972-4cb6-957a-5f1d62061f1b', NULL, 0, 0, NULL, 0, 0, N'annieajeks@gmail.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'4ad260d6-74f8-49a9-991b-6f01d8d0f48a', N'oalabi@techspecialistlimited.com', 1, N'AMwwdKXGn3o+qqdQWSYUlqpX5kE7GAyLV6Gs82Tu2HuRfhLlCllJUbaFRAfPXuivDA==', N'4dd68677-afb1-4617-b014-fbbe9be8c518', NULL, 0, 0, NULL, 0, 0, N'oalabi@techspecialistlimited.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'642eb4cd-867f-4bd3-920b-ea52d4c4e740', N'fadepoju@techspecialistlimited.com', 1, N'AOUdA7Aw8jSIGPc1D9bQ66MdCNdbjxHEzTw74uNdslNezWGxOnWnqychRUM1eyT/bA==', N'd0d93ea1-18a9-4e87-a7e9-1cff19adac92', NULL, 0, 0, NULL, 0, 0, N'fadepoju@techspecialistlimited.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'664a833e-2c6e-47dd-bbc6-ffdae71576c7', N'muyiweraro@gmail.com', 1, N'ALY98OKhD0v6bwFULRpzsnW4FHHL3fglc+P4QfLEY0AUzCkN4bLY9B3BxyaTkZMC5g==', N'7f4033a0-fc8f-4570-98be-bf6c5d51d7cf', NULL, 0, 0, NULL, 0, 0, N'muyiweraro@gmail.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'c59b1c88-d6c6-47f0-8290-28c9dc610e33', N'gejegwa@techspecialistlimited.com', 1, N'AAbYE0yGOf51ECLOj7u7zKg4GwAAawLXjFb3FwdhJQeG1gMvP/rUiAK85si4haRLSQ==', N'73065516-f9b6-4aab-992e-8cbd332f926c', NULL, 0, 0, NULL, 0, 0, N'gejegwa@techspecialistlimited.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'd3508442-df6e-4f46-ae26-abec281a3e96', N'muyiwer@gmail.com', 1, N'AG1CjxXIo2LN8hZJVjAPuMXFkliY3BiKaTQDWljjOCthkMWt0QD+6m3UUEoJ6jliXA==', N'47e88bf3-57a3-4953-8fb3-557315d301fc', NULL, 0, 0, NULL, 0, 0, N'muyiwer@gmail.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [TenantID]) VALUES (N'e57ada7a-2d0b-4c78-9c6c-f492f9c92fc4', N'grayceecharles@gmail.com', 1, N'AKC0qmjZ5cYv8S9BDzNFBmKwp+Is5eiHdLCOy0ffBSJ9nX5ESXk06IVwUJg7Nos5VQ==', N'46dfcc82-b81b-4c9c-8056-412076f013e4', NULL, 0, 0, NULL, 0, 0, N'grayceecharles@gmail.com', NULL)
GO
INSERT [dbo].[BudgetYear] ([BudgetYearID], [BudgetYear], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'27fae32d-4163-4766-a710-09d519839134', CAST(N'2017-01-01' AS Date), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BudgetYear] ([BudgetYearID], [BudgetYear], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', CAST(N'2018-01-01' AS Date), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Catalog] ([TenantID], [RequestID], [SubDomain], [OrganizationID], [DateCreated], [DateModified]) VALUES (N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'f6c0b955-de16-4dba-b3f5-97e5bdf8686d', N'NCC', N'00496d49-3e29-48de-817c-199c88c3385d', CAST(N'2018-09-19T10:32:23.713' AS DateTime), CAST(N'2018-09-19T10:32:23.713' AS DateTime))
GO
INSERT [dbo].[Catalog] ([TenantID], [RequestID], [SubDomain], [OrganizationID], [DateCreated], [DateModified]) VALUES (N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'ad905cfb-8914-4f04-ba9c-003668bec91f', N'CBN', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', CAST(N'2018-09-19T10:29:14.203' AS DateTime), CAST(N'2018-09-19T10:29:14.203' AS DateTime))
GO
INSERT [dbo].[Catalog] ([TenantID], [RequestID], [SubDomain], [OrganizationID], [DateCreated], [DateModified]) VALUES (N'87d1123a-57ec-443c-8042-887720d46a81', N'8dc27a6f-2710-4c23-9d50-676b4f6828ef', N'localhost', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', CAST(N'2018-09-19T12:29:49.263' AS DateTime), CAST(N'2018-09-19T12:29:49.263' AS DateTime))
GO
INSERT [dbo].[Catalog] ([TenantID], [RequestID], [SubDomain], [OrganizationID], [DateCreated], [DateModified]) VALUES (N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'ebff2784-a3a1-4da5-bf26-6a5ff2168213', N'old NITDA', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', CAST(N'2018-09-19T11:38:23.663' AS DateTime), CAST(N'2018-09-19T11:38:23.663' AS DateTime))
GO
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'86caf117-37ed-4370-ac31-0d86eecad8ed', N'Procurement', CAST(N'2018-09-17T11:06:12.707' AS DateTime), NULL, NULL, N'e2f21fad-1f3d-477d-a818-bb74eb85ad4b', NULL, NULL)
GO
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'ba6de7f0-0353-4118-8ff8-76720caf9def', N'Technical Unit', CAST(N'2018-09-17T11:33:17.537' AS DateTime), N'MDA Administrator', CAST(N'2018-09-17T11:33:17.537' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'01f8bc72-673b-4343-9fd6-848e517fff8c', N'Technical', CAST(N'2018-09-17T12:09:44.537' AS DateTime), N'MDA Administrator', CAST(N'2018-09-13T12:07:43.073' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'd4d62ef9-1546-4681-98f5-8a1960455739', N'Procurement Unit', CAST(N'2018-09-17T11:45:53.020' AS DateTime), N'MDA Administrator', CAST(N'2018-09-13T12:12:00.657' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'3a380f88-6b8b-40af-aa14-df546cec1aa6', N'Information Technology', NULL, NULL, NULL, N'140ec11c-befd-4db3-aee6-0c45339d3f05', NULL, NULL)
GO
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName], [DateModified], [CreatedBy], [DateCreated], [DepartmentHeadUserID], [TenantID], [OrganisationID]) VALUES (N'b9511a1b-b695-4ad9-a514-e501e6931947', N'Technical', CAST(N'2018-09-17T11:35:43.927' AS DateTime), N'MDA Administrator', CAST(N'2018-09-17T11:35:43.927' AS DateTime), N'140ec11c-befd-4db3-aee6-0c45339d3f05', NULL, NULL)
GO
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'93a3d37e-7aa7-42fc-83aa-0605a4d51c34', N'DB0100', N'Test drilling and boring work.', NULL)
GO
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'df7c8c3b-ea8d-4861-aae0-324193f95c88', N'VS0100', N'Graphic design', NULL)
GO
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'93e6ffa5-e700-45b0-bde7-6b3570dab27b', N'IT0100', N'Hardware', NULL)
GO
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'725c4c3e-19d4-449b-8a40-79c64ff6806b', N'IT0200', N'Software', NULL)
GO
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'26a527ac-dda8-4513-928b-a121446ea56b', N'DW0100', N'Building demolition and wrecking work and earthmoving work.', NULL)
GO
INSERT [dbo].[ItemCode] ([ItemCodeID], [ItemCode], [ItemName], [TenantID]) VALUES (N'78ce56a3-6fb8-4c4a-95cb-ab017752d4c6', N'GB0200', N'Construction work for buildings relating to leisure, sports, culture, lodging and restaurants.', NULL)
GO
INSERT [dbo].[Items] ([ItemID], [ProcurementID], [ApprovedItemID], [AdvertisedItemID], [ItemName], [Description], [Quantity], [UnitPrice], [CreatedBy], [DateCreated], [DateModified], [ItemCodeID], [TenantID]) VALUES (N'5918acf1-64d8-4f71-8c33-72dba0c6efcd', N'4dcd296b-d962-4390-a783-cb045af7c4ed', NULL, NULL, N'Microsoft softwares', NULL, 11, 49500, NULL, NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), N'725c4c3e-19d4-449b-8a40-79c64ff6806b', NULL)
GO
INSERT [dbo].[Items] ([ItemID], [ProcurementID], [ApprovedItemID], [AdvertisedItemID], [ItemName], [Description], [Quantity], [UnitPrice], [CreatedBy], [DateCreated], [DateModified], [ItemCodeID], [TenantID]) VALUES (N'68457dbf-16b8-4ae7-b1b6-bb7bd56c2f23', N'16b31950-2525-42c8-8bab-8cb7a04ff613', NULL, NULL, N'brazillian wig', N'telephone waya', 13, 200000, NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), NULL, N'93e6ffa5-e700-45b0-bde7-6b3570dab27b', NULL)
GO
INSERT [dbo].[Items] ([ItemID], [ProcurementID], [ApprovedItemID], [AdvertisedItemID], [ItemName], [Description], [Quantity], [UnitPrice], [CreatedBy], [DateCreated], [DateModified], [ItemCodeID], [TenantID]) VALUES (N'b12cea25-83f1-434c-b0f7-c9008db181de', N'57e10d3f-9e42-4abf-99f4-789f422d65bd', NULL, NULL, N'kemi', NULL, 13, 200000, NULL, NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), N'93e6ffa5-e700-45b0-bde7-6b3570dab27b', NULL)
GO
INSERT [dbo].[Items] ([ItemID], [ProcurementID], [ApprovedItemID], [AdvertisedItemID], [ItemName], [Description], [Quantity], [UnitPrice], [CreatedBy], [DateCreated], [DateModified], [ItemCodeID], [TenantID]) VALUES (N'7eb310af-8758-4178-96a6-d9c13312461d', N'57e10d3f-9e42-4abf-99f4-789f422d65bd', NULL, NULL, N'server', NULL, 5, 100000, NULL, NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), N'93e6ffa5-e700-45b0-bde7-6b3570dab27b', NULL)
GO
INSERT [dbo].[OrganizationSettings] ([OrganizationID], [OrganizationNameInFull], [OrganizationNameAbbreviation], [OrganizationEmail], [Address], [Country], [State], [AboutOrganization], [DateModified], [CreatedBy], [DateCreated], [OrganizationLogoPath], [TenantID]) VALUES (N'00496d49-3e29-48de-817c-199c88c3385d', N'Nigeria Communication Commissions', N'NCC', NULL, NULL, NULL, NULL, NULL, NULL, N'Techspecialist', CAST(N'2018-09-19T10:32:24.350' AS DateTime), NULL, N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07')
GO
INSERT [dbo].[OrganizationSettings] ([OrganizationID], [OrganizationNameInFull], [OrganizationNameAbbreviation], [OrganizationEmail], [Address], [Country], [State], [AboutOrganization], [DateModified], [CreatedBy], [DateCreated], [OrganizationLogoPath], [TenantID]) VALUES (N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', N'Nigeria Information Technology Development Agency', N'NITDA', NULL, NULL, NULL, NULL, NULL, NULL, N'Techspecialist', CAST(N'2018-09-19T12:29:50.213' AS DateTime), NULL, N'87d1123a-57ec-443c-8042-887720d46a81')
GO
INSERT [dbo].[OrganizationSettings] ([OrganizationID], [OrganizationNameInFull], [OrganizationNameAbbreviation], [OrganizationEmail], [Address], [Country], [State], [AboutOrganization], [DateModified], [CreatedBy], [DateCreated], [OrganizationLogoPath], [TenantID]) VALUES (N'f9062059-3da6-40c7-82c9-b4ee6bebde91', N'Central Bank of Nigeria', N'CBN', NULL, NULL, NULL, NULL, NULL, NULL, N'Techspecialist', CAST(N'2018-09-19T10:29:14.967' AS DateTime), NULL, N'e0cf4891-7884-47ac-b4d7-4989473b1822')
GO
INSERT [dbo].[OrganizationSettings] ([OrganizationID], [OrganizationNameInFull], [OrganizationNameAbbreviation], [OrganizationEmail], [Address], [Country], [State], [AboutOrganization], [DateModified], [CreatedBy], [DateCreated], [OrganizationLogoPath], [TenantID]) VALUES (N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', N'Old Nigeria Information Technology Development Agency', N'NCC', NULL, NULL, NULL, NULL, NULL, NULL, N'Techspecialist', CAST(N'2018-09-19T11:38:24.770' AS DateTime), NULL, N'0bbf7136-c806-44b3-93d1-edec6b59ea6f')
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'1a843a96-3957-4bb8-af04-0771b40fe6d1', N'Selective Tendering', CAST(N'2018-09-10T17:36:13.157' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:36:13.157' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'40fdeeb4-e487-4639-acbd-09c85715970a', N'Direct procurement', CAST(N'2018-09-10T17:36:13.157' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:36:13.157' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'3eb133fb-2ce4-40e5-9c82-19db5d138879', N'Open Competitive method', CAST(N'2018-09-10T17:36:13.157' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:36:13.157' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'bf25023c-ee75-4b87-bfe0-54deea9131be', N'Single Source of procurement', CAST(N'2018-09-10T17:36:13.157' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:36:13.157' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'01c17809-f40e-453c-b14a-5fe6a9ae0611', N'Request for quotation', CAST(N'2018-09-10T17:30:22.530' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:30:22.530' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'36028b2b-67d8-4ae1-974c-69069bbad99c', N'National Competitive Bidding (NCB)', CAST(N'2018-09-10T17:32:25.950' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:32:25.950' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'9e459ebb-0e11-4066-8f0a-6a7088d3aae0', N'International Competitive Bidding (ICB)', CAST(N'2018-09-10T17:31:47.227' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:31:47.227' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'f1ffd4cb-8dc3-4035-ad92-71b7e15e88d5', N'Limited International Bidding (LIB)', CAST(N'2018-09-10T17:32:59.603' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:32:59.603' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'75f5ed55-4abc-4e58-9859-868ca4cdf423', N'Two stage tendering', CAST(N'2018-09-10T17:34:10.290' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:34:10.290' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'c449b0b2-4e1c-465b-a669-aafe37a92e1b', N'Limited National Bidding (LNB)', CAST(N'2018-09-10T17:33:23.563' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:33:23.563' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'6b3774a1-da66-4f46-adb6-b711b8d19f57', N'Emergency bidding', CAST(N'2018-09-10T17:36:13.157' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:36:13.157' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'8cfea721-d402-473d-9a2f-c03b4446b13f', N'Shopping (Market Survey)', CAST(N'2018-09-10T17:34:43.937' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:34:43.937' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'3d30c690-f37b-43d9-a17c-d47a4f5da127', N'Request for proposal', CAST(N'2018-09-10T17:36:13.157' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:36:13.157' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'0c2aba15-f24d-42dd-9010-e57fbeb4f4df', N'Least Cost', CAST(N'2018-09-10T17:35:33.457' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:35:33.457' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'e56b83a5-cf8f-49b2-b41d-f38c5f6392b5', N'Restricted', CAST(N'2018-09-10T17:33:44.277' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:33:44.277' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'58be8c26-e9f2-4dc8-91aa-fad9e67af87d', N'Fixed Cost', CAST(N'2018-09-10T17:35:46.863' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:35:46.863' AS DateTime))
GO
INSERT [dbo].[ProcurementMethod] ([ProcurementMethodID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'9a27692c-c2af-46e1-9038-fd59dacbbedf', N'Quality and Cost Based', CAST(N'2018-09-10T17:35:15.850' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T17:35:15.850' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'1a843a96-3957-4bb8-af04-0771b40fe6d1', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.643' AS DateTime), CAST(N'2018-09-19T10:32:24.643' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'1a843a96-3957-4bb8-af04-0771b40fe6d1', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.277' AS DateTime), CAST(N'2018-09-19T10:29:15.277' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'1a843a96-3957-4bb8-af04-0771b40fe6d1', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:56.557' AS DateTime), CAST(N'2018-09-19T12:29:56.557' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'1a843a96-3957-4bb8-af04-0771b40fe6d1', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:31.137' AS DateTime), CAST(N'2018-09-19T11:38:31.137' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'40fdeeb4-e487-4639-acbd-09c85715970a', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.643' AS DateTime), CAST(N'2018-09-19T10:32:24.643' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'40fdeeb4-e487-4639-acbd-09c85715970a', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.277' AS DateTime), CAST(N'2018-09-19T10:29:15.277' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'40fdeeb4-e487-4639-acbd-09c85715970a', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:56.557' AS DateTime), CAST(N'2018-09-19T12:29:56.557' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'40fdeeb4-e487-4639-acbd-09c85715970a', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:31.137' AS DateTime), CAST(N'2018-09-19T11:38:31.137' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3eb133fb-2ce4-40e5-9c82-19db5d138879', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.643' AS DateTime), CAST(N'2018-09-19T10:32:24.643' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3eb133fb-2ce4-40e5-9c82-19db5d138879', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.277' AS DateTime), CAST(N'2018-09-19T10:29:15.277' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3eb133fb-2ce4-40e5-9c82-19db5d138879', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:56.557' AS DateTime), CAST(N'2018-09-19T12:29:56.557' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3eb133fb-2ce4-40e5-9c82-19db5d138879', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:31.137' AS DateTime), CAST(N'2018-09-19T11:38:31.137' AS DateTime))
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'bf25023c-ee75-4b87-bfe0-54deea9131be', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'bf25023c-ee75-4b87-bfe0-54deea9131be', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'bf25023c-ee75-4b87-bfe0-54deea9131be', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'bf25023c-ee75-4b87-bfe0-54deea9131be', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'01c17809-f40e-453c-b14a-5fe6a9ae0611', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'01c17809-f40e-453c-b14a-5fe6a9ae0611', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'01c17809-f40e-453c-b14a-5fe6a9ae0611', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'01c17809-f40e-453c-b14a-5fe6a9ae0611', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'36028b2b-67d8-4ae1-974c-69069bbad99c', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'36028b2b-67d8-4ae1-974c-69069bbad99c', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'36028b2b-67d8-4ae1-974c-69069bbad99c', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'36028b2b-67d8-4ae1-974c-69069bbad99c', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9e459ebb-0e11-4066-8f0a-6a7088d3aae0', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9e459ebb-0e11-4066-8f0a-6a7088d3aae0', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9e459ebb-0e11-4066-8f0a-6a7088d3aae0', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9e459ebb-0e11-4066-8f0a-6a7088d3aae0', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'f1ffd4cb-8dc3-4035-ad92-71b7e15e88d5', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'f1ffd4cb-8dc3-4035-ad92-71b7e15e88d5', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'f1ffd4cb-8dc3-4035-ad92-71b7e15e88d5', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'f1ffd4cb-8dc3-4035-ad92-71b7e15e88d5', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'75f5ed55-4abc-4e58-9859-868ca4cdf423', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'75f5ed55-4abc-4e58-9859-868ca4cdf423', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'75f5ed55-4abc-4e58-9859-868ca4cdf423', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'75f5ed55-4abc-4e58-9859-868ca4cdf423', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'c449b0b2-4e1c-465b-a669-aafe37a92e1b', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'c449b0b2-4e1c-465b-a669-aafe37a92e1b', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'c449b0b2-4e1c-465b-a669-aafe37a92e1b', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'c449b0b2-4e1c-465b-a669-aafe37a92e1b', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'6b3774a1-da66-4f46-adb6-b711b8d19f57', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'6b3774a1-da66-4f46-adb6-b711b8d19f57', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'6b3774a1-da66-4f46-adb6-b711b8d19f57', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'6b3774a1-da66-4f46-adb6-b711b8d19f57', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'8cfea721-d402-473d-9a2f-c03b4446b13f', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'8cfea721-d402-473d-9a2f-c03b4446b13f', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'8cfea721-d402-473d-9a2f-c03b4446b13f', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'8cfea721-d402-473d-9a2f-c03b4446b13f', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3d30c690-f37b-43d9-a17c-d47a4f5da127', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3d30c690-f37b-43d9-a17c-d47a4f5da127', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3d30c690-f37b-43d9-a17c-d47a4f5da127', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'3d30c690-f37b-43d9-a17c-d47a4f5da127', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'0c2aba15-f24d-42dd-9010-e57fbeb4f4df', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'0c2aba15-f24d-42dd-9010-e57fbeb4f4df', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'0c2aba15-f24d-42dd-9010-e57fbeb4f4df', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'0c2aba15-f24d-42dd-9010-e57fbeb4f4df', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'e56b83a5-cf8f-49b2-b41d-f38c5f6392b5', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'e56b83a5-cf8f-49b2-b41d-f38c5f6392b5', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'e56b83a5-cf8f-49b2-b41d-f38c5f6392b5', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'e56b83a5-cf8f-49b2-b41d-f38c5f6392b5', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'58be8c26-e9f2-4dc8-91aa-fad9e67af87d', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'58be8c26-e9f2-4dc8-91aa-fad9e67af87d', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'58be8c26-e9f2-4dc8-91aa-fad9e67af87d', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'58be8c26-e9f2-4dc8-91aa-fad9e67af87d', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9a27692c-c2af-46e1-9038-fd59dacbbedf', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.643' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9a27692c-c2af-46e1-9038-fd59dacbbedf', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.277' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9a27692c-c2af-46e1-9038-fd59dacbbedf', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:56.557' AS DateTime), NULL)
GO
INSERT [dbo].[ProcurementMethodOrganizationSettings] ([ProcurementMethodID], [TenantID], [OrganizationID], [EnableProcurementMethod], [DateCreated], [DateModified]) VALUES (N'9a27692c-c2af-46e1-9038-fd59dacbbedf', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:31.137' AS DateTime), NULL)
GO
INSERT [dbo].[Procurements] ([ProcurementID], [BudgetYearID], [DepartmentID], [ProjectName], [ProjectCategoryID], [ProcurementStatusID], [ProcurementMethodID], [SourceOfFundID], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'35692df9-fedc-4ec7-9e1b-275d3b41e61f', N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', N'86caf117-37ed-4370-ac31-0d86eecad8ed', N'Purchase of Furniture', N'dffa4e85-ea93-4958-bee5-f1b97349625e', 1, N'40fdeeb4-e487-4639-acbd-09c85715970a', NULL, CAST(N'2018-08-17T14:14:39.420' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Procurements] ([ProcurementID], [BudgetYearID], [DepartmentID], [ProjectName], [ProjectCategoryID], [ProcurementStatusID], [ProcurementMethodID], [SourceOfFundID], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'bf1ea654-8bfc-4841-8448-74676a348c88', N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', N'3a380f88-6b8b-40af-aa14-df546cec1aa6', N'Purchase of Furniture', N'dffa4e85-ea93-4958-bee5-f1b97349625e', 5, N'40fdeeb4-e487-4639-acbd-09c85715970a', NULL, CAST(N'2018-08-30T16:14:20.667' AS DateTime), NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Procurements] ([ProcurementID], [BudgetYearID], [DepartmentID], [ProjectName], [ProjectCategoryID], [ProcurementStatusID], [ProcurementMethodID], [SourceOfFundID], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'57e10d3f-9e42-4abf-99f4-789f422d65bd', N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', N'3a380f88-6b8b-40af-aa14-df546cec1aa6', N'Procurement of mouse', N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', 5, N'40fdeeb4-e487-4639-acbd-09c85715970a', NULL, CAST(N'2018-08-30T16:14:20.667' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Procurements] ([ProcurementID], [BudgetYearID], [DepartmentID], [ProjectName], [ProjectCategoryID], [ProcurementStatusID], [ProcurementMethodID], [SourceOfFundID], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'16b31950-2525-42c8-8bab-8cb7a04ff613', N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', N'3a380f88-6b8b-40af-aa14-df546cec1aa6', N'Procurement of wigs', N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', NULL, N'40fdeeb4-e487-4639-acbd-09c85715970a', NULL, NULL, NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Procurements] ([ProcurementID], [BudgetYearID], [DepartmentID], [ProjectName], [ProjectCategoryID], [ProcurementStatusID], [ProcurementMethodID], [SourceOfFundID], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'f1176ed7-3000-41bb-bfd0-ac63aeaf81aa', N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', N'3a380f88-6b8b-40af-aa14-df546cec1aa6', N'Procurement of Android phones', N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', 5, N'40fdeeb4-e487-4639-acbd-09c85715970a', NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Procurements] ([ProcurementID], [BudgetYearID], [DepartmentID], [ProjectName], [ProjectCategoryID], [ProcurementStatusID], [ProcurementMethodID], [SourceOfFundID], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (N'4dcd296b-d962-4390-a783-cb045af7c4ed', N'452be391-2a89-4212-a5c8-b0a0bb37dfe3', N'3a380f88-6b8b-40af-aa14-df546cec1aa6', N'Procurement of laptop', N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', 5, N'40fdeeb4-e487-4639-acbd-09c85715970a', NULL, CAST(N'2018-08-10T00:00:00.000' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (1, N'Draft', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (2, N'Pending', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (3, N'Not Approved', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (4, N'Approved', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProcurementStatus] ([ProcurementStatusID], [Status], [Description], [DateModified], [CreatedBy], [DateCreated], [TenantID]) VALUES (5, N'Attested', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'ff71b09d-c350-4d17-b292-1920d3f3f6e8', N'Works-Entrepreneur', CAST(N'2018-09-10T18:00:07.987' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:00:07.987' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'36829cf2-71d4-4515-9b20-19c2753db9aa', N'Corporation/Special works', CAST(N'2018-09-10T18:01:13.973' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:01:13.973' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'fd40d63b-95bc-4fd7-abd3-1e34c16d959c', N'Goods', CAST(N'2018-09-10T18:01:13.973' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:00:07.987' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', N'Services', CAST(N'2018-09-10T18:00:07.987' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:00:07.987' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'0451464e-22b2-4154-8b65-52927d302898', N'Consultant services', CAST(N'2018-09-10T18:00:43.283' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:00:43.283' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'e94bbdac-db7b-44ec-9b17-759c2d252ad5', N'Nigerian National Petroleum', CAST(N'2018-09-10T18:01:42.377' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:01:42.377' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'6b13c0fd-b2bc-413c-bb37-99ee5235c755', N'National Defense or Security', CAST(N'2018-09-10T18:02:13.880' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:02:13.880' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'62a7e216-d8fd-40ca-ac02-b9f501473c14', N'Works', CAST(N'2018-09-10T18:02:13.880' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:02:13.880' AS DateTime))
GO
INSERT [dbo].[ProjectCategory] ([ProjectCategoryID], [Name], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'dffa4e85-ea93-4958-bee5-f1b97349625e', N'Commodities', CAST(N'2018-09-10T18:02:13.880' AS DateTime), N'Techspecialist', CAST(N'2018-09-10T18:02:13.880' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'ff71b09d-c350-4d17-b292-1920d3f3f6e8', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.777' AS DateTime), CAST(N'2018-09-19T10:32:24.777' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'ff71b09d-c350-4d17-b292-1920d3f3f6e8', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.410' AS DateTime), CAST(N'2018-09-19T10:29:15.410' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'ff71b09d-c350-4d17-b292-1920d3f3f6e8', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:30:04.477' AS DateTime), CAST(N'2018-09-19T12:30:04.477' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'ff71b09d-c350-4d17-b292-1920d3f3f6e8', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:39.230' AS DateTime), CAST(N'2018-09-19T11:38:39.230' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'36829cf2-71d4-4515-9b20-19c2753db9aa', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.777' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'36829cf2-71d4-4515-9b20-19c2753db9aa', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.410' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'36829cf2-71d4-4515-9b20-19c2753db9aa', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:30:04.477' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'36829cf2-71d4-4515-9b20-19c2753db9aa', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:39.230' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'fd40d63b-95bc-4fd7-abd3-1e34c16d959c', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.777' AS DateTime), CAST(N'2018-09-19T10:32:24.777' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'fd40d63b-95bc-4fd7-abd3-1e34c16d959c', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.410' AS DateTime), CAST(N'2018-09-19T10:29:15.410' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'fd40d63b-95bc-4fd7-abd3-1e34c16d959c', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:30:04.477' AS DateTime), CAST(N'2018-09-19T12:30:04.477' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'fd40d63b-95bc-4fd7-abd3-1e34c16d959c', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:39.230' AS DateTime), CAST(N'2018-09-19T11:38:39.230' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.777' AS DateTime), CAST(N'2018-09-19T10:32:24.777' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.410' AS DateTime), CAST(N'2018-09-19T10:29:15.410' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:30:04.477' AS DateTime), CAST(N'2018-09-19T12:30:04.477' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'71cf7646-9d37-43fb-ac4d-23d34ea4d4d5', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:39.230' AS DateTime), CAST(N'2018-09-19T11:38:39.230' AS DateTime))
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'0451464e-22b2-4154-8b65-52927d302898', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.777' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'0451464e-22b2-4154-8b65-52927d302898', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.410' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'0451464e-22b2-4154-8b65-52927d302898', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:30:04.477' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'0451464e-22b2-4154-8b65-52927d302898', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:39.230' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'e94bbdac-db7b-44ec-9b17-759c2d252ad5', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.777' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'e94bbdac-db7b-44ec-9b17-759c2d252ad5', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.410' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'e94bbdac-db7b-44ec-9b17-759c2d252ad5', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:30:04.477' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'e94bbdac-db7b-44ec-9b17-759c2d252ad5', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:39.230' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'6b13c0fd-b2bc-413c-bb37-99ee5235c755', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.777' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'6b13c0fd-b2bc-413c-bb37-99ee5235c755', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.410' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'6b13c0fd-b2bc-413c-bb37-99ee5235c755', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:30:04.477' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'6b13c0fd-b2bc-413c-bb37-99ee5235c755', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:39.230' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'62a7e216-d8fd-40ca-ac02-b9f501473c14', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.777' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'62a7e216-d8fd-40ca-ac02-b9f501473c14', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.410' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'62a7e216-d8fd-40ca-ac02-b9f501473c14', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:30:04.477' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'62a7e216-d8fd-40ca-ac02-b9f501473c14', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:39.230' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'dffa4e85-ea93-4958-bee5-f1b97349625e', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.777' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'dffa4e85-ea93-4958-bee5-f1b97349625e', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.410' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'dffa4e85-ea93-4958-bee5-f1b97349625e', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:30:04.477' AS DateTime), NULL)
GO
INSERT [dbo].[ProjectCategoryOrganizationSettings] ([ProjectCategoryID], [TenantID], [OrganizationID], [EnableProjectCategory], [DateCreated], [DateModified]) VALUES (N'dffa4e85-ea93-4958-bee5-f1b97349625e', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:39.230' AS DateTime), NULL)
GO
INSERT [dbo].[RequestForDemo] ([RequestID], [OrganizationFullName], [OrganizationShortName], [AdministratorEmail], [AdministratorFirstName], [AdministratorLastName], [AdministratorPhoneNumber], [DateCreated]) VALUES (N'ad905cfb-8914-4f04-ba9c-003668bec91f', N'Central Bank of Nigeria', N'CBN', N'annieajeks@gmail.com', N'Kadir', N'Salami', N'+2347065389510', CAST(N'2018-09-06T16:56:46.497' AS DateTime))
GO
INSERT [dbo].[RequestForDemo] ([RequestID], [OrganizationFullName], [OrganizationShortName], [AdministratorEmail], [AdministratorFirstName], [AdministratorLastName], [AdministratorPhoneNumber], [DateCreated]) VALUES (N'3c0ebb6f-d4bb-46c0-a3ae-1d8fd592d459', N'Ministry Of Foriegn Affairs', N'MFA', N'annieajek@gmail.com', N'Anita', N'Salami', N'+2347065389513', CAST(N'2018-09-10T14:55:15.243' AS DateTime))
GO
INSERT [dbo].[RequestForDemo] ([RequestID], [OrganizationFullName], [OrganizationShortName], [AdministratorEmail], [AdministratorFirstName], [AdministratorLastName], [AdministratorPhoneNumber], [DateCreated]) VALUES (N'8dc27a6f-2710-4c23-9d50-676b4f6828ef', N'Nigeria Information Technology Development Agency', N'NITDA', N'oaro@techspecialistlimited.com', N'Muyiwa', N'Aro', N'+2347065949501', CAST(N'2018-09-19T12:29:21.327' AS DateTime))
GO
INSERT [dbo].[RequestForDemo] ([RequestID], [OrganizationFullName], [OrganizationShortName], [AdministratorEmail], [AdministratorFirstName], [AdministratorLastName], [AdministratorPhoneNumber], [DateCreated]) VALUES (N'ebff2784-a3a1-4da5-bf26-6a5ff2168213', N'old Nigeria Information Technology Development Agency', N'old NITDA', N'o@techspecialistlimited.com', N'Muyiwa', N'Aro', N'+2348103645221', CAST(N'2018-09-19T11:36:58.660' AS DateTime))
GO
INSERT [dbo].[RequestForDemo] ([RequestID], [OrganizationFullName], [OrganizationShortName], [AdministratorEmail], [AdministratorFirstName], [AdministratorLastName], [AdministratorPhoneNumber], [DateCreated]) VALUES (N'f6c0b955-de16-4dba-b3f5-97e5bdf8686d', N'Nigeria Communication Commissions', N'NCC', N'grayceecharles@gmail.com', N'Kadir', N'Salami', N'+2347065389511', CAST(N'2018-09-06T16:55:21.430' AS DateTime))
GO
INSERT [dbo].[SourceOfFunds] ([SourceOfFundID], [SourceOfFund], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'cf27fb7a-b764-4abc-b8a1-05fe9fe7f9f4', N'Budgetary allocation/appropriation', CAST(N'2018-09-07T12:06:24.427' AS DateTime), N'Techspecialist', CAST(N'2018-09-07T12:06:24.427' AS DateTime))
GO
INSERT [dbo].[SourceOfFunds] ([SourceOfFundID], [SourceOfFund], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'57832d27-0ef8-4f95-b48f-33cbc268ecca', N'Internally generated fund', CAST(N'2018-09-07T10:58:34.433' AS DateTime), N'Techspecialist', CAST(N'2018-09-07T10:58:34.433' AS DateTime))
GO
INSERT [dbo].[SourceOfFunds] ([SourceOfFundID], [SourceOfFund], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'7d87e3fb-0bc7-441c-ba98-850bd89f2e86', N'Special intervention fund', CAST(N'2018-09-07T02:36:02.893' AS DateTime), N'Techspecialist', CAST(N'2018-09-07T02:36:02.893' AS DateTime))
GO
INSERT [dbo].[SourceOfFunds] ([SourceOfFundID], [SourceOfFund], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'ddd514ef-a391-4a2f-9385-d702057c94eb', N'Power sector intervention fund', CAST(N'2018-09-07T02:35:55.223' AS DateTime), N'Techspecialist', CAST(N'2018-09-07T02:35:55.223' AS DateTime))
GO
INSERT [dbo].[SourceOfFunds] ([SourceOfFundID], [SourceOfFund], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'44553310-e002-4fbe-8ff0-d747517fece3', N'ETF Special intervention fund', CAST(N'2018-09-07T02:37:00.913' AS DateTime), N'Techspecialist', CAST(N'2018-09-07T02:37:00.913' AS DateTime))
GO
INSERT [dbo].[SourceOfFunds] ([SourceOfFundID], [SourceOfFund], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'107dae9c-b7e7-4f67-9d07-dfd3cb99bb3e', N'Donor fund', CAST(N'2018-09-07T02:36:01.597' AS DateTime), N'Techspecialist', CAST(N'2018-09-07T02:36:01.597' AS DateTime))
GO
INSERT [dbo].[SourceOfFunds] ([SourceOfFundID], [SourceOfFund], [DateModified], [CreatedBy], [DateCreated]) VALUES (N'5654ea90-f0f8-4004-84f0-f3d687d18dde', N'Ecological fund', CAST(N'2018-09-07T15:15:34.990' AS DateTime), N'Techspecialist', CAST(N'2018-09-07T15:15:34.990' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'cf27fb7a-b764-4abc-b8a1-05fe9fe7f9f4', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.500' AS DateTime), CAST(N'2018-09-19T10:32:24.500' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'cf27fb7a-b764-4abc-b8a1-05fe9fe7f9f4', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.147' AS DateTime), CAST(N'2018-09-19T10:29:15.147' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'cf27fb7a-b764-4abc-b8a1-05fe9fe7f9f4', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:51.603' AS DateTime), CAST(N'2018-09-19T12:29:51.603' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'cf27fb7a-b764-4abc-b8a1-05fe9fe7f9f4', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:26.120' AS DateTime), CAST(N'2018-09-19T11:38:26.120' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'57832d27-0ef8-4f95-b48f-33cbc268ecca', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.500' AS DateTime), CAST(N'2018-09-19T10:32:24.500' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'57832d27-0ef8-4f95-b48f-33cbc268ecca', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.147' AS DateTime), CAST(N'2018-09-19T10:29:15.147' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'57832d27-0ef8-4f95-b48f-33cbc268ecca', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:51.603' AS DateTime), CAST(N'2018-09-19T12:29:51.603' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'57832d27-0ef8-4f95-b48f-33cbc268ecca', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:26.120' AS DateTime), CAST(N'2018-09-19T11:38:26.120' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'7d87e3fb-0bc7-441c-ba98-850bd89f2e86', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.500' AS DateTime), CAST(N'2018-09-19T10:32:24.500' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'7d87e3fb-0bc7-441c-ba98-850bd89f2e86', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.147' AS DateTime), CAST(N'2018-09-19T10:29:15.147' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'7d87e3fb-0bc7-441c-ba98-850bd89f2e86', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:51.603' AS DateTime), CAST(N'2018-09-19T12:29:51.603' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'7d87e3fb-0bc7-441c-ba98-850bd89f2e86', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:26.120' AS DateTime), CAST(N'2018-09-19T11:38:26.120' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'ddd514ef-a391-4a2f-9385-d702057c94eb', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.500' AS DateTime), CAST(N'2018-09-19T10:32:24.500' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'ddd514ef-a391-4a2f-9385-d702057c94eb', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.147' AS DateTime), CAST(N'2018-09-19T10:29:15.147' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'ddd514ef-a391-4a2f-9385-d702057c94eb', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:51.603' AS DateTime), CAST(N'2018-09-19T12:29:51.603' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'ddd514ef-a391-4a2f-9385-d702057c94eb', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:26.120' AS DateTime), CAST(N'2018-09-19T11:38:26.120' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'44553310-e002-4fbe-8ff0-d747517fece3', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 1, CAST(N'2018-09-19T10:32:24.500' AS DateTime), CAST(N'2018-09-19T10:32:24.500' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'44553310-e002-4fbe-8ff0-d747517fece3', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 1, CAST(N'2018-09-19T10:29:15.147' AS DateTime), CAST(N'2018-09-19T10:29:15.147' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'44553310-e002-4fbe-8ff0-d747517fece3', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 1, CAST(N'2018-09-19T12:29:51.603' AS DateTime), CAST(N'2018-09-19T12:29:51.603' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'44553310-e002-4fbe-8ff0-d747517fece3', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 1, CAST(N'2018-09-19T11:38:26.120' AS DateTime), CAST(N'2018-09-19T11:38:26.120' AS DateTime))
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'107dae9c-b7e7-4f67-9d07-dfd3cb99bb3e', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.500' AS DateTime), NULL)
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'107dae9c-b7e7-4f67-9d07-dfd3cb99bb3e', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.147' AS DateTime), NULL)
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'107dae9c-b7e7-4f67-9d07-dfd3cb99bb3e', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:51.603' AS DateTime), NULL)
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'107dae9c-b7e7-4f67-9d07-dfd3cb99bb3e', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:26.120' AS DateTime), NULL)
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'5654ea90-f0f8-4004-84f0-f3d687d18dde', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07', N'00496d49-3e29-48de-817c-199c88c3385d', 0, CAST(N'2018-09-19T10:32:24.500' AS DateTime), NULL)
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'5654ea90-f0f8-4004-84f0-f3d687d18dde', N'e0cf4891-7884-47ac-b4d7-4989473b1822', N'f9062059-3da6-40c7-82c9-b4ee6bebde91', 0, CAST(N'2018-09-19T10:29:15.147' AS DateTime), NULL)
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'5654ea90-f0f8-4004-84f0-f3d687d18dde', N'87d1123a-57ec-443c-8042-887720d46a81', N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', 0, CAST(N'2018-09-19T12:29:51.603' AS DateTime), NULL)
GO
INSERT [dbo].[SourceOfFundsOrganizationSettings] ([SourceOfFundID], [TenantID], [OrganizationID], [EnableSourceOFFund], [DateCreated], [DateModified]) VALUES (N'5654ea90-f0f8-4004-84f0-f3d687d18dde', N'0bbf7136-c806-44b3-93d1-edec6b59ea6f', N'3d588c13-f147-4a31-b0c9-dc59cde7c0b8', 0, CAST(N'2018-09-19T11:38:26.120' AS DateTime), NULL)
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'cc10894d-0c79-443b-85f3-028c4ded9838', N'c59b1c88-d6c6-47f0-8290-28c9dc610e33', N'3a380f88-6b8b-40af-aa14-df546cec1aa6', N'gejegwa@techspecialistlimited.com', N'Grace', N'Ejegwa', NULL, NULL, NULL, NULL, NULL, N'87d1123a-57ec-443c-8042-887720d46a81')
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'140ec11c-befd-4db3-aee6-0c45339d3f05', N'664a833e-2c6e-47dd-bbc6-ffdae71576c7', N'86caf117-37ed-4370-ac31-0d86eecad8ed', N'muyiweraro@gmail.com', N'Muyiwa', N'Aro', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'5709168e-1952-4a3d-b7cf-2774e82be4d4', N'4ad260d6-74f8-49a9-991b-6f01d8d0f48a', N'86caf117-37ed-4370-ac31-0d86eecad8ed', N'oalabi@techspecialistlimited.com', N'Tosin', N'Alabi', NULL, NULL, NULL, NULL, NULL, N'87d1123a-57ec-443c-8042-887720d46a81')
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'0de89f83-537b-46df-b831-50316512623a', N'e57ada7a-2d0b-4c78-9c6c-f492f9c92fc4', NULL, N'grayceecharles@gmail.com', N'Kadir', N'Salami', NULL, NULL, NULL, CAST(N'2018-09-19T10:32:23.713' AS DateTime), N'00496d49-3e29-48de-817c-199c88c3385d', N'4c5e788c-309f-4446-9c92-1dcd7fb1eb07')
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'78149534-e393-46a7-a63f-831c985f7b79', N'47cf4505-a349-4b6a-9ae7-081609365fff', NULL, N'annieajeks@gmail.com', N'Kadir', N'Salami', NULL, NULL, NULL, NULL, N'f9062059-3da6-40c7-82c9-b4ee6bebde91', N'e0cf4891-7884-47ac-b4d7-4989473b1822')
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'4baa9c69-189d-49e2-a97c-a4533b63bd34', NULL, NULL, N'gejegwa@techspecialistlimited.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'87d1123a-57ec-443c-8042-887720d46a81')
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'e2f21fad-1f3d-477d-a818-bb74eb85ad4b', N'642eb4cd-867f-4bd3-920b-ea52d4c4e740', N'86caf117-37ed-4370-ac31-0d86eecad8ed', N'fadepoju@techspecialistlimited.com', N'Fikayo', N'Adepoju', NULL, NULL, NULL, NULL, NULL, N'87d1123a-57ec-443c-8042-887720d46a81')
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'f121364b-0c1d-4c49-b677-bcb4c993f303', N'2fcb9a3a-a7e9-40be-b1c6-e41a50101484', N'86caf117-37ed-4370-ac31-0d86eecad8ed', N'yolalere@techspecialistlimited.com', N'Yusuf', N'Olalere', NULL, NULL, NULL, NULL, NULL, N'87d1123a-57ec-443c-8042-887720d46a81')
GO
INSERT [dbo].[UserProfile] ([UserID], [Id], [DepartmentID], [UserEmail], [FirstName], [LastName], [UserName], [DateModified], [CreatedBy], [DateCreated], [OrganizationID], [TenantID]) VALUES (N'd605cd02-24dc-4a8e-8bc8-bd4c15a08306', N'231da76e-91e5-4080-bfee-8e5b3f034a0b', NULL, N'oaro@techspecialistlimited.com', N'Muyiwa', N'Aro', NULL, NULL, NULL, CAST(N'2018-09-19T12:29:49.263' AS DateTime), N'5e9bc68f-bee5-4270-b822-aa4c2abbc145', N'87d1123a-57ec-443c-8042-887720d46a81')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 20/09/2018 8:35:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 8:35:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 8:35:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 20/09/2018 8:35:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 20/09/2018 8:35:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 20/09/2018 8:35:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Catalog__7499EE10FCF426BA]    Script Date: 20/09/2018 8:35:26 AM ******/
ALTER TABLE [dbo].[Catalog] ADD UNIQUE NONCLUSTERED 
(
	[SubDomain] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__1736EFFE00311490]    Script Date: 20/09/2018 8:35:26 AM ******/
ALTER TABLE [dbo].[RequestForDemo] ADD UNIQUE NONCLUSTERED 
(
	[OrganizationShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__2356C68A90C6B57E]    Script Date: 20/09/2018 8:35:26 AM ******/
ALTER TABLE [dbo].[RequestForDemo] ADD UNIQUE NONCLUSTERED 
(
	[OrganizationFullName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__6E2AFC2AB8DEE445]    Script Date: 20/09/2018 8:35:26 AM ******/
ALTER TABLE [dbo].[RequestForDemo] ADD UNIQUE NONCLUSTERED 
(
	[AdministratorEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RequestF__A86C39D7895676DC]    Script Date: 20/09/2018 8:35:26 AM ******/
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
