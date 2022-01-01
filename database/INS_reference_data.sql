-- Polulate Role
SET IDENTITY_INSERT [dbo].[my_role] ON
INSERT INTO [dbo].[my_role]([Id],[ShortName],[Description]) VALUES(1, 'Admin','');
INSERT INTO [dbo].[my_role]([Id],[ShortName],[Description]) VALUES(2, 'User','');
INSERT INTO [dbo].[my_role]([Id],[ShortName],[Description]) VALUES(3, 'Guest','');
SET IDENTITY_INSERT [dbo].[my_role] OFF

-- Polulate Permission
SET IDENTITY_INSERT [dbo].[my_permission] ON
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(1, 'Role-Index','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(2, 'Role-Edit','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(3, 'Role-Create','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(4, 'Role-Delete','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(5, 'Permission-Index','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(6, 'Permission-Edit','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(7, 'Permission-Create','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(8, 'Permission-Delete','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(9, 'User-Index','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(10, 'User-Edit','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(11, 'User-Create','');
INSERT INTO [dbo].[my_permission]([Id],[ShortName],[Description]) VALUES(12, 'User-Delete','');
SET IDENTITY_INSERT [dbo].[my_permission] OFF

-- Populate dummy UserProfile
SET IDENTITY_INSERT [dbo].[my_profile] ON
INSERT INTO [dbo].[my_profile]([Id],[FirstName],[LastName]) VALUES(1, 'Tony','Zhao');
INSERT INTO [dbo].[my_profile]([Id],[FirstName],[LastName],[Gender],[BirthDate],[Mobile],[Address]) VALUES(2, 'Jerry','Zhao',1,'1970-01-01','04019999999','2 Test Ave');
SET IDENTITY_INSERT [dbo].[my_profile] OFF

-- Populate dummy user
SET IDENTITY_INSERT [dbo].[my_user] ON
INSERT INTO [dbo].[my_user]([Id],[UserProfileId],[UserName],[Password],[EmailAddress]) VALUES (1,1,'admin','admin123','');
INSERT INTO [dbo].[my_user]([Id],[UserProfileId],[UserName],[Password],[EmailAddress]) VALUES (2,1,'user','user123','');
INSERT INTO [dbo].[my_user]([Id],[UserProfileId],[UserName],[Password],[EmailAddress]) VALUES (3,1,'guest','guest123','');
INSERT INTO [dbo].[my_user]([Id],[UserProfileId],[UserName],[Password],[EmailAddress]) VALUES (4,1,'dongz','dong123','');
SET IDENTITY_INSERT [dbo].[my_user] OFF

