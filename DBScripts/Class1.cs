/*
      * USE [BillDB]
        		SET ANSI_NULLS ON
        		GO
                 SET QUOTED_IDENTIFIER ON
                 GO
                 CREATE TABLE[dbo].[BillTemplate]
        		(
        
                  [ID][int] IDENTITY(1,1) NOT NULL,
               
                 [Name] [nvarchar] (50) NOT NULL,
               
                  [TemplatePath] [nvarchar]
               		(max) NULL,
               
                  [IsInUse] [bit] NULL,
               	[CreatedDate] [datetime] NULL,
               	[IsActive] [bit] NULL,
                CONSTRAINT[PK_BillTemplate] PRIMARY KEY CLUSTERED
               (
                  [ID] ASC
               )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
               ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]
               GO
                       SET ANSI_NULLS ON
                       GO
               SET QUOTED_IDENTIFIER ON
               GO
              CREATE TABLE[dbo].[ContactBill]
                (
        
                  [ID][int] IDENTITY(1,1) NOT NULL,
               
                 [Name] [nvarchar] (50) NOT NULL,
               
                  [Surname] [nvarchar] (50) NULL,
               	[Total]
                       [nvarchar]
                       (max) NULL,
               
                   [BillNumber] [nvarchar]
                       (max) NULL,
               
                   [DownloadLink] [nvarchar]
                       (max) NULL,
               
                   [CreatedDate] [datetime] NULL,
                CONSTRAINT[PK_Bills] PRIMARY KEY CLUSTERED
               (
                  [ID] ASC
               )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
               ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]
               GO

      */
