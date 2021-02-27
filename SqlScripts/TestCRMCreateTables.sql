USE TestCRM
GO

IF OBJECT_ID('Client') IS NOT NULL
	DROP TABLE [Client]
GO

IF OBJECT_ID('User') IS NOT NULL
	DROP TABLE [User]
GO

IF OBJECT_ID('Login') IS NOT NULL
	DROP TABLE [Login]
GO

IF OBJECT_ID('Role') IS NOT NULL
	DROP TABLE [Role]
GO

IF OBJECT_ID('UserStatus') IS NOT NULL
	DROP TABLE [UserStatus]
GO

IF OBJECT_ID('Entitlement') IS NOT NULL
	DROP TABLE [Entitlement]
GO

IF OBJECT_ID('BranchOffice') IS NOT NULL
	DROP TABLE [BranchOffice]
GO

IF OBJECT_ID('AccountStatus') IS NOT NULL
	DROP TABLE [AccountStatus]
GO

DROP FUNCTION IF EXISTS ufn_FetchUserDetails;
DROP FUNCTION IF EXISTS ufn_FetchAllUserDetails;
GO

CREATE TABLE [BranchOffice] (
	[BranchOfficeId] INT IDENTITY(1,1) NOT NULL,
	[BranchOfficeLocation] NVARCHAR(200) NOT NULL,
	CONSTRAINT pk_branch_office PRIMARY KEY([BranchOfficeId]),
	CONSTRAINT uq_branch_office_location UNIQUE([BranchOfficeLocation])
)
GO

CREATE TABLE [AccountStatus] (
	[AccountStatusId] TINYINT IDENTITY(1,1) NOT NULL,
	[AccountStatusDesc] NVARCHAR(50) NOT NULL,
	CONSTRAINT pk_account_status PRIMARY KEY([AccountStatusId]),
	CONSTRAINT uq_account_status UNIQUE([AccountStatusDesc])
)
GO

CREATE TABLE [UserStatus] (
	[UserStatusId] TINYINT IDENTITY(1,1) NOT NULL,
	[UserStatusDesc] NVARCHAR(50) NOT NULL,
	CONSTRAINT pk_user_status PRIMARY KEY([UserStatusId]),
	CONSTRAINT uq_user_status UNIQUE([UserStatusDesc])
)
GO

CREATE TABLE [Entitlement] (
	[EntitlementId] TINYINT IDENTITY(1,1) NOT NULL,
	[EntitlementDesc] NVARCHAR(200) NOT NULL,
	CONSTRAINT pk_entitlement PRIMARY KEY([EntitlementId]),
	CONSTRAINT uq_entitlement UNIQUE([EntitlementDesc])
)
GO

CREATE TABLE [Role] (
	[RoleId] TINYINT IDENTITY(1,1) NOT NULL,
	[RoleDesc] NVARCHAR(50) NOT NULL,
	CONSTRAINT pk_role PRIMARY KEY ([RoleId]),
	CONSTRAINT uq_role UNIQUE([RoleDesc])
)
GO

CREATE TABLE [Login] (
	[LoginId] INT IDENTITY(1,1) NOT NULL,
	[Username] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(200) NOT NULL,
	[RoleId] TINYINT NOT NULL,
	CONSTRAINT pk_login PRIMARY KEY ([LoginId]),
	CONSTRAINT uq_username UNIQUE ([Username]),
	CONSTRAINT fk_login_role_id FOREIGN KEY ([RoleId]) REFERENCES [Role](RoleId)
)
GO

CREATE TABLE [User] (
	[UserId] INT IDENTITY(1,1) NOT NULL,
	[FirstName] NVARCHAR(200) NOT NULL,
	[LastName] NVARCHAR(200) NOT NULL,
	[BranchOfficeId] SMALLINT NOT NULL,
	[DateOfBirth] DATE NOT NULL,
	[PhoneNumber] NCHAR(10) NOT NULL,
	[StatusId] TINYINT NOT NULL,
	[EntitlementId] TINYINT NOT NULL,
	[LoginId] INT,
	CONSTRAINT pk_user PRIMARY KEY ([UserId]),
	CONSTRAINT uq_user_and_number UNIQUE([FirstName], [LastName], [PhoneNumber]),
	CONSTRAINT fk_user_login_id FOREIGN KEY([LoginId]) REFERENCES [Login](LoginId)
)
GO

CREATE TABLE [Client] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ClientId] AS 'C' + CAST([Id] AS nvarchar(10)),
	[FirstName] NVARCHAR(200) NOT NULL,
	[MiddleName] NVARCHAR(200),
	[LastName] NVARCHAR(200) NOT NULL,
	[DateOfBirth] DATE NOT NULL,
	[AssetValue] MONEY NOT NULL,
	[HomePhoneNumber] NCHAR(10) NOT NULL,
	[OfficePhoneNumber] NCHAR(10),
	[Email] NVARCHAR(200),
	[DriversLicenseIdNum] NVARCHAR(20),
	[UserId] INT NOT NULL,
	[AccountStatusId] TINYINT NOT NULL,
	CONSTRAINT pk_client_id PRIMARY KEY([Id]),
	CONSTRAINT uq_client_id UNIQUE([ClientId]),
	CONSTRAINT fk_client_account_status FOREIGN KEY([AccountStatusId]) REFERENCES [AccountStatus](AccountStatusId),
	CONSTRAINT fk_client_trust_officer FOREIGN KEY([UserId]) REFERENCES [User]([UserId])
)
GO

CREATE FUNCTION ufn_FetchUserDetails (@UserId INT)
RETURNS TABLE
AS
	RETURN (SELECT u.UserId, u.FirstName, u.LastName, b.BranchOfficeLocation, u.DateOfBirth, u.PhoneNumber, s.UserStatusDesc, e.EntitlementDesc
	FROM [User] u INNER JOIN [BranchOffice] b ON u.BranchOfficeId = b.BranchOfficeId
	INNER JOIN  [UserStatus] s ON u.StatusId = s.UserStatusId
	INNER JOIN [Entitlement] e ON u.EntitlementId = e.EntitlementId
	WHERE u.UserId = @UserId)
GO

CREATE FUNCTION ufn_FetchAllUserDetails ()
RETURNS TABLE
AS
	RETURN (SELECT u.UserId, u.FirstName, u.LastName, b.BranchOfficeLocation, u.DateOfBirth, u.PhoneNumber, s.UserStatusDesc, e.EntitlementDesc
	FROM [User] u Left JOIN [BranchOffice] b ON u.BranchOfficeId = b.BranchOfficeId
	Left JOIN  [UserStatus] s ON u.StatusId = s.UserStatusId
	Left JOIN [Entitlement] e ON u.EntitlementId = e.EntitlementId)
GO
