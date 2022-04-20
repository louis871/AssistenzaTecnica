USE [assistenza_tecnica]
GO
/****** Object:  User [assistenza01]    Script Date: 20/04/2022 17:21:47 ******/
CREATE USER [assistenza01] FOR LOGIN [assistenza01] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [assistenza01]
GO
/****** Object:  Table [dbo].[assegnati]    Script Date: 20/04/2022 17:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[assegnati](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](127) NULL,
	[id_utenti] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[richieste]    Script Date: 20/04/2022 17:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[richieste](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[testo] [varchar](max) NOT NULL,
	[id_riferimenti] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[riferimenti]    Script Date: 20/04/2022 17:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[riferimenti](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descrizione] [varchar](255) NOT NULL,
	[id_utenti] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stati]    Script Date: 20/04/2022 17:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stati](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descrizione] [varchar](63) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stati_richieste]    Script Date: 20/04/2022 17:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stati_richieste](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_richieste] [int] NOT NULL,
	[id_stati] [int] NOT NULL,
	[id_utenti] [int] NOT NULL,
	[data_aggiunta] [datetime] NOT NULL,
	[ore_lavorate] [float] NULL,
	[id_assegnati] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utenti]    Script Date: 20/04/2022 17:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utenti](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](128) NOT NULL,
	[username] [varchar](31) NOT NULL,
	[password] [varchar](31) NOT NULL,
	[profilo] [int] NOT NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [assistenza_tecnica] SET  READ_WRITE 
GO
