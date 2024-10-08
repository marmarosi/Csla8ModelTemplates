USE [master]
GO

IF NOT EXISTS 
    (SELECT name FROM sys.server_principals WHERE name = 'csla8mt')
BEGIN
    CREATE LOGIN [csla8mt] WITH PASSWORD=N'iYJdR7exyFSuA', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
END
GO

IF NOT EXISTS
    (SELECT * FROM sys.databases WHERE name = 'Csla8mt')
BEGIN

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
	ALTER DATABASE [Csla8mt] SET  READ_WRITE 
	GO

	USE [Csla8mt]
	GO
	EXEC sp_changedbowner 'csla8mt'
	GO
	ALTER LOGIN [csla8mt] WITH DEFAULT_DATABASE = [Csla8mt]
	GO

END
GO
