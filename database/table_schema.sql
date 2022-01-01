
/****** Object: drop CONSTRAINT ******/
ALTER TABLE [dbo].[my_user] DROP CONSTRAINT [FK_dbo.my_user_dbo.my_profile_UserProfileId];
ALTER TABLE [dbo].[user_role] DROP CONSTRAINT [FK_dbo.user_role_dbo.my_role_role_Id];
ALTER TABLE [dbo].[user_role] DROP CONSTRAINT [FK_dbo.user_role_dbo.my_user_user_Id];
ALTER TABLE [dbo].[role_permission] DROP CONSTRAINT [FK_dbo.role_permission_dbo.my_permission_permission_Id];
ALTER TABLE [dbo].[role_permission] DROP CONSTRAINT [FK_dbo.role_permission_dbo.my_role_role_Id];

/****** Object: drop tables ******/
DROP TABLE [dbo].[my_profile];
DROP TABLE [dbo].[my_user];
DROP TABLE [dbo].[user_role];
DROP TABLE [dbo].[role_permission];
DROP TABLE [dbo].[my_role];
DROP TABLE [dbo].[my_permission];

/****** Object: create tables schema ******/
CREATE TABLE [dbo].[my_profile] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (20) NULL,
    [LastName]  NVARCHAR (20) NULL,
    [Gender]    BIT            NULL,
    [BirthDate] DATETIME       NULL,
    [Mobile]    NVARCHAR (20) NULL,
    [Address]   NVARCHAR (255) NULL,
    [EffectiveDateTime] DATETIME  NOT NULL,
    CONSTRAINT [PK_dbo.my_profile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[my_user] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [UserProfileId] INT            NOT NULL,
    [UserName]      NVARCHAR (20) NOT NULL,
    [Password]      NVARCHAR (20) NULL,
    [EmailAddress]  NVARCHAR (100) NULL,
    CONSTRAINT [PK_dbo.my_user] PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[my_role] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ShortName]   NVARCHAR (20) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.my_role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[my_permission] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ShortName]   NVARCHAR (50) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.my_permission] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[user_role] (
    [role_Id] INT NOT NULL,
    [user_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.user_role] PRIMARY KEY CLUSTERED ([role_Id] ASC, [user_Id] ASC),      
);

CREATE TABLE [dbo].[role_permission] (
    [permission_Id] INT NOT NULL,
    [role_Id]       INT NOT NULL,
    CONSTRAINT [PK_dbo.role_permission] PRIMARY KEY CLUSTERED ([permission_Id] ASC, [role_Id] ASC), 
);

/****** Object: add foreigh key ******/
ALTER TABLE [dbo].[my_user] ADD CONSTRAINT [FK_dbo.my_user_dbo.my_profile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [dbo].[my_profile] ([Id]) ON DELETE CASCADE;
ALTER TABLE [dbo].[user_role] ADD CONSTRAINT [FK_dbo.user_role_dbo.my_role_role_Id] FOREIGN KEY ([role_Id]) REFERENCES [dbo].[my_role] ([Id]) ON DELETE CASCADE;
ALTER TABLE [dbo].[user_role] ADD CONSTRAINT [FK_dbo.user_role_dbo.my_user_user_Id] FOREIGN KEY ([user_Id]) REFERENCES [dbo].[my_user] ([Id]) ON DELETE CASCADE;
ALTER TABLE [dbo].[role_permission] ADD CONSTRAINT [FK_dbo.role_permission_dbo.my_permission_permission_Id] FOREIGN KEY ([permission_Id]) REFERENCES [dbo].[my_permission] ([Id]) ON DELETE CASCADE;
ALTER TABLE [dbo].[role_permission] ADD CONSTRAINT [FK_dbo.role_permission_dbo.my_role_role_Id] FOREIGN KEY ([role_Id]) REFERENCES [dbo].[my_role] ([Id]) ON DELETE CASCADE;

/****** Object: create index ******/
CREATE NONCLUSTERED INDEX [IX_UserProfileId] ON [dbo].[my_user]([UserProfileId] ASC);
CREATE NONCLUSTERED INDEX [IX_role_permission_permission_Id] ON [dbo].[role_permission]([permission_Id] ASC);
CREATE NONCLUSTERED INDEX [IX_role_permission_role_Id] ON [dbo].[role_permission]([role_Id] ASC);
CREATE NONCLUSTERED INDEX [IX_user_role_role_Id] ON [dbo].[user_role]([role_Id] ASC);
CREATE NONCLUSTERED INDEX [IX_user_role_user_Id] ON [dbo].[user_role]([user_Id] ASC);

/****** Object: add unique key ******/
ALTER TABLE [dbo].[my_user] ADD CONSTRAINT [UQ_dbo.my_user.UserName] UNIQUE ([UserName]);
ALTER TABLE [dbo].[my_role] ADD CONSTRAINT [UQ_dbo.my_role.ShortName] UNIQUE ([ShortName]);
ALTER TABLE [dbo].[my_permission] ADD CONSTRAINT [UQ_dbo.my_permission.ShortName] UNIQUE ([ShortName]);