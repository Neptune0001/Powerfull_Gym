-- Crear la base de datos
CREATE DATABASE PowerfullGymDB;
GO

-- Usar la base de datos creada
USE PowerfullGymDB;
GO

-- Tabla de Usuarios (Datos personales)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Age INT NOT NULL CHECK (Age BETWEEN 10 AND 100), -- Restricci�n de edad razonable
    RegistrationDate DATETIME2 DEFAULT GETDATE(),
    Phone NVARCHAR(20) CHECK (Phone LIKE '[0-9]%'), -- Solo n�meros
    Email NVARCHAR(150) UNIQUE,
    BiometricData VARBINARY(MAX) NOT NULL, -- Datos biom�tricos cifrados
    PasswordHash VARBINARY(256) NOT NULL, -- Contrase�a cifrada (se recomienda hashing en la app)
    IsActive BIT DEFAULT 1
);
GO

-- Tabla de Datos M�dicos
CREATE TABLE MedicalData (
    MedicalDataID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Conditions NVARCHAR(500), -- Condiciones m�dicas
    Injuries NVARCHAR(500), -- Lesiones previas
    EmergencyContactName NVARCHAR(100) NOT NULL, -- Nombre del contacto de emergencia
    EmergencyContactPhone NVARCHAR(20) NOT NULL CHECK (EmergencyContactPhone LIKE '[0-9]%'), -- Solo n�meros
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
GO

-- Tabla de Experiencia Deportiva
CREATE TABLE SportsBackground (
    BackgroundID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    HasPriorExperience BIT NOT NULL, -- 1 = S�, 0 = No
    ExperienceDetails NVARCHAR(500), -- Detalles de la experiencia previa en deportes o actividad f�sica
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
GO

-- Tabla de Membres�as
CREATE TABLE Memberships (
    MembershipID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE, -- Ejemplo: B�sica, Premium, VIP
    Description NVARCHAR(250),
    Cost DECIMAL(10,2) NOT NULL CHECK (Cost >= 0), -- Evitar valores negativos
    DurationMonths INT NOT NULL CHECK (DurationMonths IN (1, 2, 3, 12)), -- Duraciones fijas
    CreationDate DATETIME2 DEFAULT GETDATE()
);
GO

-- Tabla de Membres�as por Usuario
CREATE TABLE UserMemberships (
    UserMembershipID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    MembershipID INT NOT NULL,
    StartDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (MembershipID) REFERENCES Memberships(MembershipID) ON DELETE CASCADE,
    CONSTRAINT UQ_ActiveMembership UNIQUE (UserID, MembershipID, IsActive) -- Evitar membres�as activas duplicadas
);
GO

-- Vista para calcular la fecha de finalizaci�n de membres�a
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

-- Tabla de Asistencia
CREATE TABLE Attendance (
    AttendanceID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    CheckInTime DATETIME2 DEFAULT GETDATE(),
    CheckOutTime DATETIME2,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    CHECK (CheckOutTime IS NULL OR CheckOutTime >= CheckInTime) -- Validar que el CheckOut sea despu�s del CheckIn
);
GO

-- Tabla de Roles
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200)
);
GO

-- Tabla de Relaci�n Usuario-Roles
CREATE TABLE UserRoles (
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    PRIMARY KEY (UserID, RoleID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID) ON DELETE CASCADE
);
GO
