USE [master]
GO
/****** Object:  Database [cota]    Script Date: 8/9/2016 9:49:21 AM ******/
CREATE DATABASE [cota]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cota_1', FILENAME = N'E:\Setup\MSSQL\MSSQL13.SQLEXPRESS\MSSQL\DATA\cota.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'cota_1_log', FILENAME = N'E:\Setup\MSSQL\MSSQL13.SQLEXPRESS\MSSQL\DATA\cota_0.ldf' , SIZE = 22144KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [cota] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cota].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cota] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [cota] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [cota] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [cota] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [cota] SET ARITHABORT OFF 
GO
ALTER DATABASE [cota] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [cota] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [cota] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [cota] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [cota] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [cota] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [cota] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [cota] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [cota] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [cota] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [cota] SET  DISABLE_BROKER 
GO
ALTER DATABASE [cota] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [cota] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [cota] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [cota] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [cota] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [cota] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [cota] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [cota] SET RECOVERY FULL 
GO
ALTER DATABASE [cota] SET  MULTI_USER 
GO
ALTER DATABASE [cota] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [cota] SET DB_CHAINING OFF 
GO
ALTER DATABASE [cota] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [cota] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [cota]
GO
/****** Object:  Table [dbo].[SYS_Log]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_Log](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[userID] [bigint] NOT NULL,
	[logConten] [nvarchar](500) NOT NULL,
	[createdDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SYS_Resources]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_Resources](
	[ID] [bigint] IDENTITY(100,1) NOT NULL,
	[resourceTypeID] [bigint] NOT NULL,
	[parentID] [bigint] NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SYS_ResourceType]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_ResourceType](
	[ID] [bigint] NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_ResourceType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SYS_Roles]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_Roles](
	[ID] [bigint] NOT NULL,
	[name] [nvarchar](50) NULL,
	[resourceTypeID] [bigint] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SYS_UserGroup]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_UserGroup](
	[ID] [bigint] NOT NULL,
	[groupName] [nvarchar](250) NULL,
	[familyList] [varchar](250) NULL,
	[note] [nvarchar](500) NULL,
 CONSTRAINT [PK_SYS_UserGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_UserRoleResource]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_UserRoleResource](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[userID] [bigint] NOT NULL,
	[roleID] [bigint] NOT NULL,
	[resourceID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserRoleResource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SYS_Users]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Users](
	[ID] [bigint] NOT NULL,
	[firstName] [nvarchar](50) NULL,
	[lastName] [nvarchar](50) NULL,
	[birthday] [date] NULL,
	[gender] [bit] NULL,
	[address] [nvarchar](300) NULL,
	[phoneNumber] [varchar](12) NULL,
	[money] [bigint] NOT NULL,
	[note] [ntext] NULL,
	[account] [varchar](50) NOT NULL,
	[password] [varchar](32) NOT NULL,
	[status] [smallint] NOT NULL,
 CONSTRAINT [PK_SYS_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[SYS_UserRoleResourceDetail]    Script Date: 8/9/2016 9:49:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SYS_UserRoleResourceDetail]
AS
SELECT     dbo.SYS_UserRoleResource.ID, dbo.SYS_UserRoleResource.userID, dbo.SYS_UserRoleResource.roleID, dbo.SYS_UserRoleResource.resourceID, 
                      dbo.SYS_Users.account AS user_account, dbo.SYS_UserGroup.groupName AS user_groupName, dbo.SYS_Roles.resourceTypeID AS role_resourceTypeID, 
                      dbo.SYS_Roles.name AS role_name, dbo.SYS_Resources.resourceTypeID AS resource_resourceTypeID
FROM         dbo.SYS_Roles INNER JOIN
                      dbo.SYS_UserRoleResource ON dbo.SYS_Roles.ID = dbo.SYS_UserRoleResource.roleID INNER JOIN
                      dbo.SYS_Resources ON dbo.SYS_UserRoleResource.resourceID = dbo.SYS_Resources.ID LEFT OUTER JOIN
                      dbo.SYS_Users ON dbo.SYS_UserRoleResource.userID = dbo.SYS_Users.ID LEFT OUTER JOIN
                      dbo.SYS_UserGroup ON dbo.SYS_UserRoleResource.userID = dbo.SYS_UserGroup.ID


GO

SET IDENTITY_INSERT [dbo].[SYS_Resources] ON 

INSERT [dbo].[SYS_Resources] ([ID], [resourceTypeID], [parentID]) VALUES (120, 1, 235)
INSERT [dbo].[SYS_Resources] ([ID], [resourceTypeID], [parentID]) VALUES (155, 2, NULL)
INSERT [dbo].[SYS_Resources] ([ID], [resourceTypeID], [parentID]) VALUES (235, 2, 155)
INSERT [dbo].[SYS_Resources] ([ID], [resourceTypeID], [parentID]) VALUES (236, 2, 155)
INSERT [dbo].[SYS_Resources] ([ID], [resourceTypeID], [parentID]) VALUES (243, 1, 236)
SET IDENTITY_INSERT [dbo].[SYS_Resources] OFF
INSERT [dbo].[SYS_ResourceType] ([ID], [name]) VALUES (1, N'Người Dùng')
INSERT [dbo].[SYS_ResourceType] ([ID], [name]) VALUES (2, N'Nhóm Người Dùng')
INSERT [dbo].[SYS_Roles] ([ID], [name], [resourceTypeID]) VALUES (1, N'Thêm người dùng', 1)
INSERT [dbo].[SYS_Roles] ([ID], [name], [resourceTypeID]) VALUES (2, N'Sửa người dùng', 1)
INSERT [dbo].[SYS_Roles] ([ID], [name], [resourceTypeID]) VALUES (3, N'Xóa người dùng', 1)
INSERT [dbo].[SYS_Roles] ([ID], [name], [resourceTypeID]) VALUES (4, N'Xem người dùng', 1)
INSERT [dbo].[SYS_UserGroup] ([ID], [groupName], [familyList], [note]) VALUES (155, N'ALL_USER', N',155,', NULL)
INSERT [dbo].[SYS_UserGroup] ([ID], [groupName], [familyList], [note]) VALUES (235, N'Admin', N',235,155,', N'Hệ thống')
INSERT [dbo].[SYS_UserGroup] ([ID], [groupName], [familyList], [note]) VALUES (236, N'User', N',236,155,', N'Hệ thống')
SET IDENTITY_INSERT [dbo].[SYS_UserRoleResource] ON 

INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (7, 235, 1, 235)
INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (8, 235, 2, 235)
INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (9, 235, 3, 235)
INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (10, 235, 4, 235)
INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (47, 235, 1, 236)
INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (48, 235, 2, 236)
INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (49, 235, 3, 236)
INSERT [dbo].[SYS_UserRoleResource] ([ID], [userID], [roleID], [resourceID]) VALUES (50, 235, 4, 236)
SET IDENTITY_INSERT [dbo].[SYS_UserRoleResource] OFF
INSERT [dbo].[SYS_Users] ([ID], [firstName], [lastName], [birthday], [gender], [address], [phoneNumber], [money], [note], [account], [password], [status]) VALUES (120, N'Quản trị', N'cao nhất', CAST(0x18130B00 AS Date), 1, N'96 Định Công', N'213123', 231232, N'admin@mailinator.com', N'ADMIN', N'CA45E0715BBED86F4F4E7A3B45777345', 1)
INSERT [dbo].[SYS_Users] ([ID], [firstName], [lastName], [birthday], [gender], [address], [phoneNumber], [money], [note], [account], [password], [status]) VALUES (243, N'Trần', N'Hoài Nam', CAST(0x18130B00 AS Date), 1, N'Thanh Trì - Hà Nội', N'0984203106', 12570124, N'hoainamhotline@gmail.com', N'HOAINAM', N'CA45E0715BBED86F4F4E7A3B45777345', 1)
ALTER TABLE [dbo].[SYS_Users] ADD  CONSTRAINT [DF_SYS_Users_money]  DEFAULT ((0)) FOR [money]
GO
ALTER TABLE [dbo].[SYS_Users] ADD  CONSTRAINT [DF_SYS_Users_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[SYS_Log]  WITH CHECK ADD  CONSTRAINT [FK_SYS_Log_SYS_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[SYS_Users] ([ID])
GO
ALTER TABLE [dbo].[SYS_Log] CHECK CONSTRAINT [FK_SYS_Log_SYS_Users]
GO
ALTER TABLE [dbo].[SYS_Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_ResourceType] FOREIGN KEY([resourceTypeID])
REFERENCES [dbo].[SYS_ResourceType] ([ID])
GO
ALTER TABLE [dbo].[SYS_Resources] CHECK CONSTRAINT [FK_Resources_ResourceType]
GO
ALTER TABLE [dbo].[SYS_Resources]  WITH CHECK ADD  CONSTRAINT [FK_SYS_Resources_SYS_Resources] FOREIGN KEY([parentID])
REFERENCES [dbo].[SYS_Resources] ([ID])
GO
ALTER TABLE [dbo].[SYS_Resources] CHECK CONSTRAINT [FK_SYS_Resources_SYS_Resources]
GO
ALTER TABLE [dbo].[SYS_Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_ResourceType] FOREIGN KEY([resourceTypeID])
REFERENCES [dbo].[SYS_ResourceType] ([ID])
GO
ALTER TABLE [dbo].[SYS_Roles] CHECK CONSTRAINT [FK_Roles_ResourceType]
GO
ALTER TABLE [dbo].[SYS_UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_SYS_UserGroup_SYS_Resources] FOREIGN KEY([ID])
REFERENCES [dbo].[SYS_Resources] ([ID])
GO
ALTER TABLE [dbo].[SYS_UserGroup] CHECK CONSTRAINT [FK_SYS_UserGroup_SYS_Resources]
GO
ALTER TABLE [dbo].[SYS_UserRoleResource]  WITH CHECK ADD  CONSTRAINT [FK_SYS_UserRoleResource_SYS_Resources] FOREIGN KEY([resourceID])
REFERENCES [dbo].[SYS_Resources] ([ID])
GO
ALTER TABLE [dbo].[SYS_UserRoleResource] CHECK CONSTRAINT [FK_SYS_UserRoleResource_SYS_Resources]
GO
ALTER TABLE [dbo].[SYS_UserRoleResource]  WITH CHECK ADD  CONSTRAINT [FK_SYS_UserRoleResource_SYS_Resources1] FOREIGN KEY([userID])
REFERENCES [dbo].[SYS_Resources] ([ID])
GO
ALTER TABLE [dbo].[SYS_UserRoleResource] CHECK CONSTRAINT [FK_SYS_UserRoleResource_SYS_Resources1]
GO
ALTER TABLE [dbo].[SYS_UserRoleResource]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleResource_Roles] FOREIGN KEY([roleID])
REFERENCES [dbo].[SYS_Roles] ([ID])
GO
ALTER TABLE [dbo].[SYS_UserRoleResource] CHECK CONSTRAINT [FK_UserRoleResource_Roles]
GO
ALTER TABLE [dbo].[SYS_Users]  WITH CHECK ADD  CONSTRAINT [FK_SYS_Users_SYS_Resources] FOREIGN KEY([ID])
REFERENCES [dbo].[SYS_Resources] ([ID])
GO
ALTER TABLE [dbo].[SYS_Users] CHECK CONSTRAINT [FK_SYS_Users_SYS_Resources]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[21] 4[41] 2[26] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "SYS_Roles"
            Begin Extent = 
               Top = 171
               Left = 180
               Bottom = 288
               Right = 355
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SYS_UserRoleResource"
            Begin Extent = 
               Top = 45
               Left = 727
               Bottom = 164
               Right = 887
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SYS_Resources"
            Begin Extent = 
               Top = 202
               Left = 531
               Bottom = 306
               Right = 697
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SYS_Users"
            Begin Extent = 
               Top = 0
               Left = 75
               Bottom = 119
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "SYS_UserGroup"
            Begin Extent = 
               Top = 62
               Left = 307
               Bottom = 166
               Right = 467
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2220
         Alias = 3285
         Table = 2670
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SYS_UserRoleResourceDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'      Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SYS_UserRoleResourceDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SYS_UserRoleResourceDetail'
GO
USE [master]
GO
ALTER DATABASE [cota] SET  READ_WRITE 
GO
