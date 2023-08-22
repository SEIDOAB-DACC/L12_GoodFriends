USE goodfriendsefc;
GO

--Create 3 logins
CREATE LOGIN gstusr WITH PASSWORD=N'pa$$Word1', 
    DEFAULT_DATABASE=goodfriendsefc, DEFAULT_LANGUAGE=us_english, 
    CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

CREATE LOGIN usr WITH PASSWORD=N'pa$$Word1', 
DEFAULT_DATABASE=goodfriendsefc, DEFAULT_LANGUAGE=us_english, 
CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

CREATE LOGIN supusr WITH PASSWORD=N'pa$$Word1', 
DEFAULT_DATABASE=goodfriendsefc, DEFAULT_LANGUAGE=us_english, 
CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;


--create 3 users from the logins, we will late set credentials for these
CREATE USER gstusrUser FROM LOGIN gstusr;
CREATE USER usrUser FROM LOGIN usr;
CREATE USER supusrUser FROM LOGIN supusr;

/*
--Verify: See logins and users
SELECT * FROM sys.sql_logins;
SELECT * FROM sys.database_principals
WHERE type_desc = 'SQL_USER'
*/

/*
--Cleanup
DROP USER gstusrUser;
DROP USER usrUser;
DROP USER supusrUser;

DROP LOGIN gstusr;
DROP LOGIN usr;
DROP LOGIN supusr;
*/