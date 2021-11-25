IF DB_ID('Pingen') IS NULL   
   CREATE DATABASE Pingen
GO
USE Pingen
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pins]') AND type in (N'U'))
    DROP TABLE [dbo].[Pins]
GO
CREATE TABLE [dbo].[Pins](
    [Id] [int] IDENTITY(1,1) NOT NULL,
	[Pin] [varchar](4) NOT NULL,
	[IsAvailable] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pins] ADD  DEFAULT ((1)) FOR [IsAvailable]
GO