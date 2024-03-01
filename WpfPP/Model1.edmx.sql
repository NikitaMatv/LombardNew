
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/01/2024 09:18:45
-- Generated from EDMX file: C:\Users\262038\Desktop\WpfAutomation_Lombard-master\WpfPP\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DatabasePP];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tovar] DROP CONSTRAINT [FK_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dogovor] DROP CONSTRAINT [FK_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_Dogovor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tovar] DROP CONSTRAINT [FK_Dogovor];
GO
IF OBJECT_ID(N'[dbo].[FK_Sotrudnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dogovor] DROP CONSTRAINT [FK_Sotrudnik];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Client];
GO
IF OBJECT_ID(N'[dbo].[Dogovor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dogovor];
GO
IF OBJECT_ID(N'[dbo].[Sotrudnik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sotrudnik];
GO
IF OBJECT_ID(N'[dbo].[Tovar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tovar];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admin'
CREATE TABLE [dbo].[Admin] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [Id_Category] int IDENTITY(1,1) NOT NULL,
    [Name_category] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Client'
CREATE TABLE [dbo].[Client] (
    [id_client] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [paspoport_number] int  NOT NULL,
    [pasport_seria] int  NOT NULL,
    [phone] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Dogovor'
CREATE TABLE [dbo].[Dogovor] (
    [Id_Dogovor] int IDENTITY(1,1) NOT NULL,
    [Id_Client] int  NOT NULL,
    [Id_Sotrudnik] int  NOT NULL,
    [date_dogovor] datetime  NULL,
    [date_srokzaloga] datetime  NULL,
    [procent] nvarchar(50)  NULL
);
GO

-- Creating table 'Sotrudnik'
CREATE TABLE [dbo].[Sotrudnik] (
    [Id_Sotrudnik] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Phone] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Tovar'
CREATE TABLE [dbo].[Tovar] (
    [Id_Tovar] int IDENTITY(1,1) NOT NULL,
    [Id_dogovor] int  NOT NULL,
    [Name_Tovar] nvarchar(50)  NOT NULL,
    [price] nvarchar(50)  NULL,
    [Id_Category] int  NOT NULL,
    [status] nvarchar(50)  NULL,
    [Img] nvarchar(500)  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Admin'
ALTER TABLE [dbo].[Admin]
ADD CONSTRAINT [PK_Admin]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id_Category] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([Id_Category] ASC);
GO

-- Creating primary key on [id_client] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [PK_Client]
    PRIMARY KEY CLUSTERED ([id_client] ASC);
GO

-- Creating primary key on [Id_Dogovor] in table 'Dogovor'
ALTER TABLE [dbo].[Dogovor]
ADD CONSTRAINT [PK_Dogovor]
    PRIMARY KEY CLUSTERED ([Id_Dogovor] ASC);
GO

-- Creating primary key on [Id_Sotrudnik] in table 'Sotrudnik'
ALTER TABLE [dbo].[Sotrudnik]
ADD CONSTRAINT [PK_Sotrudnik]
    PRIMARY KEY CLUSTERED ([Id_Sotrudnik] ASC);
GO

-- Creating primary key on [Id_Tovar] in table 'Tovar'
ALTER TABLE [dbo].[Tovar]
ADD CONSTRAINT [PK_Tovar]
    PRIMARY KEY CLUSTERED ([Id_Tovar] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_Category] in table 'Tovar'
ALTER TABLE [dbo].[Tovar]
ADD CONSTRAINT [FK_Category]
    FOREIGN KEY ([Id_Category])
    REFERENCES [dbo].[Category]
        ([Id_Category])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Category'
CREATE INDEX [IX_FK_Category]
ON [dbo].[Tovar]
    ([Id_Category]);
GO

-- Creating foreign key on [Id_Client] in table 'Dogovor'
ALTER TABLE [dbo].[Dogovor]
ADD CONSTRAINT [FK_Client]
    FOREIGN KEY ([Id_Client])
    REFERENCES [dbo].[Client]
        ([id_client])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Client'
CREATE INDEX [IX_FK_Client]
ON [dbo].[Dogovor]
    ([Id_Client]);
GO

-- Creating foreign key on [Id_dogovor] in table 'Tovar'
ALTER TABLE [dbo].[Tovar]
ADD CONSTRAINT [FK_Dogovor]
    FOREIGN KEY ([Id_dogovor])
    REFERENCES [dbo].[Dogovor]
        ([Id_Dogovor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Dogovor'
CREATE INDEX [IX_FK_Dogovor]
ON [dbo].[Tovar]
    ([Id_dogovor]);
GO

-- Creating foreign key on [Id_Sotrudnik] in table 'Dogovor'
ALTER TABLE [dbo].[Dogovor]
ADD CONSTRAINT [FK_Sotrudnik]
    FOREIGN KEY ([Id_Sotrudnik])
    REFERENCES [dbo].[Sotrudnik]
        ([Id_Sotrudnik])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sotrudnik'
CREATE INDEX [IX_FK_Sotrudnik]
ON [dbo].[Dogovor]
    ([Id_Sotrudnik]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------