CREATE DATABASE caduca CHARACTER SET utf8 COLLATE utf8_general_ci;
CREATE USER 'AdminCaduca'@'localhost' IDENTIFIED BY 'StKRV6MR6A'; 
GRANT ALL PRIVILEGES ON caduca.* TO 'AdminCaduca'@'localhost';
CREATE USER 'SistemaCaduca'@'localhost' IDENTIFIED BY 'xADcUaP5cs';
GRANT SELECT,INSERT,UPDATE,DELETE ON caduca.* TO 'SistemaCaduca'@'localhost';