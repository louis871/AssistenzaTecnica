﻿USE [assistenza_tecnica]
GO
/****** Object:  User [assistenza01]    Script Date: 11/04/2022 20:18:03 ******/
CREATE USER [assistenza01] FOR LOGIN [assistenza01] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [assistenza01]
GO
/****** Object:  Table [dbo].[richieste]    Script Date: 11/04/2022 20:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[richieste](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[testo] [varchar](max) NOT NULL,
	[riferimento] [varchar](255) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stati]    Script Date: 11/04/2022 20:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stati](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descrizione] [varchar](63) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stati_richieste]    Script Date: 11/04/2022 20:18:03 ******/
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
	[assegnazione] [varchar](127) NULL,
	[ore_lavorate] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utenti]    Script Date: 11/04/2022 20:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utenti](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](128) NOT NULL,
	[username] [varchar](31) NOT NULL,
	[password] [varchar](31) NOT NULL
) ON [PRIMARY]
GO
