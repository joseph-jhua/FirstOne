USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [SimpleVehicleSystemUser]    Script Date: 2015/12/6 1:19:51 ******/
CREATE LOGIN [SimpleVehicleSystemUser] WITH PASSWORD=N'ö"!#c}(cTJãvÌ#Ä2¶½;SÆ_', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
GO

ALTER LOGIN [SimpleVehicleSystemUser] DISABLE
GO

ALTER SERVER ROLE [dbcreator] ADD MEMBER [SimpleVehicleSystemUser]
GO

USE [master]
GO

/****** Object:  Database [SimpleVehicleSystem]    Script Date: 2015/12/6 1:05:08 ******/
CREATE DATABASE [SimpleVehicleSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SimpleVehicleSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SimpleVehicleSystem.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SimpleVehicleSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SimpleVehicleSystem_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [SimpleVehicleSystem] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimpleVehicleSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SimpleVehicleSystem] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET ARITHABORT OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SimpleVehicleSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SimpleVehicleSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET  DISABLE_BROKER 
GO

ALTER DATABASE [SimpleVehicleSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SimpleVehicleSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [SimpleVehicleSystem] SET  MULTI_USER 
GO

ALTER DATABASE [SimpleVehicleSystem] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SimpleVehicleSystem] SET DB_CHAINING OFF 
GO

ALTER DATABASE [SimpleVehicleSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [SimpleVehicleSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [SimpleVehicleSystem] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [SimpleVehicleSystem] SET  READ_WRITE 
GO


USE [SimpleVehicleSystem]
GO

/****** Object:  User [SimpleVehicleSystemUser]    Script Date: 2015/12/6 1:05:32 ******/
CREATE USER [SimpleVehicleSystemUser] FOR LOGIN [SimpleVehicleSystemUser] WITH DEFAULT_SCHEMA=[dbo]
GO

ALTER ROLE db_owner ADD MEMBER [SimpleVehicleSystemUser]
GO

USE [SimpleVehicleSystem]
GO

/****** Object:  Table [dbo].[Vehicle]    Script Date: 2015/12/6 1:07:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Vehicle](
	[VId] [bigint] IDENTITY(1,1) NOT NULL,
	[Brand] [nvarchar](50) NULL,
	[Model] [nvarchar](50) NULL,
	[Colour] [nvarchar](50) NULL,
	[ProduceYear] [int] NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[VId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [SimpleVehicleSystem]
GO

/****** Object:  StoredProcedure [dbo].[AddVehicle]    Script Date: 2015/12/6 1:08:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddVehicle] 
	@Brand nvarchar(50),
	@Model nvarchar(50),
	@Colour nvarchar(50),
	@ProduceYear int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO SimpleVehicleSystem..Vehicle VALUES (@Brand, @Model, @Colour, @ProduceYear)
END

GO

USE [SimpleVehicleSystem]
GO

/****** Object:  StoredProcedure [dbo].[DeleteVehicleByVId]    Script Date: 2015/12/6 1:08:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteVehicleByVId] 
	@VId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM SimpleVehicleSystem..Vehicle WHERE VId = @VId
END

GO


USE [SimpleVehicleSystem]
GO

/****** Object:  StoredProcedure [dbo].[GetAllVehicle]    Script Date: 2015/12/6 1:08:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllVehicle] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM SimpleVehicleSystem..Vehicle ORDER BY VId ASC
END

GO


USE [SimpleVehicleSystem]
GO

/****** Object:  StoredProcedure [dbo].[UpdateVehicleById]    Script Date: 2015/12/6 1:08:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateVehicleById]
	@VId bigint,
	@Brand nvarchar(50),
	@Model nvarchar(50),
	@Colour nvarchar(50),
	@ProduceYear int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE SimpleVehicleSystem..Vehicle SET 
		Brand = @Brand, Model = @Model, Colour = @Colour, ProduceYear = @ProduceYear
		WHERE VId = @VId
END

GO



