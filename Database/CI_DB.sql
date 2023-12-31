USE [hello1]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[admins]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admins](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_admins] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[banners]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[banners](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[image] [nvarchar](1024) NOT NULL,
	[sort_order] [int] NOT NULL,
	[description] [nvarchar](4000) NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_banners] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[country_id] [bigint] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_cities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cms_pages]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_pages](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](128) NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[slug] [nvarchar](64) NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_cms_pages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[countries]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countries](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_countries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[favourite_missions]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[favourite_missions](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[is_favourite] [bit] NOT NULL,
	[volunteer_id] [bigint] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_favourite_missions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_applications]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_applications](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[applied_on] [datetimeoffset](7) NOT NULL,
	[status] [int] NOT NULL,
	[volunteer_id] [bigint] NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_mission_applications] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_comments]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_comments](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[text] [nvarchar](1024) NOT NULL,
	[volunteer_id] [bigint] NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_mission_comments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_media]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_media](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[name] [nvarchar](1024) NOT NULL,
	[type] [nvarchar](max) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_mission_media] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_ratings]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_ratings](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[rating] [int] NOT NULL,
	[volunteer_id] [bigint] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_mission_ratings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_skills]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_skills](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[skill_id] [int] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_mission_skills] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_themes]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_themes](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_mission_themes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[missions]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[missions](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[short_description] [nvarchar](1024) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[start_date] [datetimeoffset](7) NOT NULL,
	[end_date] [datetimeoffset](7) NULL,
	[registration_deadline] [datetimeoffset](7) NULL,
	[total_seat] [int] NULL,
	[mission_type] [int] NOT NULL,
	[organization_name] [nvarchar](128) NULL,
	[organization_details] [nvarchar](max) NULL,
	[status] [int] NOT NULL,
	[availability] [int] NULL,
	[city_id] [bigint] NOT NULL,
	[mission_theme_id] [bigint] NOT NULL,
	[goal_objective_title] [nvarchar](128) NULL,
	[goal_objective] [bigint] NULL,
	[goal_objective_achieved] [bigint] NULL,
	[thumbnail_url] [nvarchar](1024) NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_missions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[skills]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[skills](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_skills] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stories]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stories](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[short_description] [nvarchar](1024) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[status] [int] NOT NULL,
	[views] [bigint] NOT NULL,
	[published_at] [datetimeoffset](7) NULL,
	[volunteer_id] [bigint] NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_stories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[story_media]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[story_media](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[story_id] [bigint] NOT NULL,
	[path] [nvarchar](1024) NOT NULL,
	[type] [nvarchar](max) NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_story_media] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[timesheets]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[timesheets](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[volunteer_id] [bigint] NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[time] [time](7) NULL,
	[goal_achieved] [int] NULL,
	[date_volunteered] [datetimeoffset](7) NOT NULL,
	[notes] [nvarchar](max) NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
 CONSTRAINT [PK_timesheets] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](20) NOT NULL,
	[last_name] [nvarchar](20) NOT NULL,
	[email] [nvarchar](128) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[salt] [nvarchar](1024) NOT NULL,
	[token] [nvarchar](1024) NULL,
	[avatar] [nvarchar](1024) NOT NULL,
	[user_type] [int] NOT NULL,
	[status] [int] NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[modified_on] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[volunteer_skills]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[volunteer_skills](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[volunteer_id] [bigint] NOT NULL,
	[skill_id] [int] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_volunteer_skills] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[volunteers]    Script Date: 31-07-2023 09:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[volunteers](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[phone_number] [nvarchar](20) NOT NULL,
	[employee_id] [nvarchar](8) NULL,
	[department] [nvarchar](20) NULL,
	[profile_text] [nvarchar](1024) NULL,
	[city_id] [bigint] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[modified_by] [bigint] NOT NULL,
	[availability] [int] NOT NULL,
	[reason_to_be_volunteer] [nvarchar](4000) NULL,
 CONSTRAINT [PK_volunteers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230621084855_CreateDb', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230621093608_MissionCRUD', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230622121717_BannerEntity', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230622121753_CMSCRUD', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230623102720_CMSUniqueSlug', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230623122822_CMSTitleNotUnique', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230626073254_StoryAndStoryMediaEntity', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230627040501_MissionRatings', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230627124324_dbcontect-default-avatar', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230627125240_FavouriteMission', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628042058_AddedIsFavourite', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628052710_DefaultTrueIsFavourite', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628061334_update-volunteer', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628085555_ColumnUpdateInRatingAndFav', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230628114312_AddMissionApplication', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230630044419_removeSortOrderIndexFromBanner', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230703053305_FavMissionAndMissionRating', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230703095726_MissionComments', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230703123741_AddCollectionMissionApplication', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230704122656_CreateTimesheet', N'6.0.16')
SET IDENTITY_INSERT [dbo].[admins] ON 

INSERT [dbo].[admins] ([id], [user_id], [created_by], [modified_by]) VALUES (1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[admins] OFF
SET IDENTITY_INSERT [dbo].[banners] ON 

INSERT [dbo].[banners] ([id], [image], [sort_order], [description], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, N'/assets/banner/CSR-initiative-stands-for-Coffee--and-Farmer-Equity.png', 1, N'At [Organization Name], our mission is to promote a sustainable and greener future by actively participating in tree planting initiatives. We firmly believe that trees play a vital role in maintaining a healthy environment, combating climate change, and improving the overall well-being of our communities. Through our dedicated efforts, we aim to foster a culture of tree planting, nurture ecosystems, and inspire individuals to become stewards of the environment.
Our mission begins with raising awareness about the importance of trees and their positive impact on the planet. We strive to educate individuals, schools, businesses, and local communities about the numerous benefits of trees, such as improving air quality, conserving water, providing habitat for wildlife, and reducing the carbon footprint. By highlighting the intrinsic value of trees, we aim to inspire a sense of responsibility and motivate people to actively participate in our tree planting initiatives.', 1, CAST(N'2023-06-26T13:02:54.0323991+05:30' AS DateTimeOffset), CAST(N'2023-06-26T13:02:54.0323991+05:30' AS DateTimeOffset), 1, 1)
SET IDENTITY_INSERT [dbo].[banners] OFF
SET IDENTITY_INSERT [dbo].[cities] ON 

INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, 1, N'Rajkot', 1, CAST(N'2023-06-28T07:10:25.4900000+00:00' AS DateTimeOffset), CAST(N'2023-06-28T07:10:25.4900000+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (2, 1, N'Baroda', 1, CAST(N'2023-07-05T05:11:57.0500230+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:48:56.3600814+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (3, 12, N'Las Vegas', 1, CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (4, 14, N'Kazakhastan', 1, CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (5, 15, N'Toronto', 1, CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (6, 16, N'Rio De Janerio', 1, CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (7, 1, N'fsdfdfsd', 1, CAST(N'2023-07-25T05:17:50.6268145+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:17:50.6288430+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (8, 1, N'jcnnv', 1, CAST(N'2023-07-25T05:21:11.6718111+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:21:11.7135048+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[cities] ([id], [country_id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (9, 1, N'vvvfv', 10, CAST(N'2023-07-25T05:41:15.3065777+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:41:32.2962164+00:00' AS DateTimeOffset), 1, 1)
SET IDENTITY_INSERT [dbo].[cities] OFF
SET IDENTITY_INSERT [dbo].[cms_pages] ON 

INSERT [dbo].[cms_pages] ([id], [title], [description], [slug], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, N'strings', N'strings', N'strings', 10, CAST(N'2023-07-05T05:48:30.6424364+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:49:40.5620066+00:00' AS DateTimeOffset), 1, 1)
SET IDENTITY_INSERT [dbo].[cms_pages] OFF
SET IDENTITY_INSERT [dbo].[countries] ON 

INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, N'india', 1, CAST(N'2023-06-28T07:10:03.8800000+00:00' AS DateTimeOffset), CAST(N'2023-07-03T04:51:07.6884668+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (12, N'germany', 1, CAST(N'2023-07-05T05:02:12.5278393+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:02:12.5578957+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (14, N'russia', 1, CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (15, N'USA', 1, CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (16, N'Singapore', 1, CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-06T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (17, N'toronto', 1, CAST(N'2023-07-10T12:20:05.8254154+00:00' AS DateTimeOffset), CAST(N'2023-07-10T12:20:05.8606415+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (23, N'Zxsdfgdgfthygjh', 1, CAST(N'2023-07-25T05:11:19.5479535+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:11:19.5873805+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (24, N'sdfsdfdsf', 1, CAST(N'2023-07-25T05:17:33.2793691+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:17:33.3265076+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (25, N'DSFSDFDFSD', 10, CAST(N'2023-07-25T05:24:29.0444878+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:26:38.1639616+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (26, N'cdcdscsd', 10, CAST(N'2023-07-25T05:40:36.9593407+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:40:43.4070387+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[countries] ([id], [name], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (27, N'Belgium', 1, CAST(N'2023-07-31T03:53:49.7616709+00:00' AS DateTimeOffset), CAST(N'2023-07-31T03:53:49.7937902+00:00' AS DateTimeOffset), 1, 1)
SET IDENTITY_INSERT [dbo].[countries] OFF
SET IDENTITY_INSERT [dbo].[favourite_missions] ON 

INSERT [dbo].[favourite_missions] ([id], [mission_id], [is_favourite], [volunteer_id], [created_on], [modified_on]) VALUES (4, 1, 1, 2, CAST(N'2023-07-03T13:13:39.9864246+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:05:21.3963635+00:00' AS DateTimeOffset))
INSERT [dbo].[favourite_missions] ([id], [mission_id], [is_favourite], [volunteer_id], [created_on], [modified_on]) VALUES (6, 1, 1, 1, CAST(N'2023-07-04T05:12:41.7636741+00:00' AS DateTimeOffset), CAST(N'2023-07-04T05:31:54.5667678+00:00' AS DateTimeOffset))
INSERT [dbo].[favourite_missions] ([id], [mission_id], [is_favourite], [volunteer_id], [created_on], [modified_on]) VALUES (7, 1, 1, 3, CAST(N'2023-07-04T10:37:27.5240705+00:00' AS DateTimeOffset), CAST(N'2023-07-07T06:43:08.5755347+00:00' AS DateTimeOffset))
INSERT [dbo].[favourite_missions] ([id], [mission_id], [is_favourite], [volunteer_id], [created_on], [modified_on]) VALUES (8, 2, 1, 3, CAST(N'2023-07-05T05:56:36.2487338+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:56:38.9880985+00:00' AS DateTimeOffset))
INSERT [dbo].[favourite_missions] ([id], [mission_id], [is_favourite], [volunteer_id], [created_on], [modified_on]) VALUES (9, 4, 0, 3, CAST(N'2023-07-07T09:57:46.4325287+00:00' AS DateTimeOffset), CAST(N'2023-07-07T09:57:48.8226249+00:00' AS DateTimeOffset))
INSERT [dbo].[favourite_missions] ([id], [mission_id], [is_favourite], [volunteer_id], [created_on], [modified_on]) VALUES (10, 6, 1, 3, CAST(N'2023-07-07T10:41:09.7437545+00:00' AS DateTimeOffset), CAST(N'2023-07-07T10:41:09.7444483+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[favourite_missions] OFF
SET IDENTITY_INSERT [dbo].[mission_applications] ON 

INSERT [dbo].[mission_applications] ([id], [applied_on], [status], [volunteer_id], [mission_id], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, CAST(N'2023-07-01T00:00:00.0000000+05:30' AS DateTimeOffset), 5, 1, 1, CAST(N'2023-07-01T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-01T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
INSERT [dbo].[mission_applications] ([id], [applied_on], [status], [volunteer_id], [mission_id], [created_on], [modified_on], [created_by], [modified_by]) VALUES (2, CAST(N'2023-07-02T00:00:00.0000000+05:30' AS DateTimeOffset), 5, 3, 1, CAST(N'2023-07-02T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-02T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1)
SET IDENTITY_INSERT [dbo].[mission_applications] OFF
SET IDENTITY_INSERT [dbo].[mission_comments] ON 

INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (3, 1, N'test', 1, 4, CAST(N'2023-07-03T12:57:15.2413116+00:00' AS DateTimeOffset), CAST(N'2023-07-03T12:57:15.2127380+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (5, 1, N'string', 2, 4, CAST(N'2023-07-03T13:00:48.1689479+00:00' AS DateTimeOffset), CAST(N'2023-07-03T13:00:48.1414878+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (6, 1, N'string', 2, 4, CAST(N'2023-07-03T13:04:17.3280491+00:00' AS DateTimeOffset), CAST(N'2023-07-03T13:04:17.2977160+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (7, 1, N'test', 2, 4, CAST(N'2023-07-03T13:04:36.0047726+00:00' AS DateTimeOffset), CAST(N'2023-07-03T13:04:36.0047718+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (9, 1, N'string', 2, 4, CAST(N'2023-07-04T04:45:02.7575604+00:00' AS DateTimeOffset), CAST(N'2023-07-04T04:45:02.7275520+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (10, 1, N'hello', 2, 4, CAST(N'2023-07-04T04:45:09.4539350+00:00' AS DateTimeOffset), CAST(N'2023-07-04T04:45:09.4539343+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (11, 1, N'hello', 2, 4, CAST(N'2023-07-04T05:04:05.8227864+00:00' AS DateTimeOffset), CAST(N'2023-07-04T05:04:05.8221049+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (12, 1, N'demo', 2, 4, CAST(N'2023-07-04T05:04:13.2896359+00:00' AS DateTimeOffset), CAST(N'2023-07-04T05:04:13.2896354+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (13, 1, N'testing mapping', 1, 4, CAST(N'2023-07-04T05:12:10.1345129+00:00' AS DateTimeOffset), CAST(N'2023-07-04T05:12:10.1345121+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (14, 1, N'testing changes', 1, 4, CAST(N'2023-07-04T05:32:04.5234149+00:00' AS DateTimeOffset), CAST(N'2023-07-04T05:32:04.5227351+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (15, 1, N'testing status', 1, 4, CAST(N'2023-07-04T05:32:15.7598217+00:00' AS DateTimeOffset), CAST(N'2023-07-04T05:32:15.7598212+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (16, 1, N'string', 2, 4, CAST(N'2023-07-04T06:39:31.5000338+00:00' AS DateTimeOffset), CAST(N'2023-07-04T06:39:31.4691831+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (17, 1, N'string', 2, 4, CAST(N'2023-07-04T06:48:15.6258431+00:00' AS DateTimeOffset), CAST(N'2023-07-04T06:48:15.5934773+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (18, 1, N'string', 2, 4, CAST(N'2023-07-04T10:06:07.8632159+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:06:07.8639504+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (19, 1, N'string', 3, 4, CAST(N'2023-07-04T10:38:07.6069869+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:38:07.6078658+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (20, 1, N'string1', 1, 4, CAST(N'2023-07-04T10:46:29.2594980+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:46:29.2602140+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (21, 1, N'success', 3, 4, CAST(N'2023-07-04T10:46:34.5832462+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:46:34.5832469+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (22, 1, N'hello', 3, 4, CAST(N'2023-07-04T12:38:39.4969653+00:00' AS DateTimeOffset), CAST(N'2023-07-04T12:38:39.5245936+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (23, 1, N'this is me', 3, 4, CAST(N'2023-07-04T13:06:29.8366109+00:00' AS DateTimeOffset), CAST(N'2023-07-04T13:06:29.8682233+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (24, 2, N'string', 3, 4, CAST(N'2023-07-05T04:24:32.7303583+00:00' AS DateTimeOffset), CAST(N'2023-07-05T04:24:32.7311467+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (25, 2, N'string', 3, 4, CAST(N'2023-07-05T05:54:48.9883149+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:54:49.0201474+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (26, 2, N'harsh', 3, 4, CAST(N'2023-07-05T05:56:25.2359708+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:56:25.2359714+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (27, 2, N'today', 3, 4, CAST(N'2023-07-05T11:31:31.4461498+00:00' AS DateTimeOffset), CAST(N'2023-07-05T11:31:31.4759808+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (28, 1, N'tomorrow', 3, 4, CAST(N'2023-07-05T11:31:47.2689060+00:00' AS DateTimeOffset), CAST(N'2023-07-05T11:31:47.2689066+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (29, 4, N'', 3, 4, CAST(N'2023-07-07T09:39:26.7152080+00:00' AS DateTimeOffset), CAST(N'2023-07-07T09:39:26.7430749+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_comments] ([id], [mission_id], [text], [volunteer_id], [status], [created_on], [modified_on]) VALUES (30, 4, N' ', 3, 4, CAST(N'2023-07-07T10:41:24.8871945+00:00' AS DateTimeOffset), CAST(N'2023-07-07T10:41:24.8879206+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[mission_comments] OFF
SET IDENTITY_INSERT [dbo].[mission_media] ON 

INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (1, N'Screenshot (16).png', N'/assets/mission-images/1_e3de1423-928a-4eba-bddf-413cd0e3cef9_Screenshot (16).png', N'image/png', 1, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (2, N'syncup-6_2.pdf', N'/assets/mission-documents/1_dee9092b-8c4b-452f-8cf1-eb36263a5a19_syncup-6_2.pdf', N'application/pdf', 1, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (3, N'Screenshot (28).png', N'/assets/mission-images/1_20853602-18bc-4ae1-90fd-23fd3d7833a9_Screenshot (28).png', N'image/png', 2, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (4, N'Screenshot (19).png', N'/assets/mission-images/1_d0fe2557-7229-49b2-a04b-da2e3941fc9c_Screenshot (19).png', N'image/png', 2, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (5, N'FeaturesListing.pdf', N'/assets/mission-documents/1_4fd8adff-2751-4e21-8784-43238f172940_FeaturesListing.pdf', N'application/pdf', 2, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (6, N'bg.jpg', N'/assets/mission-images/1_38fdc3d8-f472-4939-a96e-95523b758e76_bg.jpg', N'image/jpeg', 3, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (7, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'/assets/mission-images/1_898ec8c3-7299-473b-8d82-23b830dd2768_CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'image/png', 3, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (8, N'syncup-6_2.pdf', N'/assets/mission-documents/1_0a04970d-ef7d-4d1c-b63d-c3e5fa243a22_syncup-6_2.pdf', N'application/pdf', 3, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (9, N'bg.jpg', N'/assets/mission-images/1_25682f7e-32f5-48a0-a6a5-1b4ffa126487_bg.jpg', N'image/jpeg', 4, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (10, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'/assets/mission-images/1_0135db39-db50-48eb-ae88-64ea44c51fa8_CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'image/png', 4, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (11, N'syncup-6_2.pdf', N'/assets/mission-documents/1_29f15962-a97c-4861-819d-a85dcf41b87e_syncup-6_2.pdf', N'application/pdf', 4, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (12, N'bg.jpg', N'/assets/mission-images/1_2e3f04ab-3dcb-4f1f-a6b5-221569d8ddb3_bg.jpg', N'image/jpeg', 5, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (13, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'/assets/mission-images/1_69788d0e-e246-4454-8bf3-41b886e9fe92_CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'image/png', 5, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (14, N'syncup-6_2.pdf', N'/assets/mission-documents/1_492d6038-72f3-4a4f-9549-108617a3ee3b_syncup-6_2.pdf', N'application/pdf', 5, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (15, N'bg.jpg', N'/assets/mission-images/1_13a98524-8e61-44e6-b87d-7bbae11739b3_bg.jpg', N'image/jpeg', 6, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (16, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'/assets/mission-images/1_ee38900f-b2a6-4615-b8c9-c5665f57cb7e_CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'image/png', 6, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (17, N'syncup-6_2.pdf', N'/assets/mission-documents/1_c65f38c7-5ce3-4b24-ae1f-ce2bed393704_syncup-6_2.pdf', N'application/pdf', 6, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (18, N'bg.jpg', N'/assets/mission-images/1_a1865017-0708-4420-864c-d81fccb3873d_bg.jpg', N'image/jpeg', 7, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (19, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'/assets/mission-images/1_d0db505b-e786-4613-82b4-9ffb6397dbee_CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png', N'image/png', 7, 1)
INSERT [dbo].[mission_media] ([id], [title], [name], [type], [mission_id], [status]) VALUES (20, N'syncup-6_2.pdf', N'/assets/mission-documents/1_36e3581f-53a6-45ee-9c49-4b3c3c5a85ac_syncup-6_2.pdf', N'application/pdf', 7, 1)
SET IDENTITY_INSERT [dbo].[mission_media] OFF
SET IDENTITY_INSERT [dbo].[mission_ratings] ON 

INSERT [dbo].[mission_ratings] ([id], [mission_id], [rating], [volunteer_id], [created_on], [modified_on]) VALUES (1, 1, 3, 2, CAST(N'2023-07-03T09:29:13.3511651+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:05:40.8599966+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_ratings] ([id], [mission_id], [rating], [volunteer_id], [created_on], [modified_on]) VALUES (2, 1, 5, 1, CAST(N'2023-07-03T12:39:54.6998362+00:00' AS DateTimeOffset), CAST(N'2023-07-04T05:31:36.0776352+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_ratings] ([id], [mission_id], [rating], [volunteer_id], [created_on], [modified_on]) VALUES (4, 1, 4, 3, CAST(N'2023-07-04T10:37:49.0555577+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:45:53.4374950+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_ratings] ([id], [mission_id], [rating], [volunteer_id], [created_on], [modified_on]) VALUES (5, 2, 3, 3, CAST(N'2023-07-05T05:56:47.7009674+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:56:47.7016344+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_ratings] ([id], [mission_id], [rating], [volunteer_id], [created_on], [modified_on]) VALUES (6, 4, 2, 3, CAST(N'2023-07-07T09:56:12.8880790+00:00' AS DateTimeOffset), CAST(N'2023-07-07T09:56:16.0751011+00:00' AS DateTimeOffset))
INSERT [dbo].[mission_ratings] ([id], [mission_id], [rating], [volunteer_id], [created_on], [modified_on]) VALUES (7, 3, 5, 3, CAST(N'2023-07-07T10:41:03.7561881+00:00' AS DateTimeOffset), CAST(N'2023-07-07T10:41:03.7833485+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[mission_ratings] OFF
SET IDENTITY_INSERT [dbo].[mission_themes] ON 

INSERT [dbo].[mission_themes] ([id], [title], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, N'writing', 1, CAST(N'2023-06-28T07:15:37.2900000+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:50:17.9649300+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[mission_themes] ([id], [title], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (2, N'dancing', 1, CAST(N'2023-07-05T05:50:40.2424688+00:00' AS DateTimeOffset), CAST(N'2023-07-05T05:50:40.2435406+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[mission_themes] ([id], [title], [status], [created_on], [modified_on], [created_by], [modified_by]) VALUES (3, N'vfdfdvbgfb', 1, CAST(N'2023-07-25T05:22:29.3373789+00:00' AS DateTimeOffset), CAST(N'2023-07-25T05:22:59.3221378+00:00' AS DateTimeOffset), 1, 1)
SET IDENTITY_INSERT [dbo].[mission_themes] OFF
SET IDENTITY_INSERT [dbo].[missions] ON 

INSERT [dbo].[missions] ([id], [title], [short_description], [description], [start_date], [end_date], [registration_deadline], [total_seat], [mission_type], [organization_name], [organization_details], [status], [availability], [city_id], [mission_theme_id], [goal_objective_title], [goal_objective], [goal_objective_achieved], [thumbnail_url], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, N'test', N'test', N'test', CAST(N'2023-07-01T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-10T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-02T00:00:00.0000000+05:30' AS DateTimeOffset), 100, 2, N'test', N'test', 1, 1, 1, 1, NULL, NULL, NULL, N'/assets/mission-images/1_ea32fdd0-648a-439e-9a61-f7b9efb71c16_Screenshot (8).png', CAST(N'2023-06-28T07:15:42.9372446+00:00' AS DateTimeOffset), CAST(N'2023-06-28T07:15:42.9357490+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[missions] ([id], [title], [short_description], [description], [start_date], [end_date], [registration_deadline], [total_seat], [mission_type], [organization_name], [organization_details], [status], [availability], [city_id], [mission_theme_id], [goal_objective_title], [goal_objective], [goal_objective_achieved], [thumbnail_url], [created_on], [modified_on], [created_by], [modified_by]) VALUES (2, N'testing', N'testing', N'testing', CAST(N'2023-07-05T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-10-05T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-10T00:00:00.0000000+05:30' AS DateTimeOffset), 150, 2, N'demo', N'demo', 1, 2, 4, 2, NULL, NULL, NULL, N'/assets/mission-images/1_62efa1a1-43f4-403d-a530-84b9c3245ad9_Screenshot (10).png', CAST(N'2023-07-05T04:23:42.0934065+00:00' AS DateTimeOffset), CAST(N'2023-07-05T04:23:42.1267176+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[missions] ([id], [title], [short_description], [description], [start_date], [end_date], [registration_deadline], [total_seat], [mission_type], [organization_name], [organization_details], [status], [availability], [city_id], [mission_theme_id], [goal_objective_title], [goal_objective], [goal_objective_achieved], [thumbnail_url], [created_on], [modified_on], [created_by], [modified_by]) VALUES (3, N'mission one', N'mission one', N'mission one', CAST(N'2023-07-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-27T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-10T00:00:00.0000000+05:30' AS DateTimeOffset), 250, 2, N'mission one', N'mission one', 1, 4, 4, 1, NULL, NULL, NULL, N'/assets/mission-images/1_a6f80f5e-8f7c-4d73-8217-79d12aa86f92_Screenshot (51).png', CAST(N'2023-07-07T04:46:23.4606074+00:00' AS DateTimeOffset), CAST(N'2023-07-07T04:46:23.4921566+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[missions] ([id], [title], [short_description], [description], [start_date], [end_date], [registration_deadline], [total_seat], [mission_type], [organization_name], [organization_details], [status], [availability], [city_id], [mission_theme_id], [goal_objective_title], [goal_objective], [goal_objective_achieved], [thumbnail_url], [created_on], [modified_on], [created_by], [modified_by]) VALUES (4, N'mission two', N'mission two', N'mission two', CAST(N'2023-07-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-27T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-10T00:00:00.0000000+05:30' AS DateTimeOffset), 740, 2, N'mission two', N'mission two', 1, 4, 3, 2, NULL, NULL, NULL, N'/assets/mission-images/1_82813dbf-609c-4c10-bf54-920a74da5fe6_Grow-Trees-On-the-path-to-environment-sustainability.png', CAST(N'2023-07-07T04:47:16.2055224+00:00' AS DateTimeOffset), CAST(N'2023-07-07T04:47:16.2055232+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[missions] ([id], [title], [short_description], [description], [start_date], [end_date], [registration_deadline], [total_seat], [mission_type], [organization_name], [organization_details], [status], [availability], [city_id], [mission_theme_id], [goal_objective_title], [goal_objective], [goal_objective_achieved], [thumbnail_url], [created_on], [modified_on], [created_by], [modified_by]) VALUES (5, N'mission three', N'mission three', N'mission three', CAST(N'2023-07-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-27T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-10T00:00:00.0000000+05:30' AS DateTimeOffset), 102, 2, N'mission three', N'mission three', 1, 4, 2, 2, NULL, NULL, NULL, N'/assets/mission-images/1_f668c8c8-0928-48a9-bdc5-90f4a998e8ee_Education-Supplies-for-Every--Pair-of-Shoes-Sold.png', CAST(N'2023-07-07T04:47:53.7448467+00:00' AS DateTimeOffset), CAST(N'2023-07-07T04:47:53.7448478+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[missions] ([id], [title], [short_description], [description], [start_date], [end_date], [registration_deadline], [total_seat], [mission_type], [organization_name], [organization_details], [status], [availability], [city_id], [mission_theme_id], [goal_objective_title], [goal_objective], [goal_objective_achieved], [thumbnail_url], [created_on], [modified_on], [created_by], [modified_by]) VALUES (6, N'mission four', N'mission four', N'mission four', CAST(N'2023-07-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-27T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-10T00:00:00.0000000+05:30' AS DateTimeOffset), 85, 2, N'mission four', N'mission four', 1, 3, 5, 1, NULL, NULL, NULL, N'/assets/mission-images/1_04aba289-3dad-4689-88f0-b0b292e0a2c4_404-Page-image.png', CAST(N'2023-07-07T04:48:33.8308455+00:00' AS DateTimeOffset), CAST(N'2023-07-07T04:48:33.8308465+00:00' AS DateTimeOffset), 1, 1)
INSERT [dbo].[missions] ([id], [title], [short_description], [description], [start_date], [end_date], [registration_deadline], [total_seat], [mission_type], [organization_name], [organization_details], [status], [availability], [city_id], [mission_theme_id], [goal_objective_title], [goal_objective], [goal_objective_achieved], [thumbnail_url], [created_on], [modified_on], [created_by], [modified_by]) VALUES (7, N'mission five', N'mission five', N'mission five', CAST(N'2023-07-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-27T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-07-10T00:00:00.0000000+05:30' AS DateTimeOffset), 85, 2, N'mission five', N'mission five', 1, 2, 1, 1, NULL, NULL, NULL, N'/assets/mission-images/1_fb6ea59a-41bc-41c1-aae5-78d1c733f4e8_404-Page-image.png', CAST(N'2023-07-07T04:49:35.3999445+00:00' AS DateTimeOffset), CAST(N'2023-07-07T04:49:35.3999458+00:00' AS DateTimeOffset), 1, 1)
SET IDENTITY_INSERT [dbo].[missions] OFF
SET IDENTITY_INSERT [dbo].[stories] ON 

INSERT [dbo].[stories] ([id], [title], [short_description], [description], [status], [views], [published_at], [volunteer_id], [mission_id], [created_on], [modified_on], [created_by], [modified_by]) VALUES (1, N'hello', N'hello', N'hello', 3, 0, NULL, 3, 1, CAST(N'2023-07-07T09:49:50.7706925+00:00' AS DateTimeOffset), CAST(N'2023-07-07T09:49:50.8016888+00:00' AS DateTimeOffset), 4, 4)
SET IDENTITY_INSERT [dbo].[stories] OFF
SET IDENTITY_INSERT [dbo].[story_media] ON 

INSERT [dbo].[story_media] ([id], [story_id], [path], [type], [status]) VALUES (1, 1, N'/assets/story-media/4_6c5316b1-5e8b-4e7f-99ab-089a161ea9cf_CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4.png', N'image/png', 1)
SET IDENTITY_INSERT [dbo].[story_media] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [first_name], [last_name], [email], [password], [salt], [token], [avatar], [user_type], [status], [created_on], [modified_on]) VALUES (1, N'admin', N'admin', N'noreply.ci.tatvasoft@gmail.com', N'2ED0227C628B3FE95C30B387A0B4AC0382648BCF1AA59882C2D68F216080DEBE994A9A3DA9CA59E5B67E8A73DE2583B1220A098B20F3C4BE2511B737B09BBB6D', N'E29C23D44A0CD818CDD21A8D01C3C7326A372A172B4071359FFA76379A8D1D6443265653161768992CC36D262780A79BA38F1ED697729FA6B35A536148355157', NULL, N'/assets/avatar/profile.png', 2, 1, CAST(N'2023-06-26T13:02:54.0323991+05:30' AS DateTimeOffset), CAST(N'2023-06-26T13:02:54.0323991+05:30' AS DateTimeOffset))
INSERT [dbo].[users] ([id], [first_name], [last_name], [email], [password], [salt], [token], [avatar], [user_type], [status], [created_on], [modified_on]) VALUES (2, N'Jenil', N'Vora', N'jenil.vora0408@gmail.com', N'C3DA105B7EA737C7E64E3F7A47D1701BD6AF73F8383982FD33361EA3ECEC296780C09D6219EA5C0967C7F6F990C9AADAE24044F82E111DDF0AC51008D28CD28A', N'48393E4360F36D9A96CE7F08FB95D304527C65815DD1CC249C398190E7DAFA9CFC6D797EAD0E2E6507853461762018861C7170476A882620CDB5B5DE702DC5AA', NULL, N'/assets/avatar/profile.png', 1, 1, CAST(N'2023-06-28T07:10:45.6306684+00:00' AS DateTimeOffset), CAST(N'2023-06-28T07:10:45.6010180+00:00' AS DateTimeOffset))
INSERT [dbo].[users] ([id], [first_name], [last_name], [email], [password], [salt], [token], [avatar], [user_type], [status], [created_on], [modified_on]) VALUES (3, N'demo', N'demo', N'demo@gmail.com', N'69F3086F236EA39CF6E44FC9DC18312EF524D9F8E11FDE2FFE31E2FA7EC57DC41155FC6E8E0C00A14B1F88A130DDFA0531560EE0E2C2CB6FA0326B0B33EDB86C', N'381CE80A54E98FFE1F6DBC3FB9BC016C6C160C027894D0F7A521982B7496E120A4295E882CD5E1305EBF17BAD6D05954A2161284EE5ADC1024B0C30F3571D363', NULL, N'/assets/avatar/profile.png', 1, 1, CAST(N'2023-07-03T08:59:22.7018612+00:00' AS DateTimeOffset), CAST(N'2023-07-03T08:59:22.6654793+00:00' AS DateTimeOffset))
INSERT [dbo].[users] ([id], [first_name], [last_name], [email], [password], [salt], [token], [avatar], [user_type], [status], [created_on], [modified_on]) VALUES (4, N'test', N'test', N'test@gmail.com', N'FEF1059DEEC2DED211BB82BE460A4FB25843A811A4FA036830240D0C502367E03F472187999F683EF769DD85203D85F94D6B5799620970A11CEAF59574A05210', N'EB74C1319E99B67767DE7C72F1EC221A4C41D035CC06E09D0BEB8B3520B4503612F2B3C80D3DF10C35599E27CBB4245334CA3985F760C79AF61CB9A05FFFA2FF', NULL, N'/assets/avatar/profile.png', 1, 1, CAST(N'2023-07-04T10:09:06.3834335+00:00' AS DateTimeOffset), CAST(N'2023-07-04T10:09:06.3841065+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[users] OFF
SET IDENTITY_INSERT [dbo].[volunteers] ON 

INSERT [dbo].[volunteers] ([id], [phone_number], [employee_id], [department], [profile_text], [city_id], [user_id], [created_by], [modified_by], [availability], [reason_to_be_volunteer]) VALUES (1, N'9328378727', N'1', N'1', N'jenil', 1, 2, 1, 1, 1, NULL)
INSERT [dbo].[volunteers] ([id], [phone_number], [employee_id], [department], [profile_text], [city_id], [user_id], [created_by], [modified_by], [availability], [reason_to_be_volunteer]) VALUES (2, N'1236547890', N'1', N'1', N'demo', 1, 3, 1, 1, 1, NULL)
INSERT [dbo].[volunteers] ([id], [phone_number], [employee_id], [department], [profile_text], [city_id], [user_id], [created_by], [modified_by], [availability], [reason_to_be_volunteer]) VALUES (3, N'7856587410', N'1', N'1', N'test', 1, 4, 1, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[volunteers] OFF
ALTER TABLE [dbo].[banners] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[banners] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[banners] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[cities] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[cities] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[cities] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[cms_pages] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[cms_pages] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[cms_pages] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[countries] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[countries] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[countries] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[favourite_missions] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[favourite_missions] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[mission_comments] ADD  DEFAULT ((4)) FOR [status]
GO
ALTER TABLE [dbo].[mission_comments] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[mission_comments] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[mission_ratings] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[mission_ratings] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[mission_themes] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[mission_themes] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[mission_themes] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[skills] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[skills] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[skills] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[stories] ADD  DEFAULT ((3)) FOR [status]
GO
ALTER TABLE [dbo].[stories] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[stories] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[timesheets] ADD  DEFAULT ((4)) FOR [status]
GO
ALTER TABLE [dbo].[timesheets] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[timesheets] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ('/assets/avatar/profile.png') FOR [avatar]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((1)) FOR [user_type]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (getutcdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (getutcdate()) FOR [modified_on]
GO
ALTER TABLE [dbo].[volunteers] ADD  DEFAULT ((1)) FOR [availability]
GO
ALTER TABLE [dbo].[admins]  WITH CHECK ADD  CONSTRAINT [FK_admins_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[admins] CHECK CONSTRAINT [FK_admins_users_created_by]
GO
ALTER TABLE [dbo].[admins]  WITH CHECK ADD  CONSTRAINT [FK_admins_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[admins] CHECK CONSTRAINT [FK_admins_users_modified_by]
GO
ALTER TABLE [dbo].[admins]  WITH CHECK ADD  CONSTRAINT [FK_admins_users_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[admins] CHECK CONSTRAINT [FK_admins_users_user_id]
GO
ALTER TABLE [dbo].[banners]  WITH CHECK ADD  CONSTRAINT [FK_banners_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[banners] CHECK CONSTRAINT [FK_banners_users_created_by]
GO
ALTER TABLE [dbo].[banners]  WITH CHECK ADD  CONSTRAINT [FK_banners_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[banners] CHECK CONSTRAINT [FK_banners_users_modified_by]
GO
ALTER TABLE [dbo].[cities]  WITH CHECK ADD  CONSTRAINT [FK_cities_countries_country_id] FOREIGN KEY([country_id])
REFERENCES [dbo].[countries] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cities] CHECK CONSTRAINT [FK_cities_countries_country_id]
GO
ALTER TABLE [dbo].[cities]  WITH CHECK ADD  CONSTRAINT [FK_cities_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[cities] CHECK CONSTRAINT [FK_cities_users_created_by]
GO
ALTER TABLE [dbo].[cities]  WITH CHECK ADD  CONSTRAINT [FK_cities_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[cities] CHECK CONSTRAINT [FK_cities_users_modified_by]
GO
ALTER TABLE [dbo].[cms_pages]  WITH CHECK ADD  CONSTRAINT [FK_cms_pages_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[cms_pages] CHECK CONSTRAINT [FK_cms_pages_users_created_by]
GO
ALTER TABLE [dbo].[cms_pages]  WITH CHECK ADD  CONSTRAINT [FK_cms_pages_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[cms_pages] CHECK CONSTRAINT [FK_cms_pages_users_modified_by]
GO
ALTER TABLE [dbo].[countries]  WITH CHECK ADD  CONSTRAINT [FK_countries_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[countries] CHECK CONSTRAINT [FK_countries_users_created_by]
GO
ALTER TABLE [dbo].[countries]  WITH CHECK ADD  CONSTRAINT [FK_countries_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[countries] CHECK CONSTRAINT [FK_countries_users_modified_by]
GO
ALTER TABLE [dbo].[favourite_missions]  WITH CHECK ADD  CONSTRAINT [FK_favourite_missions_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[favourite_missions] CHECK CONSTRAINT [FK_favourite_missions_missions_mission_id]
GO
ALTER TABLE [dbo].[favourite_missions]  WITH CHECK ADD  CONSTRAINT [FK_favourite_missions_volunteers_volunteer_id] FOREIGN KEY([volunteer_id])
REFERENCES [dbo].[volunteers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[favourite_missions] CHECK CONSTRAINT [FK_favourite_missions_volunteers_volunteer_id]
GO
ALTER TABLE [dbo].[mission_applications]  WITH CHECK ADD  CONSTRAINT [FK_mission_applications_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_applications] CHECK CONSTRAINT [FK_mission_applications_missions_mission_id]
GO
ALTER TABLE [dbo].[mission_applications]  WITH CHECK ADD  CONSTRAINT [FK_mission_applications_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[mission_applications] CHECK CONSTRAINT [FK_mission_applications_users_created_by]
GO
ALTER TABLE [dbo].[mission_applications]  WITH CHECK ADD  CONSTRAINT [FK_mission_applications_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[mission_applications] CHECK CONSTRAINT [FK_mission_applications_users_modified_by]
GO
ALTER TABLE [dbo].[mission_applications]  WITH CHECK ADD  CONSTRAINT [FK_mission_applications_volunteers_volunteer_id] FOREIGN KEY([volunteer_id])
REFERENCES [dbo].[volunteers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_applications] CHECK CONSTRAINT [FK_mission_applications_volunteers_volunteer_id]
GO
ALTER TABLE [dbo].[mission_comments]  WITH CHECK ADD  CONSTRAINT [FK_mission_comments_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_comments] CHECK CONSTRAINT [FK_mission_comments_missions_mission_id]
GO
ALTER TABLE [dbo].[mission_comments]  WITH CHECK ADD  CONSTRAINT [FK_mission_comments_volunteers_volunteer_id] FOREIGN KEY([volunteer_id])
REFERENCES [dbo].[volunteers] ([id])
GO
ALTER TABLE [dbo].[mission_comments] CHECK CONSTRAINT [FK_mission_comments_volunteers_volunteer_id]
GO
ALTER TABLE [dbo].[mission_media]  WITH CHECK ADD  CONSTRAINT [FK_mission_media_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_media] CHECK CONSTRAINT [FK_mission_media_missions_mission_id]
GO
ALTER TABLE [dbo].[mission_ratings]  WITH CHECK ADD  CONSTRAINT [FK_mission_ratings_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_ratings] CHECK CONSTRAINT [FK_mission_ratings_missions_mission_id]
GO
ALTER TABLE [dbo].[mission_ratings]  WITH CHECK ADD  CONSTRAINT [FK_mission_ratings_volunteers_volunteer_id] FOREIGN KEY([volunteer_id])
REFERENCES [dbo].[volunteers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_ratings] CHECK CONSTRAINT [FK_mission_ratings_volunteers_volunteer_id]
GO
ALTER TABLE [dbo].[mission_skills]  WITH CHECK ADD  CONSTRAINT [FK_mission_skills_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_skills] CHECK CONSTRAINT [FK_mission_skills_missions_mission_id]
GO
ALTER TABLE [dbo].[mission_skills]  WITH CHECK ADD  CONSTRAINT [FK_mission_skills_skills_skill_id] FOREIGN KEY([skill_id])
REFERENCES [dbo].[skills] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[mission_skills] CHECK CONSTRAINT [FK_mission_skills_skills_skill_id]
GO
ALTER TABLE [dbo].[mission_themes]  WITH CHECK ADD  CONSTRAINT [FK_mission_themes_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[mission_themes] CHECK CONSTRAINT [FK_mission_themes_users_created_by]
GO
ALTER TABLE [dbo].[mission_themes]  WITH CHECK ADD  CONSTRAINT [FK_mission_themes_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[mission_themes] CHECK CONSTRAINT [FK_mission_themes_users_modified_by]
GO
ALTER TABLE [dbo].[missions]  WITH CHECK ADD  CONSTRAINT [FK_missions_cities_city_id] FOREIGN KEY([city_id])
REFERENCES [dbo].[cities] ([id])
GO
ALTER TABLE [dbo].[missions] CHECK CONSTRAINT [FK_missions_cities_city_id]
GO
ALTER TABLE [dbo].[missions]  WITH CHECK ADD  CONSTRAINT [FK_missions_mission_themes_mission_theme_id] FOREIGN KEY([mission_theme_id])
REFERENCES [dbo].[mission_themes] ([id])
GO
ALTER TABLE [dbo].[missions] CHECK CONSTRAINT [FK_missions_mission_themes_mission_theme_id]
GO
ALTER TABLE [dbo].[missions]  WITH CHECK ADD  CONSTRAINT [FK_missions_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[missions] CHECK CONSTRAINT [FK_missions_users_created_by]
GO
ALTER TABLE [dbo].[missions]  WITH CHECK ADD  CONSTRAINT [FK_missions_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[missions] CHECK CONSTRAINT [FK_missions_users_modified_by]
GO
ALTER TABLE [dbo].[skills]  WITH CHECK ADD  CONSTRAINT [FK_skills_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[skills] CHECK CONSTRAINT [FK_skills_users_created_by]
GO
ALTER TABLE [dbo].[skills]  WITH CHECK ADD  CONSTRAINT [FK_skills_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[skills] CHECK CONSTRAINT [FK_skills_users_modified_by]
GO
ALTER TABLE [dbo].[stories]  WITH CHECK ADD  CONSTRAINT [FK_stories_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
GO
ALTER TABLE [dbo].[stories] CHECK CONSTRAINT [FK_stories_missions_mission_id]
GO
ALTER TABLE [dbo].[stories]  WITH CHECK ADD  CONSTRAINT [FK_stories_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[stories] CHECK CONSTRAINT [FK_stories_users_created_by]
GO
ALTER TABLE [dbo].[stories]  WITH CHECK ADD  CONSTRAINT [FK_stories_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[stories] CHECK CONSTRAINT [FK_stories_users_modified_by]
GO
ALTER TABLE [dbo].[stories]  WITH CHECK ADD  CONSTRAINT [FK_stories_volunteers_volunteer_id] FOREIGN KEY([volunteer_id])
REFERENCES [dbo].[volunteers] ([id])
GO
ALTER TABLE [dbo].[stories] CHECK CONSTRAINT [FK_stories_volunteers_volunteer_id]
GO
ALTER TABLE [dbo].[story_media]  WITH CHECK ADD  CONSTRAINT [FK_story_media_stories_story_id] FOREIGN KEY([story_id])
REFERENCES [dbo].[stories] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[story_media] CHECK CONSTRAINT [FK_story_media_stories_story_id]
GO
ALTER TABLE [dbo].[timesheets]  WITH CHECK ADD  CONSTRAINT [FK_timesheets_missions_mission_id] FOREIGN KEY([mission_id])
REFERENCES [dbo].[missions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[timesheets] CHECK CONSTRAINT [FK_timesheets_missions_mission_id]
GO
ALTER TABLE [dbo].[timesheets]  WITH CHECK ADD  CONSTRAINT [FK_timesheets_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[timesheets] CHECK CONSTRAINT [FK_timesheets_users_created_by]
GO
ALTER TABLE [dbo].[timesheets]  WITH CHECK ADD  CONSTRAINT [FK_timesheets_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[timesheets] CHECK CONSTRAINT [FK_timesheets_users_modified_by]
GO
ALTER TABLE [dbo].[timesheets]  WITH CHECK ADD  CONSTRAINT [FK_timesheets_volunteers_volunteer_id] FOREIGN KEY([volunteer_id])
REFERENCES [dbo].[volunteers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[timesheets] CHECK CONSTRAINT [FK_timesheets_volunteers_volunteer_id]
GO
ALTER TABLE [dbo].[volunteer_skills]  WITH CHECK ADD  CONSTRAINT [FK_volunteer_skills_skills_skill_id] FOREIGN KEY([skill_id])
REFERENCES [dbo].[skills] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[volunteer_skills] CHECK CONSTRAINT [FK_volunteer_skills_skills_skill_id]
GO
ALTER TABLE [dbo].[volunteer_skills]  WITH CHECK ADD  CONSTRAINT [FK_volunteer_skills_volunteers_volunteer_id] FOREIGN KEY([volunteer_id])
REFERENCES [dbo].[volunteers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[volunteer_skills] CHECK CONSTRAINT [FK_volunteer_skills_volunteers_volunteer_id]
GO
ALTER TABLE [dbo].[volunteers]  WITH CHECK ADD  CONSTRAINT [FK_volunteers_cities_city_id] FOREIGN KEY([city_id])
REFERENCES [dbo].[cities] ([id])
GO
ALTER TABLE [dbo].[volunteers] CHECK CONSTRAINT [FK_volunteers_cities_city_id]
GO
ALTER TABLE [dbo].[volunteers]  WITH CHECK ADD  CONSTRAINT [FK_volunteers_users_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[volunteers] CHECK CONSTRAINT [FK_volunteers_users_created_by]
GO
ALTER TABLE [dbo].[volunteers]  WITH CHECK ADD  CONSTRAINT [FK_volunteers_users_modified_by] FOREIGN KEY([modified_by])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[volunteers] CHECK CONSTRAINT [FK_volunteers_users_modified_by]
GO
ALTER TABLE [dbo].[volunteers]  WITH CHECK ADD  CONSTRAINT [FK_volunteers_users_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[volunteers] CHECK CONSTRAINT [FK_volunteers_users_user_id]
GO
