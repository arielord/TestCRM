USE TestCRM
GO

INSERT INTO [Role] VALUES('User');
INSERT INTO [Role] VALUES('Admin');

INSERT INTO [Entitlement] VALUES('Trust Officer');
INSERT INTO [Entitlement] VALUES('Customer Relationship Officer');

INSERT INTO [UserStatus] VALUES('Active');
INSERT INTO [UserStatus] VALUES('Inactive');

INSERT INTO [AccountStatus] VALUES('Open');
INSERT INTO [AccountStatus] VALUES('Closed');

INSERT INTO [BranchOffice] VALUES('New York');
INSERT INTO [BranchOffice] VALUES('Chicago');
INSERT INTO [BranchOffice] VALUES('Dallas');
INSERT INTO [BranchOffice] VALUES('San Jose');

INSERT INTO [Login] VALUES('Admin', 'Admin', 2);
INSERT INTO [Login] VALUES('ariel.ordonez','User@1234',1);

INSERT INTO [User] ([FirstName], [LastName], [BranchOfficeId], [DateOfBirth], [PhoneNumber], [StatusId], [EntitlementId]) 
VALUES('Ariel', 'Ordonez', 1, '19940311', '9173738559', 1, 1);
INSERT INTO [User] ([FirstName], [LastName], [BranchOfficeId], [DateOfBirth], [PhoneNumber], [StatusId], [EntitlementId]) 
VALUES('Billy', 'Bob', 2, '19940311', '1234567890', 1, 2);

UPDATE [User] SET [LoginId] = 2 WHERE [UserId] = 1;

INSERT INTO [Client] ([FirstName], [LastName], [DateOfBirth], [AssetValue], [HomePhoneNumber], [UserId], [AccountStatusId]) 
VALUES('Ari', 'Ord', '19940311', 1200.0, '9173738559', 1, 1);

SELECT * FROM [Role]
SELECT * FROM [Entitlement]
SELECT * FROM [UserStatus]
SELECT * FROM [AccountStatus]
SELECT * FROM [BranchOffice]
SELECT * FROM [Login]
SELECT * FROM [User]
SELECT * FROM [Client]

SELECT * FROM [User] WHERE [UserId] = (SELECT [UserId] FROM [Client] WHERE [ClientId] = 'C1');

SELECT * FROM ufn_FetchUserDetails(1);
SELECT * FROM ufn_FetchAllUserDetails();

SELECT c.ClientId, c.FirstName, c.MiddleName, c.LastName, c.DateOfBirth, c.AssetValue, c.HomePhoneNumber, c.OfficePhoneNumber, c.Email, u.FirstName, u.LastName, acc.AccountStatusDesc
FROM [Client] c LEFT JOIN [User] u ON c.UserId = u.UserId
LEFT JOIN [AccountStatus] acc ON c.AccountStatusId = acc.AccountStatusId

SELECT u.UserId, u.FirstName, u.LastName, b.BranchOfficeLocation, u.DateOfBirth, u.PhoneNumber, s.UserStatusDesc, e.EntitlementDesc
	FROM [User] u Left JOIN [BranchOffice] b ON u.BranchOfficeId = b.BranchOfficeId
	Left JOIN  [UserStatus] s ON u.StatusId = s.UserStatusId
	Left JOIN [Entitlement] e ON u.EntitlementId = e.EntitlementId