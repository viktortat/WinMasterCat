--select * from [tmp_ProductAll]


CREATE TABLE [dbo].[tmp_ProductAll](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idRow] [int] NULL,
	[PropertyName] [nvarchar](max) NULL,
	[PropertyVal] [nvarchar](max) NULL,
	[FileName] [nvarchar](max) NULL,
	[DirName] [nvarchar](max) NULL,	
	[FileDate] [datetimeoffset](7) NOT NULL,	
	[DateAdd] [datetimeoffset](7) NOT NULL,
	[TransId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.tmp_ProductAll] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


