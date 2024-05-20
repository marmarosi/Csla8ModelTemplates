USE [master]
GO
/****** Object:  Database [Csla8mt]    Script Date: 5/12/2024 3:37:15 PM ******/
CREATE DATABASE [Csla8mt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Csla8mt', FILENAME = N'/var/opt/mssql/data/Csla8mt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Csla8mt_log', FILENAME = N'/var/opt/mssql/data/Csla8mt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Csla8mt] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Csla8mt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Csla8mt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Csla8mt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Csla8mt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Csla8mt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Csla8mt] SET ARITHABORT OFF 
GO
ALTER DATABASE [Csla8mt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Csla8mt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Csla8mt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Csla8mt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Csla8mt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Csla8mt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Csla8mt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Csla8mt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Csla8mt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Csla8mt] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Csla8mt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Csla8mt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Csla8mt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Csla8mt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Csla8mt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Csla8mt] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Csla8mt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Csla8mt] SET RECOVERY FULL 
GO
ALTER DATABASE [Csla8mt] SET  MULTI_USER 
GO
ALTER DATABASE [Csla8mt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Csla8mt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Csla8mt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Csla8mt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Csla8mt] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Csla8mt] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Csla8mt', N'ON'
GO
ALTER DATABASE [Csla8mt] SET QUERY_STORE = ON
GO
ALTER DATABASE [Csla8mt] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Csla8mt]
GO
/****** Object:  Table [dbo].[Folders]    Script Date: 5/12/2024 3:37:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folders](
	[FolderKey] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentKey] [bigint] NULL,
	[RootKey] [bigint] NULL,
	[FolderOrder] [int] NULL,
	[FolderName] [nvarchar](100) NULL,
	[Timestamp] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Folders] PRIMARY KEY CLUSTERED 
(
	[FolderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPersons]    Script Date: 5/12/2024 3:37:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPersons](
	[GroupKey] [bigint] NOT NULL,
	[PersonKey] [bigint] NOT NULL,
 CONSTRAINT [PK_GroupPersons] PRIMARY KEY CLUSTERED 
(
	[GroupKey] ASC,
	[PersonKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 5/12/2024 3:37:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupKey] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupCode] [nvarchar](10) NULL,
	[GroupName] [nvarchar](100) NULL,
	[Timestamp] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 5/12/2024 3:37:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[PersonKey] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonCode] [nvarchar](10) NULL,
	[PersonName] [nvarchar](100) NULL,
	[Timestamp] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[PersonKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Players]    Script Date: 5/12/2024 3:37:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[PlayerKey] [bigint] IDENTITY(1,1) NOT NULL,
	[TeamKey] [bigint] NULL,
	[PlayerCode] [nvarchar](10) NULL,
	[PlayerName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[PlayerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 5/12/2024 3:37:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[TeamKey] [bigint] IDENTITY(1,1) NOT NULL,
	[TeamGuid] [uniqueidentifier] NULL,
	[TeamCode] [nvarchar](10) NULL,
	[TeamName] [nvarchar](100) NULL,
	[Timestamp] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[TeamKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Folders_ParentKey_FolderOrder]    Script Date: 5/12/2024 3:37:15 PM ******/
CREATE NONCLUSTERED INDEX [IX_Folders_ParentKey_FolderOrder] ON [dbo].[Folders]
(
	[ParentKey] ASC,
	[FolderOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupPersons_PersonKey]    Script Date: 5/12/2024 3:37:15 PM ******/
CREATE NONCLUSTERED INDEX [IX_GroupPersons_PersonKey] ON [dbo].[GroupPersons]
(
	[PersonKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Groups_GroupCode]    Script Date: 5/12/2024 3:37:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Groups_GroupCode] ON [dbo].[Groups]
(
	[GroupCode] ASC
)
WHERE ([GroupCode] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Persons_PersonCode]    Script Date: 5/12/2024 3:37:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Persons_PersonCode] ON [dbo].[Persons]
(
	[PersonCode] ASC
)
WHERE ([PersonCode] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Players_TeamKey_PlayerCode]    Script Date: 5/12/2024 3:37:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Players_TeamKey_PlayerCode] ON [dbo].[Players]
(
	[TeamKey] ASC,
	[PlayerCode] ASC
)
WHERE ([TeamKey] IS NOT NULL AND [PlayerCode] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Teams_TeamCode]    Script Date: 5/12/2024 3:37:15 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Teams_TeamCode] ON [dbo].[Teams]
(
	[TeamCode] ASC
)
WHERE ([TeamCode] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Folders]  WITH CHECK ADD  CONSTRAINT [FK_Folders_Folders_ParentKey] FOREIGN KEY([ParentKey])
REFERENCES [dbo].[Folders] ([FolderKey])
GO
ALTER TABLE [dbo].[Folders] CHECK CONSTRAINT [FK_Folders_Folders_ParentKey]
GO
ALTER TABLE [dbo].[GroupPersons]  WITH CHECK ADD  CONSTRAINT [FK_GroupPersons_Groups_GroupKey] FOREIGN KEY([GroupKey])
REFERENCES [dbo].[Groups] ([GroupKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupPersons] CHECK CONSTRAINT [FK_GroupPersons_Groups_GroupKey]
GO
ALTER TABLE [dbo].[GroupPersons]  WITH CHECK ADD  CONSTRAINT [FK_GroupPersons_Persons_PersonKey] FOREIGN KEY([PersonKey])
REFERENCES [dbo].[Persons] ([PersonKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupPersons] CHECK CONSTRAINT [FK_GroupPersons_Persons_PersonKey]
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD  CONSTRAINT [FK_Players_Teams_TeamKey] FOREIGN KEY([TeamKey])
REFERENCES [dbo].[Teams] ([TeamKey])
GO
ALTER TABLE [dbo].[Players] CHECK CONSTRAINT [FK_Players_Teams_TeamKey]
GO
USE [master]
GO
ALTER DATABASE [Csla8mt] SET  READ_WRITE 
GO