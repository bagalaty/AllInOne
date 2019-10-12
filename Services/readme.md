# Markdown File

Add-Migration -Context allinoneContext MySQLInitialCreate

Update-Database -Context allinoneContext



CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` NVARCHAR (150) NOT NULL,
    `ProductVersion` NVARCHAR (32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);