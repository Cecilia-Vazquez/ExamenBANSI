USE [master]
GO

CREATE DATABASE [BdiExamen]

CREATE TABLE [dbo].[tbIExamen](
	[idExamen] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NULL,
	[Descripcion] [varchar](255) NULL,
 CONSTRAINT [PK_tbIExamen] PRIMARY KEY CLUSTERED 
(
	
