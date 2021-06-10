USE master
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FinancesCore')
BEGIN
  CREATE DATABASE [FinancesCore];
END;
GO

USE FinancesCore

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Categories] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [Title] varchar(200) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Transactions] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [Title] varchar(200) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [Type] int NOT NULL,
    [Active] bit NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Transactions_CategoryId] ON [Transactions] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210525190901_Initial', N'3.1.15');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transactions]') AND [c].[name] = N'Title');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Transactions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Transactions] ALTER COLUMN [Title] varchar(100) NOT NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'Title');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Categories] ALTER COLUMN [Title] varchar(100) NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210603132443_changeTitlezise', N'3.1.15');

GO

