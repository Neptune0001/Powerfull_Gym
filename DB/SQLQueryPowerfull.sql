-- Create the database
CREATE DATABASE PowerfullGymDB;
GO

-- Use the created database
USE PowerfullGymDB;
GO

-- Users Table (Personal Data)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Age INT NOT NULL CHECK (Age BETWEEN 10 AND 100), -- Restricción de edad razonable
    RegistrationDate DATETIME2 DEFAULT GETDATE(),
    Phone NVARCHAR(20) CHECK (Phone LIKE '[0-9]%'), -- Solo números
    Email NVARCHAR(150) UNIQUE,
    BiometricData VARBINARY(MAX) NOT NULL, -- Datos biométricos cifrados
    Password VARBINARY(256) NOT NULL, -- Contraseña cifrada (se recomienda hashing en la app)
    IsActive BIT DEFAULT 1
);
GO

-- Medical Data Table
CREATE TABLE MedicalData (
    MedicalDataID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Conditions NVARCHAR(500), -- Medical conditions
    Injuries NVARCHAR(500), -- Past injuries
    EmergencyContactName NVARCHAR(100) NOT NULL, -- Emergency contact name
    EmergencyContactPhone NVARCHAR(20) NOT NULL CHECK (EmergencyContactPhone LIKE '[0-9]%'), -- Only numbers
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
GO

-- Sports Background Table
CREATE TABLE SportsBackground (
    BackgroundID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    HasPriorExperience BIT NOT NULL, -- 1 = Yes, 0 = No
    ExperienceDetails NVARCHAR(500), -- Details of prior sports or physical activity
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
GO

-- Memberships Table
CREATE TABLE Memberships (
    MembershipID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE, -- Example: Basic, Premium, VIP
    Description NVARCHAR(250),
    Cost DECIMAL(10, 2) NOT NULL CHECK (Cost >= 0), -- Prevent negative values
    DurationMonths INT NOT NULL CHECK (DurationMonths IN (1, 2, 3, 12)), -- Fixed durations
    CreationDate DATETIME2 DEFAULT GETDATE()
);
GO

-- User Memberships Table
CREATE TABLE UserMemberships (
    UserMembershipID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    MembershipID INT NOT NULL,
    StartDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (MembershipID) REFERENCES Memberships(MembershipID) ON DELETE CASCADE,
    CONSTRAINT UQ_ActiveMembership UNIQUE (UserID, MembershipID, IsActive) -- Prevent duplicate active memberships
);
GO

-- Vista para calcular EndDate
CREATE VIEW vw_UserMembershipsWithEndDate AS
SELECT 
    UM.UserMembershipID,
    UM.UserID,
    UM.MembershipID,
    UM.StartDate,
    DATEADD(MONTH, M.DurationMonths, UM.StartDate) AS EndDate,
    UM.IsActive
FROM UserMemberships UM
INNER JOIN Memberships M ON UM.MembershipID = M.MembershipID;
GO

-- Attendance Table
CREATE TABLE Attendance (
    AttendanceID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    CheckInTime DATETIME2 DEFAULT GETDATE(),
    CheckOutTime DATETIME2,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    CHECK (CheckOutTime IS NULL OR CheckOutTime >= CheckInTime) -- Ensure valid check-out time
);
GO

-- Roles Table
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200)
);
GO

-- User Roles Relationship Table
CREATE TABLE UserRoles (
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    PRIMARY KEY (UserID, RoleID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID) ON DELETE CASCADE
);
GO
