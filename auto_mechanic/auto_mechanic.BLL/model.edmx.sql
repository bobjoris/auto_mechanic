
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/20/2014 09:20:44
-- Generated from EDMX file: C:\Users\Joris GIRARDOT\Documents\auto_mechanic\auto_mechanic\auto_mechanic.BLL\model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Automechanics];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FranchiseMechanic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mechanic] DROP CONSTRAINT [FK_FranchiseMechanic];
GO
IF OBJECT_ID(N'[dbo].[FK_MechanicMechanic_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mechanic_Service] DROP CONSTRAINT [FK_MechanicMechanic_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceMechanic_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mechanic_Service] DROP CONSTRAINT [FK_ServiceMechanic_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_BrandCar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Car] DROP CONSTRAINT [FK_BrandCar];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceBookCar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Car] DROP CONSTRAINT [FK_ServiceBookCar];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceBookService_ServiceBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceBookService] DROP CONSTRAINT [FK_ServiceBookService_ServiceBook];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceBookService_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceBookService] DROP CONSTRAINT [FK_ServiceBookService_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_MechanicHoliday]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Holiday] DROP CONSTRAINT [FK_MechanicHoliday];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Mechanic]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mechanic];
GO
IF OBJECT_ID(N'[dbo].[Franchise]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Franchise];
GO
IF OBJECT_ID(N'[dbo].[Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Service];
GO
IF OBJECT_ID(N'[dbo].[Mechanic_Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mechanic_Service];
GO
IF OBJECT_ID(N'[dbo].[Car]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Car];
GO
IF OBJECT_ID(N'[dbo].[Brand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Brand];
GO
IF OBJECT_ID(N'[dbo].[ServiceBook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceBook];
GO
IF OBJECT_ID(N'[dbo].[Holiday]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Holiday];
GO
IF OBJECT_ID(N'[dbo].[ServiceBookService]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceBookService];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Mechanic'
CREATE TABLE [dbo].[Mechanic] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [FranchiseID] int  NOT NULL
);
GO

-- Creating table 'Franchise'
CREATE TABLE [dbo].[Franchise] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Service'
CREATE TABLE [dbo].[Service] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Label] nvarchar(max)  NOT NULL,
    [KM] int  NOT NULL
);
GO

-- Creating table 'Mechanic_Service'
CREATE TABLE [dbo].[Mechanic_Service] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MechanicID] int  NOT NULL,
    [ServiceID] int  NOT NULL,
    [Duration] int  NOT NULL
);
GO

-- Creating table 'Car'
CREATE TABLE [dbo].[Car] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [BrandID] int  NOT NULL,
    [ServiceBookID] int  NOT NULL
);
GO

-- Creating table 'Brand'
CREATE TABLE [dbo].[Brand] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ServiceBook'
CREATE TABLE [dbo].[ServiceBook] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Holiday'
CREATE TABLE [dbo].[Holiday] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MechanicID] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL
);
GO

-- Creating table 'SimJeu'
CREATE TABLE [dbo].[SimJeu] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DateBegin] datetime  NOT NULL,
    [Duration] nvarchar(max)  NOT NULL,
    [Param] nvarchar(max)  NOT NULL,
    [Init] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SimIterJeu'
CREATE TABLE [dbo].[SimIterJeu] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SimID] int  NOT NULL,
    [Repair] nvarchar(max)  NOT NULL,
    [Drive] nvarchar(max)  NOT NULL,
    [Planning] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ServiceBookService'
CREATE TABLE [dbo].[ServiceBookService] (
    [ServiceBookService_Service_ID] int  NOT NULL,
    [Service_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Mechanic'
ALTER TABLE [dbo].[Mechanic]
ADD CONSTRAINT [PK_Mechanic]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Franchise'
ALTER TABLE [dbo].[Franchise]
ADD CONSTRAINT [PK_Franchise]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [PK_Service]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Mechanic_Service'
ALTER TABLE [dbo].[Mechanic_Service]
ADD CONSTRAINT [PK_Mechanic_Service]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Car'
ALTER TABLE [dbo].[Car]
ADD CONSTRAINT [PK_Car]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Brand'
ALTER TABLE [dbo].[Brand]
ADD CONSTRAINT [PK_Brand]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ServiceBook'
ALTER TABLE [dbo].[ServiceBook]
ADD CONSTRAINT [PK_ServiceBook]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Holiday'
ALTER TABLE [dbo].[Holiday]
ADD CONSTRAINT [PK_Holiday]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SimJeu'
ALTER TABLE [dbo].[SimJeu]
ADD CONSTRAINT [PK_SimJeu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SimIterJeu'
ALTER TABLE [dbo].[SimIterJeu]
ADD CONSTRAINT [PK_SimIterJeu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ServiceBookService_Service_ID], [Service_ID] in table 'ServiceBookService'
ALTER TABLE [dbo].[ServiceBookService]
ADD CONSTRAINT [PK_ServiceBookService]
    PRIMARY KEY NONCLUSTERED ([ServiceBookService_Service_ID], [Service_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FranchiseID] in table 'Mechanic'
ALTER TABLE [dbo].[Mechanic]
ADD CONSTRAINT [FK_FranchiseMechanic]
    FOREIGN KEY ([FranchiseID])
    REFERENCES [dbo].[Franchise]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FranchiseMechanic'
CREATE INDEX [IX_FK_FranchiseMechanic]
ON [dbo].[Mechanic]
    ([FranchiseID]);
GO

-- Creating foreign key on [MechanicID] in table 'Mechanic_Service'
ALTER TABLE [dbo].[Mechanic_Service]
ADD CONSTRAINT [FK_MechanicMechanic_Service]
    FOREIGN KEY ([MechanicID])
    REFERENCES [dbo].[Mechanic]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MechanicMechanic_Service'
CREATE INDEX [IX_FK_MechanicMechanic_Service]
ON [dbo].[Mechanic_Service]
    ([MechanicID]);
GO

-- Creating foreign key on [ServiceID] in table 'Mechanic_Service'
ALTER TABLE [dbo].[Mechanic_Service]
ADD CONSTRAINT [FK_ServiceMechanic_Service]
    FOREIGN KEY ([ServiceID])
    REFERENCES [dbo].[Service]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceMechanic_Service'
CREATE INDEX [IX_FK_ServiceMechanic_Service]
ON [dbo].[Mechanic_Service]
    ([ServiceID]);
GO

-- Creating foreign key on [BrandID] in table 'Car'
ALTER TABLE [dbo].[Car]
ADD CONSTRAINT [FK_BrandCar]
    FOREIGN KEY ([BrandID])
    REFERENCES [dbo].[Brand]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandCar'
CREATE INDEX [IX_FK_BrandCar]
ON [dbo].[Car]
    ([BrandID]);
GO

-- Creating foreign key on [ServiceBookID] in table 'Car'
ALTER TABLE [dbo].[Car]
ADD CONSTRAINT [FK_ServiceBookCar]
    FOREIGN KEY ([ServiceBookID])
    REFERENCES [dbo].[ServiceBook]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceBookCar'
CREATE INDEX [IX_FK_ServiceBookCar]
ON [dbo].[Car]
    ([ServiceBookID]);
GO

-- Creating foreign key on [ServiceBookService_Service_ID] in table 'ServiceBookService'
ALTER TABLE [dbo].[ServiceBookService]
ADD CONSTRAINT [FK_ServiceBookService_ServiceBook]
    FOREIGN KEY ([ServiceBookService_Service_ID])
    REFERENCES [dbo].[ServiceBook]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Service_ID] in table 'ServiceBookService'
ALTER TABLE [dbo].[ServiceBookService]
ADD CONSTRAINT [FK_ServiceBookService_Service]
    FOREIGN KEY ([Service_ID])
    REFERENCES [dbo].[Service]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceBookService_Service'
CREATE INDEX [IX_FK_ServiceBookService_Service]
ON [dbo].[ServiceBookService]
    ([Service_ID]);
GO

-- Creating foreign key on [MechanicID] in table 'Holiday'
ALTER TABLE [dbo].[Holiday]
ADD CONSTRAINT [FK_MechanicHoliday]
    FOREIGN KEY ([MechanicID])
    REFERENCES [dbo].[Mechanic]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MechanicHoliday'
CREATE INDEX [IX_FK_MechanicHoliday]
ON [dbo].[Holiday]
    ([MechanicID]);
GO

-- Creating foreign key on [SimID] in table 'SimIterJeu'
ALTER TABLE [dbo].[SimIterJeu]
ADD CONSTRAINT [FK_SimSimIter]
    FOREIGN KEY ([SimID])
    REFERENCES [dbo].[SimJeu]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SimSimIter'
CREATE INDEX [IX_FK_SimSimIter]
ON [dbo].[SimIterJeu]
    ([SimID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------