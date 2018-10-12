
CREATE TABLE [dbo].[ItemCodeCategory](
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[DateCreated] [datetime2](7) NULL,
	[DateModified] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


alter table ItemCode add
CategoryID uniqueidentifier FOREIGN KEY REFERENCES ItemCodeCategory(CategoryID)

ALTER TABLE [dbo].[ItemCode]  WITH CHECK ADD  CONSTRAINT [FK__ItemCode__Catego__29221CFB] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[ItemCodeCategory] ([CategoryID])
ON DELETE SET NULL
GO

INSERT INTO [dbo].[ItemCodeCategory]
           ([CategoryID]
           ,[CategoryName]
           ,[DateCreated]
           ,[DateModified])
     VALUES
           ('F1CED345-32AA-41F2-9045-90C02F2C209A','Goods',getdate(),getdate()),
		   ('AFCFFC39-9B74-4B3F-AE10-C011FDFC366B','Works',getdate(),getdate()),
		   ('6710ECD3-B081-476C-B1BF-DA2FE63B60BF','Services',getdate(),getdate())
GO

INSERT INTO [dbo].[ItemCodeCategory]
           ([CategoryID]
           ,[CategoryName]
           ,[DateCreated]
           ,[DateModified])
     VALUES
           ('F1CED345-32AA-41F2-9045-90C02F2C209A','Goods',getdate(),getdate()),
		   ('AFCFFC39-9B74-4B3F-AE10-C011FDFC366B','Works',getdate(),getdate()),
		   ('6710ECD3-B081-476C-B1BF-DA2FE63B60BF','Services',getdate(),getdate())
GO
