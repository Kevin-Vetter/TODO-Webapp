USE [master]
GO
ALTER DATABASE ToDoDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE IF EXISTS ToDoDB
GO
CREATE DATABASE ToDoDB
--TABLES--
GO
USE ToDoDB
CREATE TABLE Users(
    UserID INT NOT NULL IDENTITY(1,1) PRIMARY KEY(UserID),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(250) NOT NULL,
    Username NVARCHAR(32) NOT NULL,
    [Password] NVARCHAR(150) NOT NULL,
    [Disabled] BIT DEFAULT 0 NOT NULL,
    CONSTRAINT UQ_Users_Username UNIQUE (Username)
)
GO
CREATE TABLE Priorities(
    PrioID INT NOT NULL IDENTITY(1,1) PRIMARY KEY(PrioID),
    [Priority] NVARCHAR(6),
)
GO
CREATE TABLE ToDos(
    ToDoID UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY DEFAULT NEWID(),
    PrioID INT NOT NULL REFERENCES Priorities (PrioID),
    Title NVARCHAR(25) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    Created DATE NOT NULL,
    Completed BIT NOT NULL,
    [Disabled] BIT DEFAULT 0 NOT NULL
)
GO
CREATE TABLE Users_ToDos(
    CombinedID INT NOT NULL IDENTITY(1,1) PRIMARY KEY(CombinedID),
    UserID INT NOT NULL REFERENCES Users(UserID),
    ToDoID UNIQUEIDENTIFIER  NOT NULL REFERENCES ToDos(ToDoID),
    [Disabled] BIT DEFAULT 0 NOT NULL
)
--TABLE DATA--
INSERT INTO Priorities
(Priority)
VALUES
('Low'),
('Normal'),
('High')
GO
INSERT INTO Users
(FirstName,LastName,Username,[Password])
VALUES('Kevin','Vetter','MEKURUTO','P@ssw0rd')
--PROCEDURES--
GO
CREATE PROCEDURE CreateUser
    @F_Name NVARCHAR(50),
    @L_Name NVARCHAR(250),
    @Username NVARCHAR(32),
    @Password NVARCHAR(150)
AS
    INSERT INTO Users
    (FirstName, LastName, Username, [Password])
    VALUES
    (@F_Name, @L_Name, @Username, @Password)
GO
CREATE PROCEDURE UpdateUser
    @ID INT,
    @F_Name NVARCHAR(50),
    @L_Name NVARCHAR(250),
    @Username NVARCHAR(32),
    @Password NVARCHAR(150)
AS
    UPDATE UserTable
    SET FirseName = @F_Name, LastName = @L_Name, Username = @Username, [Password] = @Password
    WHERE UserID = @ID
GO
CREATE PROCEDURE DisableUser
    @ID INT
AS
    UPDATE Users
    SET [Disabled] = 1
    WHERE UserID = @ID
GO
CREATE PROCEDURE GetUser
    @Username NVARCHAR(50)
AS
    SELECT * FROM GetUserView
    WHERE Username = @Username
GO
CREATE PROCEDURE CreateToDo
    @ToDoID NVARCHAR(36),
    @Title NVARCHAR(50),
    @Description NVARCHAR(50),
    @Created DATE,
    @Priority INT,
    @Completed BIT,
    @UserID INT
AS
    INSERT INTO ToDos (ToDoID, PrioID, Title, [Description], Created, Completed)
    VALUES (@ToDoID, @Priority, @Title, @Description, @Created, @Completed)
    INSERT INTO Users_ToDos (UserID,ToDoID)
    VALUES (@UserID, @ToDoID)
GO
CREATE PROCEDURE UpdateToDo
    @ToDoID NVARCHAR(36),
    @Description NVARCHAR(50),
    @Priority INT
AS
    UPDATE ToDos
    SET [Description] = @Description, PrioID = @Priority
    WHERE ToDoID = @ToDoID
GO
CREATE PROCEDURE DeleteToDo
    @ToDoID NVARCHAR(50)
AS
    UPDATE ToDos
    SET [Disabled] = 1
    WHERE ToDoID = @ToDoID
GO
CREATE PROCEDURE CompleteToDo
    @ToDoID NVARCHAR(50)
AS
    UPDATE ToDos
    SET Completed = 1
    WHERE ToDoID = @ToDoID
GO
CREATE PROCEDURE GetUsersToDos
    @ID INT
AS
    SELECT T.ToDoID, PrioID, Title, [Description], Created, Completed
    FROM GetToDoView AS T INNER JOIN
    GetUserToDoView AS UT ON T.ToDoID = UT.ToDoID INNER JOIN
    GetUserView AS U ON UT.UserID = U.UserID
    WHERE U.UserID = @ID
GO
CREATE PROCEDURE AllUsernames
AS
    SELECT * FROM GetAllUsernames
GO
CREATE PROCEDURE AddPartyMember
@ToDoID NVARCHAR(36),
@Username NVARCHAR(32)
AS
    INSERT INTO Users_ToDos
    (UserID,ToDoID)
    VALUES
    ((SELECT UserID FROM GetUserView WHERE Username = @Username), @ToDoID)
GO
--VIEWS--
CREATE VIEW GetToDoView
AS
    SELECT ToDoID,PrioID,Title,Description,Created,Completed FROM ToDos
    WHERE [Disabled] = 0
GO
CREATE VIEW GetUserToDoView
AS
    SELECT * from Users_ToDos
GO
CREATE VIEW GetUserView
AS
    SELECT * FROM Users
    WHERE [Disabled] = 0
GO
CREATE VIEW GetAllUsernames
AS
    SELECT Username FROM Users