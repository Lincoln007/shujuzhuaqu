USE [yiyilandb]
GO
/****** Object:  Table [dbo].[imagedetail]    Script Date: 2018/6/19 20:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[imagedetail](
	[id] [nchar](64) NOT NULL,
	[images] [text] NULL,
	[details] [text] NULL,
	[videourl] [text] NULL,
 CONSTRAINT [PK_imagedetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[productparams]    Script Date: 2018/6/19 20:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productparams](
	[id] [nchar](64) NOT NULL,
	[paramlabel] [nchar](64) NOT NULL,
	[paramvalue] [nchar](1024) NULL,
 CONSTRAINT [PK_productparams] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[paramlabel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[products]    Script Date: 2018/6/19 20:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [nchar](64) NOT NULL,
	[title] [nchar](128) NULL,
	[description] [nchar](128) NULL,
	[params] [text] NULL,
	[current_price_min] [numeric](18, 0) NULL,
	[current_price_max] [numeric](18, 0) NULL,
	[original_price_min] [numeric](18, 0) NULL,
	[original_price_max] [numeric](18, 0) NULL,
	[month_sales_count] [int] NULL,
	[stock] [int] NULL,
	[skunumber] [int] NULL,
	[shipping_address] [nchar](64) NULL,
	[shop_id] [nchar](64) NULL,
	[category_id] [nchar](64) NULL,
	[keyword] [nchar](64) NULL,
	[comments_count] [int] NULL,
	[stores_count] [int] NULL,
	[score] [numeric](18, 0) NULL,
	[uri] [nchar](256) NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[productskus]    Script Date: 2018/6/19 20:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productskus](
	[id] [nchar](64) NOT NULL,
	[skname] [nchar](64) NOT NULL,
	[imageurl] [nchar](1024) NULL,
 CONSTRAINT [PK_productskus] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[skname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[shops]    Script Date: 2018/6/19 20:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shops](
	[shop_id] [nchar](64) NOT NULL,
	[shop_name] [nchar](64) NULL,
 CONSTRAINT [PK_shops] PRIMARY KEY CLUSTERED 
(
	[shop_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
