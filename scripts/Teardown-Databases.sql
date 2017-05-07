USE [master]
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Marketing')
DROP DATABASE [Marketing]
GO