
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/04/2026 16:58:56
-- Generated from EDMX file: C:\4_sem\OOP\lab9\ModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FlowerShopLab8];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Manufacturers'
CREATE TABLE [dbo].[Manufacturers] (
    [ManufacturerId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [CategoryCategoryId] int  NOT NULL,
    [ManufacturerManufacturerId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [ManufacturerId] in table 'Manufacturers'
ALTER TABLE [dbo].[Manufacturers]
ADD CONSTRAINT [PK_Manufacturers]
    PRIMARY KEY CLUSTERED ([ManufacturerId] ASC);
GO

-- Creating primary key on [ProductId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryCategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_CategoryProducts]
    FOREIGN KEY ([CategoryCategoryId])
    REFERENCES [dbo].[Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProducts'
CREATE INDEX [IX_FK_CategoryProducts]
ON [dbo].[Products]
    ([CategoryCategoryId]);
GO

-- Creating foreign key on [ManufacturerManufacturerId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ManufacturerProduct]
    FOREIGN KEY ([ManufacturerManufacturerId])
    REFERENCES [dbo].[Manufacturers]
        ([ManufacturerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManufacturerProduct'
CREATE INDEX [IX_FK_ManufacturerProduct]
ON [dbo].[Products]
    ([ManufacturerManufacturerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------