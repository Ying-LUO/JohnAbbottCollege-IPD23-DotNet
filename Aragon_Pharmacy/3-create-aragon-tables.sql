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


/***** Table No. 1 - HumanResources.JobTitle ****/
CREATE TABLE HumanResources.JobTitle
(
	JobID int IDENTITY(1,1) NOT NULL,
	Title nvarchar(30) NOT NULL,   --Ying LUO: According to the request in documents 30 characters
	CONSTRAINT PK_JobTitle PRIMARY KEY CLUSTERED (JobID ASC)
)
;
go

/***** Table No. 2 - HumanResources.Class ****/
CREATE TABLE HumanResources.Class
(
	ClassID int IDENTITY(1,1) NOT NULL, -- auto-generated number
	Description nvarchar(100) NULL,
	Cost MONEY NOT NULL,
	Renewal SMALLINT NOT NULL,
	Required bit NOT NULL,
	Provideer nvarchar(50) NOT NULL,
	CONSTRAINT PK_Class PRIMARY KEY CLUSTERED (ClassID ASC)
)
;
go


/***** Table No. 3 - Sales.HouseHold ****/
CREATE TABLE Sales.HouseHold
(
	HouseID int IDENTITY(1,1) NOT NULL, -- auto-generated number
	Address nvarchar(60) NULL,
	City nvarchar(15) NULL,
	Prov NCHAR(2) NULL, -- YING LUO: CANADIAN Specification: State nvarchar(15) NULL,
	PostalCode NVARCHAR(7) NULL,   -- Ying LUO: Canadian Specification: ZIP nvarchar(10) NULL,
	CONSTRAINT PK_HouseHold PRIMARY KEY CLUSTERED (HouseID ASC)
)
;
go

/***** Table No. 4 - Sales.HealthPlan ****/
CREATE TABLE Sales.HealthPlan
(
	PlanID NVARCHAR(15) NOT NULL, -- YING LUO: SIZE NOT SURE
	PlanName NVARCHAR(30) NOT NULL,  -- YING LUO: SIZE NOT SURE
	Address nvarchar(60) NOT NULL,
	City nvarchar(15) NOT NULL,
	Prov NCHAR(2) NOT NULL, -- YING LUO: CANADIAN Specification: State nvarchar(15) NULL,
	PostalCode NVARCHAR(7) NOT NULL,   -- Ying LUO: Canadian Specification: ZIP nvarchar(10) NULL,
	Phone nvarchar(15) NOT NULL,
	Days int NOT NULL,
	Website NVARCHAR(30) NULL,  -- YING LUO: SIZE NOT SURE
	CONSTRAINT PK_HealthPlan PRIMARY KEY CLUSTERED (PlanID ASC)
)
;
go


/***** Table No. 5 - Pharmacy.Clinic ****/
CREATE TABLE Pharmacy.Clinic
(
	ClinicID int IDENTITY(1,1) NOT NULL, -- auto-generated number
	ClinicName NVARCHAR(50) NOT NULL,
	Address1 nvarchar(40) NULL,
	Address2 nvarchar(40) NULL,
	City nvarchar(40) NULL,
	Prov NCHAR(2) NULL, -- YING LUO: CANADIAN Specification: State nvarchar(15) NULL,
	PostalCode NVARCHAR(7) NULL,   -- Ying LUO: Canadian Specification: ZIP nvarchar(10) NULL,
	Phone nvarchar(15) NULL,
	CONSTRAINT PK_Clinic PRIMARY KEY CLUSTERED (ClinicID ASC)
)
;
go


/***** Table No. 6 - Pharmacy.Drug ****/
CREATE TABLE Pharmacy.Drug
(
	DIN NCHAR(8) NOT NULL,   -- YING LUO: Eight digits, CANADIAN Specification: Replace UPN in U.S.   
	Name NVARCHAR(30) NOT NULL,   --Ying LUO: request NOT EXCEED 30 CHARACTERS 
	Generic BIT NOT NULL,
	Description NVARCHAR(200) NULL,  --Ying LUO: contraindications, generic equivalents, and recommended dosage.
	Unit NVARCHAR(10) NULL,    --YING LUO: the unit of measure for a drug, such as pill or bottle
	Dosage NUMERIC(6,2) NOT NULL,  --YING LUO: ACCORDING TO THE TXT DATA FORMAT
	DosageForm nvarchar(20) NOT NULL,
	Cost MONEY NOT NULL,
	Price MONEY NOT NULL,
	Fee MONEY NOT NULL,
	Interactions NVARCHAR(200) NULL,  --YING LUO: CAN BE NULL
	Supplier NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_Drug PRIMARY KEY CLUSTERED (DIN ASC)
)
;
go


/***** Table No. 7 - Pharmacy.Doctor ****/
CREATE TABLE Pharmacy.Doctor
(
	DoctorID int IDENTITY(1,1) NOT NULL, -- auto-generated number
	DoctorFirst NVARCHAR(30) NOT NULL,   --YING LUO: According to the document, 30 characters
	DoctorLast nvarchar(30) NULL,  --YING LUO: According to the document, 30 characters
	Phone nvarchar(15) NOT NULL,  --YING LUO: According to the document, 15 characters
	Cell nvarchar(15) NOT NULL,
	ClinicID int NOT NULL,
	CONSTRAINT PK_Doctor PRIMARY KEY CLUSTERED (DoctorID ASC)
)
;
go

/***** Table No. 8 - HumanResources.Employee ****/
CREATE TABLE HumanResources.Employee
(
	EmpID int IDENTITY(1,1) NOT NULL, -- auto-generated number
	EmpFirst nvarchar(30) NOT NULL,   --Ying LUO: According to requests, not exceed 30 characters
	EmpMi nvarchar(2) NULL,
	EmpLast nvarchar(30) NULL, 
	SIN nvarchar(11) NOT NULL,
	DOB datetime NOT NULL,
	StartDate datetime NOT NULL,
	EndDate datetime NULL,
	Address nvarchar(60) NOT NULL,
	City nvarchar(15) NOT NULL,
	Prov NCHAR(2) NOT NULL, -- YING LUO: CANADIAN Specification: State nvarchar(15) NULL,
	PostalCode NVARCHAR(7) NOT NULL,   -- Ying LUO: Canadian Specification: ZIP nvarchar(10) NULL,
	JobID int NOT NULL,
	Memo nvarchar(255) NULL,
	Phone nvarchar(15) NULL,
	Cell nvarchar(15) NULL,
	Salary money NULL,
	HourlyRate money NULL,
	Review datetime NULL,
	CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED (EmpID ASC)
)
;
go


/***** Table No. 9 - HumanResources.EmployeeTraining ****/
CREATE TABLE HumanResources.EmployeeTraining
(
	EmpID INT NOT NULL,
	DATE DATETIME NOT NULL,
	ClassID INT NOT NULL,
	CONSTRAINT PK_EmployeeTraining PRIMARY KEY CLUSTERED (EmpID ASC, DATE ASC, ClassID ASC)
)
;
go


/***** Table No. 10 - Sales.Customer ****/
CREATE TABLE Sales.Customer
(
	CustID int IDENTITY(1,1) NOT NULL, -- auto-generated number
	CustFirst nvarchar(30) NOT NULL,
	CustLast nvarchar(30) NULL,
	Phone nvarchar(15) NULL,
	DOB DATETIME NOT NULL,
	Gender NCHAR(1) NOT NULL, 
	Balance MONEY NOT NULL,
	ChildCap BIT NOT NULL,
	PlanID NVARCHAR(15) NULL, -- YING LUO: SIZE NOT SURE
	HouseID int NOT NULL,
	HeadHH BIT NOT NULL,  -- YING LUO: NO IDEA WHAT ABOUT THIS FIELD
	Allegies NVARCHAR(30) NOT NULL,  -- YING LUO: SIZE NOT SURE
	CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (CustID ASC)
)
;
go


/***** Table No. 11 - Pharmacy.Rx ****/
CREATE TABLE Pharmacy.Rx
(
	PrescriptionID INT IDENTITY(1,1) NOT NULL, -- auto-generated number
	DIN NCHAR(8) NOT NULL,   -- YING LUO: CANADIAN Specification: Replace UPN in U.S.
	Quantity NUMERIC(5,2) NOT NULL,   --Ying LUO: Numeric field that might contain decial places
	Unit NVARCHAR(10) NOT NULL, --YING LUO: the unit of measure for a drug, such as pill or bottle
	Date DATETIME NOT NULL,
	ExpireDate DATETIME NOT NULL,
	Refills SMALLINT NOT NULL,
	AutoRefill BIT NULL,  --Ying LUO: TRUE is converted to 1 and FALSE is converted to 0.
	RefillsUsed SMALLINT NOT NULL,
	Instructions NVARCHAR(50) NOT NULL,
	CustID int NOT NULL,
	DoctorID INT NOT NULL,
	CONSTRAINT PK_Rx PRIMARY KEY CLUSTERED (PrescriptionID ASC)
)
;
go


/***** Table No. 12 - Pharmacy.Refill ****/
CREATE TABLE Pharmacy.Refill
(
	PrescriptionID INT NOT NULL,
	RefillDate DATETIME NOT NULL,
	EmpID INT NOT NULL,
	CONSTRAINT PK_Refill PRIMARY KEY CLUSTERED (PrescriptionID ASC, RefillDate ASC)
)
;
go