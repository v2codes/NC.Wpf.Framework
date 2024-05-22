/*
 Navicat Premium Data Transfer

 Source Server         : TDK-localhost
 Source Server Type    : SQL Server
 Source Server Version : 16004115 (16.00.4115)
 Source Host           : localhost:1433
 Source Catalog        : NCWpfSample
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16004115 (16.00.4115)
 File Encoding         : 65001

 Date: 22/05/2024 14:45:47
*/


-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type IN ('U'))
	DROP TABLE [dbo].[__EFMigrationsHistory]
GO

CREATE TABLE [dbo].[__EFMigrationsHistory] (
  [MigrationId] nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [ProductVersion] nvarchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[__EFMigrationsHistory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240517063353_Initial', N'8.0.0')
GO


-- ----------------------------
-- Table structure for TSamples
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[TSamples]') AND type IN ('U'))
	DROP TABLE [dbo].[TSamples]
GO

CREATE TABLE [dbo].[TSamples] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Type] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [ExtraProperties] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT CONVERT([bit],(0)) NOT NULL
)
GO

ALTER TABLE [dbo].[TSamples] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of TSamples
-- ----------------------------
INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'59E4EBFC-DDE1-45CE-817E-0E2C343900D6', N'Sample-10', N'Information', N'{}', N'e3c2014f295342bba7b74339aaddb77f', N'2024-05-17 15:24:06.5316637', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'5AB560D5-0ACE-43D2-85BD-275E3FABB8E3', N'Sample-9', N'Information', N'{}', N'020ba3eef9684638931ba37a3ec9d1bd', N'2024-05-17 15:24:03.0625350', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'5F9AFE83-3658-4854-A063-39D9BC9AC7B1', N'Sample-11', N'Warning', N'{}', N'8eeb9d2227cc4acb97533b7c3d32b535', N'2024-05-17 15:24:48.3772652', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'1B25034B-BA89-4C28-879A-3B9954B5E782', N'Sample-5', N'Information', N'{}', N'fb63b1f5049e48769b695bd21f3ed39a', N'2024-05-17 15:23:52.2705586', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'6C3D273D-BA59-4FA8-8070-49AE9BFD58AF', N'Sample-2', N'Information', N'{}', N'33a382d12b2c441896bc3f5e41b8a8a0', N'2024-05-17 15:23:43.0125728', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'18CE4571-A2E8-4A38-A3BB-9D6321CA3CAD', N'Sample-8', N'Information', N'{}', N'd402df18ff724868b3f1cbb38873edeb', N'2024-05-17 15:24:00.5583173', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'F68611AA-1DBB-4E41-B2AA-A394C93227E8', N'Sample-13', N'Error', N'{}', N'26d2a173664844d09a107de56647bc23', N'2024-05-17 15:24:11.4052099', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'ED793A36-5F99-4D07-8C65-AF54119CF74E', N'Sample-7', N'Information', N'{}', N'af034706cf524508886b431425fe5cc4', N'2024-05-17 15:23:58.1870362', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'07833F9F-6F54-4833-93DA-C54B6532A4DA', N'Sample-4', N'Information', N'{}', N'9b926be49f25486ebce8ac937a7b547b', N'2024-05-17 15:23:49.4606237', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'A8681526-3D0D-49C0-8343-C8BD2B425565', N'Sample-1', N'Information', N'{}', N'cdb6c84a54b1440e8b56fa1cf0e168b7', N'2024-05-17 15:23:26.1171112', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'7BC13AD1-C4D4-477C-A6D1-D3A60B3044B8', N'Sample-6', N'Information', N'{}', N'79f106cc6b114f73b7792677a6b39a32', N'2024-05-17 15:23:55.7577000', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'672B8596-3879-41A0-97B9-EA891CCA4BE6', N'Sample-3', N'Information', N'{}', N'd1643f7ccf6e4969a409523d6f3d9afd', N'2024-05-17 15:23:45.4688407', NULL, NULL, NULL, N'0')
GO

INSERT INTO [dbo].[TSamples] ([Id], [Name], [Type], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted]) VALUES (N'E4A2B257-893D-407E-96C4-F176DC007C4A', N'Sample-12', N'Error', N'{}', N'ca99b4f08d2e4b76a2e70a7cd9953e53', N'2024-05-17 15:24:55.2549779', NULL, NULL, NULL, N'0')
GO


-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table TSamples
-- ----------------------------
ALTER TABLE [dbo].[TSamples] ADD CONSTRAINT [PK_TSamples] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

