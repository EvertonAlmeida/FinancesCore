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

