USE [master]
GO
/****** Object:  Database [TotalSearch]    Script Date: 04/21/2019 10:23:20 ******/
CREATE DATABASE [TotalSearch] ON  PRIMARY 
( NAME = N'TotalSearch', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\TotalSearch.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TotalSearch_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\TotalSearch_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TotalSearch] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TotalSearch].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TotalSearch] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [TotalSearch] SET ANSI_NULLS OFF
GO
ALTER DATABASE [TotalSearch] SET ANSI_PADDING OFF
GO
ALTER DATABASE [TotalSearch] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [TotalSearch] SET ARITHABORT OFF
GO
ALTER DATABASE [TotalSearch] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [TotalSearch] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [TotalSearch] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [TotalSearch] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [TotalSearch] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [TotalSearch] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [TotalSearch] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [TotalSearch] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [TotalSearch] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [TotalSearch] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [TotalSearch] SET  DISABLE_BROKER
GO
ALTER DATABASE [TotalSearch] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [TotalSearch] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [TotalSearch] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [TotalSearch] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [TotalSearch] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [TotalSearch] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [TotalSearch] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [TotalSearch] SET  READ_WRITE
GO
ALTER DATABASE [TotalSearch] SET RECOVERY SIMPLE
GO
ALTER DATABASE [TotalSearch] SET  MULTI_USER
GO
ALTER DATABASE [TotalSearch] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [TotalSearch] SET DB_CHAINING OFF
GO
USE [TotalSearch]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 04/21/2019 10:23:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[appartementid] [int] IDENTITY(1,1) NOT NULL,
	[typebien] [nvarchar](100) NULL,
	[zone] [nvarchar](100) NULL,
	[ville] [nvarchar](100) NULL,
	[quartier] [nvarchar](100) NULL,
	[nombrechambre] [decimal](3, 1) NULL,
	[prix] [money] NULL,
	[etage] [decimal](3, 1) NULL,
	[commentaire] [nvarchar](500) NULL,
	[nombrebalcon] [int] NULL,
	[plusieursresidents] [bit] NULL,
	[adresse] [nvarchar](500) NULL,
	[superficie] [int] NULL,
	[nombreetage] [int] NULL,
	[climatisation] [bit] NULL,
	[balcon] [bit] NULL,
	[ascenseur] [bit] NULL,
	[chambreforte] [bit] NULL,
	[garage] [bit] NULL,
	[acceshandicape] [bit] NULL,
	[cave] [bit] NULL,
	[bareaux] [bit] NULL,
	[balconsoleil] [bit] NULL,
	[renovee] [bit] NULL,
	[meublee] [bit] NULL,
	[unitelogement] [bit] NULL,
	[animauxdomestique] [bit] NULL,
	[date] [datetime] NULL,
	[sourceid] [nvarchar](50) NULL,
	[contact] [nvarchar](200) NULL,
	[telcontact] [nvarchar](50) NULL,
	[numeromaison] [nvarchar](20) NULL,
	[dateentree] [datetime] NULL,
	[entreeimmediate] [bit] NULL,
	[urlimage] [nvarchar](200) NULL,
	[nombreversement] [int] NULL,
	[entreeflexible] [bit] NULL,
	[sourcename] [nvarchar](50) NULL,
	[longitude] [decimal](10, 7) NULL,
	[latitude] [decimal](10, 7) NULL,
 CONSTRAINT [PK_Appartement] PRIMARY KEY CLUSTERED 
(
	[appartementid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Access]    Script Date: 04/21/2019 10:23:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Access](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[value] [nvarchar](100) NULL,
 CONSTRAINT [PK_Access] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
