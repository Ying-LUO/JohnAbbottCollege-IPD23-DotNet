/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 01, 2020
	Developed By: Ying LUO
*/

USE master;
GO

-- switch to the AragonPharmacy20V1 database
USE AragonPharmacy20V1
;
GO

-- YING LUO: DROP SCHEMA IF NEEDED

DROP SCHEMA Pharmacy;
GO 

DROP SCHEMA HumanResources;
GO 

DROP SCHEMA Sales;
GO 


/* create schema and set the owner to each schma according to the Table 1 - Preliminary database exectations for 4Corners Pharmacy, 
    set each department into schema of database
*/
-- 1) create Pharmacy schema
-- Table: Clinic
-- Table: Doctor
-- Table: Prescription
-- Table: Refill
-- Table: Drugs
create schema Pharmacy authorization dbo
;
go

-- 2) create HumanResources schema
-- Table: Employee
-- Table: JobTitle
-- Table: EmployeeTraining
-- Table: Class
create schema HumanResources authorization dbo
;
go

-- 3) create Sales schema
-- Table: Customer
-- Table: HouseHold
-- Table: HealthPlan
create schema Sales authorization dbo
;
go

/* YING LUO:

Query below lists all schemas in SQL Server database. Schemas include default db_* , sys, information_schema and guest schemas.

If you want to list user only schemas use this script.*/

select s.name as schema_name, 
    s.schema_id,
    u.name as schema_owner
from sys.schemas s
    inner join sys.sysusers u
        on u.uid = s.principal_id
order by s.name