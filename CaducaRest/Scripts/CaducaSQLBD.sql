CREATE DATABASE caduca
GO
CREATE LOGIN AdminCaduca WITH PASSWORD = 'StKRV6MR6A'
GO
CREATE LOGIN SistemaCaduca WITH PASSWORD = 'xADcUaP5cs'
GO
USE caduca
GO
CREATE USER AdminCaduca FOR LOGIN AdminCaduca;
CREATE USER SistemaCaduca FOR LOGIN SistemaCaduca;

ALTER ROLE db_owner ADD MEMBER AdminCaduca;

ALTER ROLE db_datareader ADD MEMBER SistemaCaduca;
ALTER ROLE db_datawriter ADD MEMBER SistemaCaduca;