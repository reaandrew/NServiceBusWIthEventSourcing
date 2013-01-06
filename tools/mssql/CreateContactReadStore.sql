USE [master]
GO

/****** Object:  Database [Contact]    Script Date: 01/02/2013 20:11:51 ******/
CREATE DATABASE [Contact] ON  PRIMARY 
( NAME = N'Contact', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\Contact.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Contact_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\Contact_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [Contact] SET COMPATIBILITY_LEVEL = 90
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Contact].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Contact] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Contact] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Contact] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Contact] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Contact] SET ARITHABORT OFF 
GO

ALTER DATABASE [Contact] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Contact] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [Contact] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Contact] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Contact] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Contact] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Contact] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Contact] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Contact] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Contact] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Contact] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Contact] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Contact] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Contact] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Contact] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Contact] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Contact] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Contact] SET  READ_WRITE 
GO

ALTER DATABASE [Contact] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Contact] SET  MULTI_USER 
GO

ALTER DATABASE [Contact] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Contact] SET DB_CHAINING OFF 
GO


USE [Contact]
GO
/****** Object:  Table [dbo].[Authentication]    Script Date: 01/02/2013 20:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authentication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthenticationId] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[HashedPassword] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Authentication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccommodationLeads]    Script Date: 01/02/2013 20:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccommodationLeads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccommodationLeadId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_AccommodationLeads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/02/2013 20:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccommodationSuppliers]    Script Date: 01/02/2013 20:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccommodationSuppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccommodationSupplierId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AccommodationSuppliersd] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_AccommodationLeads_Approved]    Script Date: 01/02/2013 20:11:42 ******/
ALTER TABLE [dbo].[AccommodationLeads] ADD  CONSTRAINT [DF_AccommodationLeads_Approved]  DEFAULT ((0)) FOR [Approved]
GO
