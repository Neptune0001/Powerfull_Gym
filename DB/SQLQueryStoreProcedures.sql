ALTER PROCEDURE sp_RegisterUser
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Email NVARCHAR(150),
    @Password VARBINARY(256),
    @RoleID INT -- ID del rol (Administrador, Recepcionista, etc.)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el correo ya está registrado
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        -- El correo electrónico ya está en uso.
        RETURN -1; -- Correo ya registrado
    END

    -- Insertar nuevo usuario
    INSERT INTO Users (FirstName, LastName, Phone, Email, Password, IsActive)
    VALUES (@FirstName, @LastName, @Phone, @Email, @Password, 1);

    -- Asignar rol al usuario recién insertado
    INSERT INTO UserRoles (UserID, RoleID)
    VALUES (SCOPE_IDENTITY(), @RoleID);
	
    -- Usuario Registrado Exitosamente
    RETURN 1; -- Registro exitoso
END;

GO

ALTER PROCEDURE sp_LoginUser
    @Email NVARCHAR(150),
    @Password NVARCHAR(256) -- Contraseña proporcionada (en formato string)
AS
BEGIN
    SET NOCOUNT ON;

    -- Declarar variables para los datos de retorno
    DECLARE @UserID INT;
    DECLARE @FirstName NVARCHAR(100);
    DECLARE @LastName NVARCHAR(100);
    DECLARE @IsActive BIT;
    DECLARE @RoleName NVARCHAR(50);

    -- Verificar si el correo electrónico existe
    IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        -- Correo electrónico no registrado.
        RETURN -1; -- Correo no registrado
    END

    -- Comparar la contraseña encriptada
    SELECT @UserID = UserID, 
           @FirstName = FirstName, 
           @LastName = LastName, 
           @IsActive = IsActive
    FROM Users
    WHERE Email = @Email;

    -- Verificar si el usuario está activo
    IF @IsActive = 0
    BEGIN
        -- El usuario está inactivo.
        RETURN -3; -- Usuario inactivo
    END

    -- Verificar la contraseña
    IF NOT EXISTS (
        SELECT 1 FROM Users 
        WHERE Email = @Email AND Password = CONVERT(VARBINARY(256), HASHBYTES('SHA2_256', @Password))
    )
    BEGIN
        -- Contraseña incorrecta.
        RETURN -2; -- Contraseña incorrecta
    END

    -- Obtener el rol del usuario
    SELECT @RoleName = R.RoleName
    FROM Roles R
    INNER JOIN UserRoles UR ON R.RoleID = UR.RoleID
    WHERE UR.UserID = @UserID;

    -- Devolver los datos del usuario y el rol
    SELECT @UserID AS UserID,
           @FirstName AS FirstName,
           @LastName AS LastName,
           @RoleName AS Role,
           @IsActive AS IsActive;
END;
GO
