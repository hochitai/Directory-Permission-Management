USE [master]
GO
/****** Object:  Database [DirectoryManagement]    Script Date: 9/2/2024 11:41:38 PM ******/
CREATE DATABASE [DirectoryManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DirectoryManagement', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DirectoryManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DirectoryManagement_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DirectoryManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DirectoryManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DirectoryManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DirectoryManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DirectoryManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DirectoryManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DirectoryManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DirectoryManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [DirectoryManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DirectoryManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DirectoryManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DirectoryManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DirectoryManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DirectoryManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DirectoryManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DirectoryManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DirectoryManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DirectoryManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DirectoryManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DirectoryManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DirectoryManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DirectoryManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DirectoryManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DirectoryManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DirectoryManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DirectoryManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [DirectoryManagement] SET  MULTI_USER 
GO
ALTER DATABASE [DirectoryManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DirectoryManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DirectoryManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DirectoryManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DirectoryManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DirectoryManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DirectoryManagement', N'ON'
GO
ALTER DATABASE [DirectoryManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [DirectoryManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DirectoryManagement]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/2/2024 11:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drives]    Script Date: 9/2/2024 11:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drives](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Drives] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Folders]    Script Date: 9/2/2024 11:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ParrentFolderId] [int] NULL,
	[DriveId] [int] NOT NULL,
 CONSTRAINT [PK_Folders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 9/2/2024 11:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[FolderId] [int] NULL,
	[DriveId] [int] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 9/2/2024 11:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[UserId] [int] NOT NULL,
	[DriveId] [int] NULL,
	[FolderId] [int] NULL,
	[ItemId] [int] NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/2/2024 11:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/2/2024 11:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Salt] [nvarchar](max) NOT NULL,
	[IsActived] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240902093502_InitialMigrations', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'contributor')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'reader')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
USE [master]
GO
ALTER DATABASE [DirectoryManagement] SET  READ_WRITE 
GO

Use DirectoryManagement;

ALTER TABLE [dbo].[Drives]
ADD CONSTRAINT fk_drives_user_id
 FOREIGN KEY ([UserId])
 REFERENCES [dbo].[Users] ([Id]);

ALTER TABLE [dbo].[Folders]
ADD CONSTRAINT fk_folders_drive_id
 FOREIGN KEY ([DriveId])
 REFERENCES [dbo].[Drives] ([Id]);

ALTER TABLE [dbo].[Folders]
ADD CONSTRAINT fk_folders_parrent_folder_id
 FOREIGN KEY ([ParrentFolderId])
 REFERENCES [dbo].[Folders] ([Id]);

ALTER TABLE [dbo].[Items]
ADD CONSTRAINT fk_items_folder_id
 FOREIGN KEY ([FolderId])
 REFERENCES [dbo].[Folders] ([Id]);

ALTER TABLE [dbo].[Items]
ADD CONSTRAINT fk_items_drive_id
 FOREIGN KEY ([DriveId])
 REFERENCES [dbo].[Drives] ([Id]);

ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT fk_permissions_user_id
 FOREIGN KEY ([UserId])
 REFERENCES [dbo].[Users] ([Id]);

ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT fk_permissions_drive_id
 FOREIGN KEY ([DriveId])
 REFERENCES [dbo].[Drives] ([Id]);

ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT fk_permissions_folder_id
 FOREIGN KEY ([FolderId])
 REFERENCES [dbo].[Folders] ([Id]);

ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT fk_permissions_item_id
 FOREIGN KEY ([ItemId])
 REFERENCES [dbo].[Items] ([Id]);

ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT fk_permissions_role_id
 FOREIGN KEY ([RoleId])
 REFERENCES [dbo].[Roles] ([Id]);

ALTER TABLE [dbo].[Permissions]
	DROP CONSTRAINT [PK_Permissions];