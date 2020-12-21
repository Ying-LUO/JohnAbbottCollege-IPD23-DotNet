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

/***** LIST OF ALL USER DEFINED TABLES ******/
select *
from sys.all_objects
where [type]= 'U'
;
go

/***** LIST OF ALL THE CONSTRIANTS ******/
SELECT OBJECT_NAME(OBJECT_ID) AS NameofConstraint,
SCHEMA_NAME(schema_id) AS SchemaName,
OBJECT_NAME(parent_object_id) AS TableName,
type_desc AS ConstraintType
FROM sys.objects
WHERE type_desc LIKE '%CONSTRAINT';
GO

/****** USE TO CHECK OBJECT STATUS **********
SELECT * FROM sys.objects
WHERE Object_id = OBJECT_ID(' ');
GO
*/

/***** DISABLE ALL THE CONSTRAINT BEFORE DROP TABLES ******/
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
GO


-- DROP VIEW BEFORE CREATE THEM --
DROP VIEW IF EXISTS HumanResources.EmployeeHourlyRateView ;  
GO  

DROP VIEW IF EXISTS HumanResources.EmployeeListView ;  
GO  

DROP VIEW IF EXISTS HumanResources.FirstEmployeeHiredListView ;  
GO  

DROP VIEW IF EXISTS HumanResources.HourlyRateAnalysisView ;  
GO  

DROP VIEW IF EXISTS HumanResources.HourlyRateSummaryView ;  
GO  

DROP VIEW IF EXISTS HumanResources.PharmacistListView ;  
GO  

DROP VIEW IF EXISTS HumanResources.SpeakSpanishView ;  
GO  

DROP VIEW IF EXISTS HumanResources.EmployeePhoneListView ;  
GO 

DROP VIEW IF EXISTS HumanResources.ReprimandListView ;  
GO 

DROP VIEW IF EXISTS HumanResources.StartDateListView ;  
GO 

-- DROP VIEW BEFORE CREATE THEM --
DROP VIEW IF EXISTS HumanResources.DuplicatePostalCodesView ;  
GO  

DROP VIEW IF EXISTS HumanResources.NoTrainingView ;  
GO  

DROP VIEW IF EXISTS HumanResources.EmployeeTrainingView ;  
GO  

DROP VIEW IF EXISTS HumanResources.UpToDateView ;  
GO  

DROP VIEW IF EXISTS HumanResources.Top5HourlyRatesView ;  
GO  

DROP VIEW IF EXISTS HumanResources.MaxMinAvgHourlyRate ;  
GO  

DROP VIEW IF EXISTS HumanResources.EmployeeAgeView ;  
GO  

DROP VIEW IF EXISTS HumanResources.AvgEmployeeAgeView ;  
GO  

/********* DROP ALL TRIGGER BEFORE CREATE *******/
IF OBJECT_ID ('Pharmacy.OverExpiredDate', 'TR') IS NOT NULL  
   DROP TRIGGER Pharmacy.OverExpiredDate;  

IF OBJECT_ID ('Pharmacy.OverRefillTimes', 'TR') IS NOT NULL  
   DROP TRIGGER Pharmacy.OverRefillTimes;  
   


/********* DROP ALL FUNCTIONS BEFORE CREATE *******/
IF OBJECT_ID('HumanResources.ClassCostAnalysisFn', 'TF') IS NOT NULL
DROP FUNCTION HumanResources.ClassCostAnalysisFn;
GO

IF OBJECT_ID('HumanResources.ClassCostAnalysisByYearAndClassFn', 'FN') IS NOT NULL
DROP FUNCTION HumanResources.ClassCostAnalysisByYearAndClassFn;
GO

IF OBJECT_ID('HumanResources.EmpHistoryFn', 'IF') IS NOT NULL
DROP FUNCTION HumanResources.EmpHistoryFn;
GO

IF OBJECT_ID('HumanResources.EmployeeTrainingHistoryFn', 'IF') IS NOT NULL
DROP FUNCTION HumanResources.EmployeeTrainingHistoryFn;
GO

IF OBJECT_ID('HumanResources.ObsoleteClassesFn', 'TF') IS NOT NULL
DROP FUNCTION HumanResources.ObsoleteClassesFn;
GO

IF OBJECT_ID('HumanResources.DeleteClassesView', 'IF') IS NOT NULL
DROP FUNCTION HumanResources.DeleteClassesView;
GO

IF OBJECT_ID('HumanResources.TechnicianRaiseFn', 'IF') IS NOT NULL
DROP FUNCTION HumanResources.TechnicianRaiseFn;
GO

IF OBJECT_ID('HumanResources.RetirementView', 'IF') IS NOT NULL
DROP FUNCTION HumanResources.RetirementView;
GO

IF OBJECT_ID('HumanResources.Top3SalariesFn', 'IF') IS NOT NULL
DROP FUNCTION HumanResources.Top3SalariesFn;
GO

if OBJECT_ID('HumanResources.EmployeeHistory', 'U') is not null
DROP TABLE HumanResources.EmployeeHistory;
GO

if OBJECT_ID('HumanResources.EmployeeTrainingHistory', 'U') is not null
DROP TABLE HumanResources.EmployeeTrainingHistory;
GO

---- USED FOR STORE PROCEDURE
IF OBJECT_ID('Pharmacy.CustomerNumbersByDrugNumbers', 'FN') IS NOT NULL
DROP FUNCTION Pharmacy.CustomerNumbersByDrugNumbers;
GO

IF OBJECT_ID('Pharmacy.TotalCustomerNumberWhoBoughtDrugsFn', 'FN') IS NOT NULL
DROP FUNCTION Pharmacy.TotalCustomerNumberWhoBoughtDrugsFn;
GO

IF OBJECT_ID('Pharmacy.GreatestNumberOfDrugsFn', 'FN') IS NOT NULL
DROP FUNCTION Pharmacy.GreatestNumberOfDrugsFn;
GO

IF OBJECT_ID('Pharmacy.CustomerNumbersByDrugNumbersFn', 'TF') IS NOT NULL
DROP FUNCTION Pharmacy.CustomerNumbersByDrugNumbersFn;
GO


/***** DROP PROCEDURE BEFORE CREATE THEM *****/
IF OBJECT_ID ( 'Pharmacy.TotalSalesUSP', 'P' ) IS NOT NULL
    DROP PROCEDURE Pharmacy.TotalSalesUSP;
GO

IF OBJECT_ID ( 'Pharmacy.CountCustomerNumbersByDrugNumbersUSP', 'P' ) IS NOT NULL
    DROP PROCEDURE Pharmacy.CountCustomerNumbersByDrugNumbersUSP;
GO

IF OBJECT_ID ( 'Sales.HealthPlanCustomerProportionUSP', 'P' ) IS NOT NULL
    DROP PROCEDURE Sales.HealthPlanCustomerProportionUSP;
GO

IF OBJECT_ID ( 'TransactionTestUSP', 'P' ) IS NOT NULL
    DROP PROCEDURE TransactionTestUSP;
GO



/***** DROP TABLE BEFORE CREATE THEM ******/

if OBJECT_ID('Pharmacy.Refill', 'U') is not null
	drop table Pharmacy.Refill
;
go

if OBJECT_ID('Pharmacy.Rx', 'U') is not null
	drop table Pharmacy.Rx
;
go

if OBJECT_ID('Sales.Customer', 'U') is not null
	drop table Sales.Customer
;
go

if OBJECT_ID('HumanResources.EmployeeTraining', 'U') is not null
	drop table HumanResources.EmployeeTraining 
;
go

if OBJECT_ID('HumanResources.Employee', 'U') is not null
	drop table HumanResources.Employee
;
go

if OBJECT_ID('Pharmacy.Doctor', 'U') is not null
	drop table Pharmacy.Doctor
;
go

if OBJECT_ID('Pharmacy.Drug', 'U') is not null
	drop table Pharmacy.Drug
;
go

if OBJECT_ID('Pharmacy.Clinic', 'U') is not null
	drop table Pharmacy.Clinic
;
go

if OBJECT_ID('Sales.HealthPlan', 'U') is not null
	drop table Sales.HealthPlan
;
go

if OBJECT_ID('Sales.HouseHold', 'U') is not null
	drop table Sales.HouseHold
;
go

if OBJECT_ID('HumanResources.Class', 'U') is not null
	drop table HumanResources.Class
;
go

if OBJECT_ID('HumanResources.JobTitle', 'U') is not null
	drop table HumanResources.JobTitle
;
go

