CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Categoria` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Clave` int NOT NULL,
    `Nombre` VARCHAR(80) NOT NULL,
    CONSTRAINT `PK_Categoria` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20181007234331_InitialCreate', '2.1.4-rtm-31024');

