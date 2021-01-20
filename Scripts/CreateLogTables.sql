CREATE TABLE [dbo].[ApplicationLogs](  
    [Id] [int] IDENTITY(1,1) NOT NULL, 
	[ApplicationName] [varchar](255) not null,
    [Date] [datetime] NOT NULL,  
    [Thread] [varchar](255) NOT NULL,  
    [Level] [varchar](50) NOT NULL,  
    [Logger] [varchar](255) NOT NULL,  
    [Message] [nvarchar](Max) NOT NULL,  
    [Exception] [nvarchar](Max) NULL, 
	[ClassName] [varchar](1000) null,
	[MethodName] [nvarchar](1000) NULL,  
	[Request][nvarchar](max) null,
	[Response][nvarchar](max) null
 CONSTRAINT [PK_Log4NetLog] PRIMARY KEY CLUSTERED   
(  
    [Id] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY] 