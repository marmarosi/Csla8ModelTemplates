USE [master]
GO

IF NOT EXISTS 
    (SELECT name FROM master.sys.server_principals WHERE name = 'csla8mt')
BEGIN
    CREATE LOGIN [csla8mt] WITH PASSWORD=N'iYJdR7exyFSuA', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
END
GO

IF NOT EXISTS
    (SELECT * FROM master.sys.databases WHERE name = 'Csla8mt')
BEGIN
	CREATE DATABASE [Csla8mt]
END
GO

USE [Csla8mt]
GO

DECLARE @owner AS nvarchar(100)
SELECT @owner = suser_sname(owner_sid) FROM sys.databases WHERE name = 'Csla8mt'
IF @owner != 'csla8mt'
BEGIN
	EXEC sp_changedbowner 'csla8mt'
END
GO

DECLARE @dfltdb AS nvarchar(100)
SELECT @dfltdb = default_database_name FROM sys.server_principals WHERE name = 'csla8mt';
IF @dfltdb != 'Csla8mt'
BEGIN
	ALTER LOGIN [csla8mt] WITH DEFAULT_DATABASE = [Csla8mt]
END
GO
