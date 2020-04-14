CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181007234331_InitialCreate') THEN

    CREATE TABLE `Categoria` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Clave` int NOT NULL,
        `Nombre` VARCHAR(80) NOT NULL,
        CONSTRAINT `PK_Categoria` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181007234331_InitialCreate') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20181007234331_InitialCreate', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181101045843_IndicesCategoria') THEN

    CREATE UNIQUE INDEX `UI_CategoriaClave` ON `Categoria` (`Clave`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181101045843_IndicesCategoria') THEN

    CREATE UNIQUE INDEX `UI_CategoriaNombre` ON `Categoria` (`Nombre`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181101045843_IndicesCategoria') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20181101045843_IndicesCategoria', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181201043458_Tabla_Productos') THEN

    CREATE TABLE `Producto` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CategoriaId` int NOT NULL,
        `Clave` int NOT NULL,
        `Nombre` VARCHAR(80) NOT NULL,
        CONSTRAINT `PK_Producto` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181201043458_Tabla_Productos') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20181201043458_Tabla_Productos', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181206045514_Llave_Producto_Categoria') THEN

    CREATE INDEX `IX_ProductoCategoria` ON `Producto` (`CategoriaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181206045514_Llave_Producto_Categoria') THEN

    ALTER TABLE `Producto` ADD CONSTRAINT `FK_Producto_Categoria` FOREIGN KEY (`CategoriaId`) REFERENCES `Categoria` (`Id`) ON DELETE RESTRICT;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20181206045514_Llave_Producto_Categoria') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20181206045514_Llave_Producto_Categoria', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190129035350_ProductoNombreUnico') THEN

    CREATE UNIQUE INDEX `UX_ProductoNombre` ON `Producto` (`Nombre`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190129035350_ProductoNombreUnico') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190129035350_ProductoNombreUnico', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190130042513_IXProductoClave') THEN

    CREATE UNIQUE INDEX `UX_ProductoClave` ON `Producto` (`Clave`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190130042513_IXProductoClave') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190130042513_IXProductoClave', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190226040532_Clientes') THEN

    CREATE TABLE `Cliente` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Clave` int NOT NULL,
        `RFC` VARCHAR(15) NULL,
        `RazonSocial` VARCHAR(250) NOT NULL,
        `NombreComercial` VARCHAR(250) NOT NULL,
        `Direccion` VARCHAR(200) NOT NULL,
        `Email` VARCHAR(150) NULL,
        `Telefono` VARCHAR(20) NULL,
        `Celular` VARCHAR(20) NULL,
        `SitioWeb` VARCHAR(20) NULL,
        `Activo` tinyint(1) NOT NULL,
        CONSTRAINT `PK_Cliente` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190226040532_Clientes') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190226040532_Clientes', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190320041421_ClienteCategoria') THEN

    CREATE TABLE `ClienteCategoria` (
        `Id` int NOT NULL,
        `ClienteId` int NOT NULL,
        `CategoriaId` int NOT NULL,
        CONSTRAINT `PK_ClienteCategoria` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_ClienteCategoria_Categoria_CategoriaId` FOREIGN KEY (`CategoriaId`) REFERENCES `Categoria` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_ClienteCategoria_Cliente_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `Cliente` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190320041421_ClienteCategoria') THEN

    CREATE UNIQUE INDEX `UI_ClienteCategoriaClave` ON `Cliente` (`Clave`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190320041421_ClienteCategoria') THEN

    CREATE UNIQUE INDEX `UI_ClienteCategoriaNombre` ON `Cliente` (`RazonSocial`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190320041421_ClienteCategoria') THEN

    CREATE INDEX `IX_ClienteCategoria_CategoriaId` ON `ClienteCategoria` (`CategoriaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190320041421_ClienteCategoria') THEN

    CREATE UNIQUE INDEX `UI_ClienteForo` ON `ClienteCategoria` (`ClienteId`, `CategoriaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190320041421_ClienteCategoria') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190320041421_ClienteCategoria', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190515040239_Caducidad') THEN

    CREATE INDEX `IX_Caducidad_ProductoId` ON `Caducidad` (`ProductoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190515040239_Caducidad') THEN

    CREATE UNIQUE INDEX `UI_ClienteProducto` ON `Caducidad` (`ClienteId`, `ProductoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190515040239_Caducidad') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190515040239_Caducidad', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190531032036_TablaRol') THEN

    CREATE TABLE `Rol` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Nombre` VARCHAR(50) NOT NULL,
        CONSTRAINT `PK_Rol` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190531032036_TablaRol') THEN

    INSERT INTO `Rol` (`Id`, `Nombre`)
    VALUES (1, 'Administrador');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190531032036_TablaRol') THEN

    INSERT INTO `Rol` (`Id`, `Nombre`)
    VALUES (2, 'Vendedor');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190531032036_TablaRol') THEN

    INSERT INTO `Rol` (`Id`, `Nombre`)
    VALUES (3, 'Cliente');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190531032036_TablaRol') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190531032036_TablaRol', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE TABLE `Tabla` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Nombre` VARCHAR(40) NOT NULL,
        `Descripción` VARCHAR(200) NOT NULL,
        CONSTRAINT `PK_Tabla` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE TABLE `Usuario` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Clave` VARCHAR(15) NOT NULL,
        `Password` VARCHAR(255) NOT NULL,
        `Activo` tinyint(1) NOT NULL,
        `Adicional1` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Nombre` VARCHAR(200) NOT NULL,
        `Email` VARCHAR(80) NOT NULL,
        CONSTRAINT `PK_Usuario` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE TABLE `Historial` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `TablaId` int NOT NULL,
        `OrigenId` int NOT NULL,
        `Actividad` int NOT NULL,
        `UsuarioId` int NOT NULL,
        `FechaHora` datetime(6) NOT NULL,
        `Observa` VARCHAR(250) NULL,
        CONSTRAINT `PK_Historial` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Historial_Tabla_TablaId` FOREIGN KEY (`TablaId`) REFERENCES `Tabla` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_Historial_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE TABLE `UsuarioAcceso` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UsuarioId` int NOT NULL,
        `Fecha` datetime(6) NOT NULL,
        `Token` VARCHAR(300) NOT NULL,
        `Activo` tinyint(1) NOT NULL,
        `SistemaOperativo` VARCHAR(200) NOT NULL,
        `Navegador` VARCHAR(200) NOT NULL,
        `Ciudad` VARCHAR(300) NOT NULL,
        `Estado` VARCHAR(300) NOT NULL,
        `RefreshToken` VARCHAR(200) NOT NULL,
        `FechaRefresh` datetime(6) NOT NULL,
        `MantenerSesion` tinyint(1) NOT NULL,
        CONSTRAINT `PK_UsuarioAcceso` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UsuarioAcceso_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE TABLE `UsuarioCliente` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UsuarioId` int NOT NULL,
        `ClienteId` int NOT NULL,
        CONSTRAINT `PK_UsuarioCliente` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UsuarioCliente_Cliente_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `Cliente` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_UsuarioCliente_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE TABLE `UsuarioRol` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UsuarioId` int NOT NULL,
        `RolId` int NOT NULL,
        CONSTRAINT `PK_UsuarioRol` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UsuarioRol_Rol_RolId` FOREIGN KEY (`RolId`) REFERENCES `Rol` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_UsuarioRol_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE INDEX `IX_Historial_TablaId` ON `Historial` (`TablaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE INDEX `IX_HistorialTabla` ON `Historial` (`TabladId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE INDEX `IX_ctrUsuario` ON `Historial` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE INDEX `IX_Actividad` ON `Historial` (`Actividad`, `TabladId`, `FechaHora`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE INDEX `IX_Historial` ON `Historial` (`TabladId`, `OrigenId`, `Actividad`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE UNIQUE INDEX `UI_UsuarioClave` ON `Usuario` (`Clave`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE UNIQUE INDEX `UI_RefreshToken` ON `UsuarioAcceso` (`RefreshToken`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE UNIQUE INDEX `UI_Token` ON `UsuarioAcceso` (`UsuarioId`, `Token`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE INDEX `IX_UsuarioCliente_ClienteId` ON `UsuarioCliente` (`ClienteId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE UNIQUE INDEX `UI_UsuarioCliente` ON `UsuarioCliente` (`UsuarioId`, `ClienteId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE INDEX `IX_UsuarioRol_RolId` ON `UsuarioRol` (`RolId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    CREATE UNIQUE INDEX `UI_UsuarioRol` ON `UsuarioRol` (`UsuarioId`, `RolId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190612032258_TablasSeguridad') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190612032258_TablasSeguridad', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    ALTER TABLE `Caducidad` DROP INDEX `UI_ClienteProducto`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    INSERT INTO `Usuario` (`Id`, `Activo`, `Adicional1`, `Clave`, `Email`, `Nombre`, `Password`)
    VALUES (1, TRUE, '2a3efe03a96840478bde71ae36a20f2e', 'Juan', 'correo@gmail.com', 'Juan Peréz', '9f9b901a43d795295661443f7f7098ee8e6c6c3694428717c54d5fd058220fed');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    INSERT INTO `Usuario` (`Id`, `Activo`, `Adicional1`, `Clave`, `Email`, `Nombre`, `Password`)
    VALUES (2, TRUE, '37b93bbd77b2d7a586cc7d5032f83808', 'Maria', 'correo@gmail.com', 'Maria Lopez', '6ad9ebcfe2bebed6655a4abb3e0409c83ad1e6db35098083476744cfe0d106b9');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    INSERT INTO `Usuario` (`Id`, `Activo`, `Adicional1`, `Clave`, `Email`, `Nombre`, `Password`)
    VALUES (3, TRUE, '5dd69f799e8ac1fd877460c4d461eb74', 'Carlos', 'correo@gmail.com', 'Carlos Hernández', '6c60e72d7ea36a7defc15f0b551cd739180d2254ddaf4c8833ece2ecf8b48c5a');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    INSERT INTO `UsuarioRol` (`Id`, `RolId`, `UsuarioId`)
    VALUES (1, 3, 1);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    INSERT INTO `UsuarioRol` (`Id`, `RolId`, `UsuarioId`)
    VALUES (2, 2, 2);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    INSERT INTO `UsuarioRol` (`Id`, `RolId`, `UsuarioId`)
    VALUES (3, 1, 3);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    CREATE UNIQUE INDEX `UI_ClienteProducto` ON `Caducidad` (`ClienteId`, `ProductoId`, `Fecha`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190615033413_UsuariosRoles') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190615033413_UsuariosRoles', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190702034310_UsuariosCategorias') THEN

    CREATE TABLE `UsuarioCategoria` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UsuarioId` int NOT NULL,
        `CategoriaId` int NOT NULL,
        CONSTRAINT `PK_UsuarioCategoria` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UsuarioCategoria_Categoria_CategoriaId` FOREIGN KEY (`CategoriaId`) REFERENCES `Categoria` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_UsuarioCategoria_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190702034310_UsuariosCategorias') THEN

    CREATE INDEX `IX_UsuarioCategoria_CategoriaId` ON `UsuarioCategoria` (`CategoriaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190702034310_UsuariosCategorias') THEN

    CREATE UNIQUE INDEX `UI_UsuarioCategoria` ON `UsuarioCategoria` (`UsuarioId`, `CategoriaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190702034310_UsuariosCategorias') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190702034310_UsuariosCategorias', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    CREATE TABLE `Permiso` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Clave` int NOT NULL,
        `Nombre` VARCHAR(100) NULL,
        CONSTRAINT `PK_Permiso` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    CREATE TABLE `TablaPermiso` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `TablaId` int NOT NULL,
        `PermisoId` int NOT NULL,
        CONSTRAINT `PK_TablaPermiso` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_TablaPermiso_Permiso_PermisoId` FOREIGN KEY (`PermisoId`) REFERENCES `Permiso` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_TablaPermiso_Tabla_TablaId` FOREIGN KEY (`TablaId`) REFERENCES `Tabla` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    CREATE TABLE `RolTablaPermiso` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `TablaPermisoId` int NOT NULL,
        `RolId` int NOT NULL,
        `TienePermiso` tinyint(1) NOT NULL,
        CONSTRAINT `PK_RolTablaPermiso` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_RolTablaPermiso_Rol_RolId` FOREIGN KEY (`RolId`) REFERENCES `Rol` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_RolTablaPermiso_TablaPermiso_TablaPermisoId` FOREIGN KEY (`TablaPermisoId`) REFERENCES `TablaPermiso` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    INSERT INTO `Permiso` (`Id`, `Clave`, `Nombre`)
    VALUES (1, 1, 'Crear');
    INSERT INTO `Permiso` (`Id`, `Clave`, `Nombre`)
    VALUES (2, 2, 'Modificar');
    INSERT INTO `Permiso` (`Id`, `Clave`, `Nombre`)
    VALUES (3, 3, 'Borrar');
    INSERT INTO `Permiso` (`Id`, `Clave`, `Nombre`)
    VALUES (4, 4, 'Consultar');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    INSERT INTO `Rol` (`Id`, `Nombre`)
    VALUES (4, 'Supervisor');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (11, 'Permite registrar los clientes para los usuarios del sistema', 'UsuarioCliente');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (10, 'Permite registrar las categorias de los usuarios del sistema', 'UsuarioCategoria');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (9, 'Permite registrar los usuarios del sistema', 'Usuario');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (8, 'Permite registrar los roles de los usuarios', 'Rol');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (7, 'Permite registrar los productos', 'Producto');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (4, 'Permite registrar las categorías de productos de cada cliente', 'ClienteCategoria');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (5, 'Permite registrar los productos', 'Producto');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (12, 'Permite registrar los roles para los usuarios del sistema', 'UsuarioRol');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (3, 'Permite registrar los clientes', 'Cliente');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (2, 'Permite registrar las categorias de los productos', 'Categoria');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (1, 'Permite registrar las fechas de caducidad de los productos', 'Caducidad');
    INSERT INTO `Tabla` (`Id`, `Descripción`, `Nombre`)
    VALUES (6, 'Permite registrar los permisos para el sistema', 'Permiso');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    UPDATE `Usuario` SET `Email` = 'carlos@gmail.com'
    WHERE `Id` = 3;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (1, 1, 2);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (2, 2, 2);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (3, 3, 2);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (4, 4, 2);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (5, 1, 3);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (6, 2, 3);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (7, 3, 3);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (8, 4, 3);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (9, 1, 5);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (10, 2, 5);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (11, 3, 5);
    INSERT INTO `TablaPermiso` (`Id`, `PermisoId`, `TablaId`)
    VALUES (12, 4, 5);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (1, 4, 1, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (15, 4, 12, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (14, 4, 11, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (13, 4, 10, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (12, 4, 9, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (11, 2, 8, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (10, 4, 8, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (16, 2, 12, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (9, 4, 7, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (7, 4, 5, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (6, 3, 4, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (5, 2, 4, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (4, 4, 4, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (3, 4, 3, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (2, 4, 2, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (8, 4, 6, TRUE);
    INSERT INTO `RolTablaPermiso` (`Id`, `RolId`, `TablaPermisoId`, `TienePermiso`)
    VALUES (17, 3, 12, TRUE);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    CREATE INDEX `IX_RolTablaPermiso_RolId` ON `RolTablaPermiso` (`RolId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    CREATE UNIQUE INDEX `UI_TablaPermiso` ON `RolTablaPermiso` (`TablaPermisoId`, `RolId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    CREATE INDEX `IX_TablaPermiso_PermisoId` ON `TablaPermiso` (`PermisoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    CREATE UNIQUE INDEX `UI_TablaPermiso` ON `TablaPermiso` (`TablaId`, `PermisoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190710040544_RolTablaPermiso') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190710040544_RolTablaPermiso', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190808035237_UsuarioToken') THEN

    ALTER TABLE `UsuarioAcceso` MODIFY COLUMN `Token` VARCHAR(400) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190808035237_UsuarioToken') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190808035237_UsuarioToken', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190813035657_UsuarioCodigo') THEN

    ALTER TABLE `Usuario` ADD `Codigo` int NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190813035657_UsuarioCodigo') THEN

    ALTER TABLE `Usuario` ADD `Intentos` int NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20190813035657_UsuarioCodigo') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20190813035657_UsuarioCodigo', '3.1.3');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

