﻿USE [master]
GO

/****** Object:  Database [DellTehnicalTest]    Script Date: 11/20/2018 12:15:54 PM ******/
CREATE DATABASE [DellTehnicalTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DellTehnicalTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\DellTehnicalTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DellTehnicalTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\DellTehnicalTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [DellTehnicalTest] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DellTehnicalTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [DellTehnicalTest] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET ARITHABORT OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [DellTehnicalTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [DellTehnicalTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET  DISABLE_BROKER 
GO

ALTER DATABASE [DellTehnicalTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [DellTehnicalTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [DellTehnicalTest] SET  MULTI_USER 
GO

ALTER DATABASE [DellTehnicalTest] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [DellTehnicalTest] SET DB_CHAINING OFF 
GO

ALTER DATABASE [DellTehnicalTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [DellTehnicalTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [DellTehnicalTest] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [DellTehnicalTest] SET QUERY_STORE = OFF
GO

USE [DellTehnicalTest]
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

ALTER DATABASE [DellTehnicalTest] SET  READ_WRITE 
GO


