/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 01, 2020
	Developed By: Ying LUO
*/

USE master;
GO

-- Ying LUO: Re-Build Database if existed
IF DB_ID (N'AragonPharmacy20V1') IS NOT NULL
DROP DATABASE AragonPharmacy20V1;
GO

-- Ying LUO: Creating a database without specifying files
CREATE DATABASE AragonPharmacy20V1
;
GO

-- Ying LUO: Verify the database files and sizes
SELECT name, size, size*1.0/128 AS [Size in MBs]
FROM sys.master_files
WHERE name = N'AragonPharmacy20V1';
GO

/* return information about AragonPharmacy20V1 database usinf system stored procedure.
Syntax: exec(ute) procedure_name */
execute sp_helpdb 'AragonPharmacy20V1'
;
go
