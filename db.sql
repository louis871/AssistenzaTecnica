USE [assistenza_tecnica]
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
	[descrizione] [varchar](63) NOT NULL,
	[profilo_minimo] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[stati] ADD  CONSTRAINT [DF_stati_profilo_minimo]  DEFAULT ((0)) FOR [profilo_minimo]
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

SET IDENTITY_INSERT [dbo].[stati] ON 
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (1, N'Inserita', 0)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (2, N'Presa in carico', 2)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (3, N'Lavorazione in corso', 2)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (4, N'Completata', 2)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (5, N'Rifiutata', 3)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (6, N'Annullata', 1)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (7, N'Richiesta di ulteriori dettagli', 2)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (8, N'Abortita per richiesta del cliente', 1)
GO
INSERT [dbo].[stati] ([id], [descrizione], [profilo_minimo]) VALUES (9, N'Inseriti ulteriori dettagli', 1)
GO
SET IDENTITY_INSERT [dbo].[stati] OFF
GO
SET IDENTITY_INSERT [dbo].[utenti] ON 
GO
INSERT [dbo].[utenti] ([id], [nome], [username], [password], [profilo]) VALUES (1, N'Amministratore', N'admin', N'Admin1234', 4)
GO
INSERT [dbo].[utenti] ([id], [nome], [username], [password], [profilo]) VALUES (2, N'Operatore Due', N'oper2', N'Oper1234', 2)
GO
INSERT [dbo].[utenti] ([id], [nome], [username], [password], [profilo]) VALUES (3, N'Organizzatore', N'org1', N'Org123', 3)
GO
INSERT [dbo].[utenti] ([id], [nome], [username], [password], [profilo]) VALUES (4, N'Cliente', N'cli1', N'Cli123', 0)
GO
INSERT [dbo].[utenti] ([id], [nome], [username], [password], [profilo]) VALUES (5, N'Operatore Uno', N'oper1', N'Oper123', 1)
GO
SET IDENTITY_INSERT [dbo].[utenti] OFF
GO
