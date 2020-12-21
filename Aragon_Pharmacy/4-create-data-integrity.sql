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
ALTER TABLE HumanResources.JobTitle
	ADD CONSTRAINT uq_title_JobTitle unique (Title)
;
Go

/***** Table No. 2 - HumanResources.Class ****/
ALTER TABLE HumanResources.Class
ADD
	CONSTRAINT CK_Class_Cost CHECK (Cost > 0);
GO


/***** Table No. 3 - Sales.HouseHold ****/
ALTER TABLE Sales.HouseHold
ADD
	CONSTRAINT DF_HouseHold_Province DEFAULT ('QC') for Prov,
	CONSTRAINT CK_HouseHold_Province CHECK (Prov LIKE '\b(?:AB|BC|CB|LB|MB|NF|NWT|ON|PE|QC|PC|QU|SK|YT)\b' AND Prov = UPPER(Prov)),  -- YING LUO: ADD CHECK CONSTRAINT TO ALLOW UPPER ALPHABETS ONLY
	CONSTRAINT CK_HouseHold_PostalCode CHECK (PostalCode LIKE '[A-Z][0-9][A-Z] [A-Z][0-9][A-Z]')  -- Ying LUO: ADD CHECK CONSTRAINT TO MEET THE FORMAT
;
go

/***** Table No. 4 - Sales.HealthPlan ****/
ALTER TABLE Sales.HealthPlan
ADD
	CONSTRAINT DF_Province_HealthPlan DEFAULT ('QC') for Prov,
	CONSTRAINT CK_HealthPlan_Province CHECK (Prov LIKE '\b(?:AB|BC|CB|LB|MB|NF|NWT|ON|PE|QC|PC|QU|SK|YT)\b' AND Prov = UPPER(Prov)),  -- YING LUO: ADD CHECK CONSTRAINT TO ALLOW UPPER ALPHABETS ONLY
	CONSTRAINT CK_HealthPlan_PostalCode CHECK (PostalCode LIKE '[A-Z][0-9][A-Z] [A-Z][0-9][A-Z]'),  -- Ying LUO: ADD CHECK CONSTRAINT TO MEET THE FORMAT
	CONSTRAINT CK_HealthPlan_Phone CHECK (Phone LIKE '((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}')
;
go


/***** Table No. 5 - Pharmacy.Clinic ****/
ALTER TABLE Pharmacy.Clinic
ADD
	CONSTRAINT DF_Clinic_Province DEFAULT ('QC') for Prov,
	CONSTRAINT CK_Clinic_Province CHECK (Prov LIKE '\b(?:AB|BC|CB|LB|MB|NF|NWT|ON|PE|QC|PC|QU|SK|YT)\b' AND Prov = UPPER(Prov)),  -- YING LUO: ADD CHECK CONSTRAINT TO ALLOW UPPER ALPHABETS ONLY
	CONSTRAINT CK_Clinic_PostalCode CHECK (PostalCode LIKE '[A-Z][0-9][A-Z][A-Z][0-9][A-Z]'),  -- Ying LUO: ADD CHECK CONSTRAINT TO MEET THE FORMAT
	CONSTRAINT CK_Clinic_Phone CHECK (Phone LIKE '((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}')
;
go

/***** Table No. 6 - Pharmacy.Drug ****/
ALTER TABLE Pharmacy.Drug
ADD
	CONSTRAINT CK_Drug_Price CHECK (Cost > 0),
	CONSTRAINT CK_Drug_Cost CHECK ( Price > 0),
	CONSTRAINT CK_Cost_Price CHECK (Cost <= Price)
;
go

CREATE INDEX ID_Name ON Pharmacy.Drug (Name);
GO

/***** Table No. 7 - Pharmacy.Doctor ****/
ALTER TABLE Pharmacy.Doctor
ADD
	CONSTRAINT FK_Doctor_Clinic FOREIGN KEY (ClinicID) REFERENCES Pharmacy.Clinic(ClinicID),
	CONSTRAINT CK_Doctor_Phone CHECK (Phone LIKE '((\(\d{3}\)?)|(\d{3}-))?\d{3}-\d{4}'),
	CONSTRAINT CK_Doctor_Cell CHECK (Cell LIKE '((\(\d{3}\)?)|(\d{3}-))?\d{3}-\d{4}')
;
go

/***** Table No. 8 - HumanResources.Employee ****/
ALTER TABLE HumanResources.Employee
ADD
	CONSTRAINT FK_Employee_JobTitle FOREIGN KEY (JobID) REFERENCES HumanResources.JobTitle(JobID),
	CONSTRAINT DF_Employee_Province DEFAULT ('QC') for Prov,
	CONSTRAINT CK_Employee_Province CHECK (Prov LIKE '\b(?:AB|BC|CB|LB|MB|NF|NWT|ON|PE|QC|PC|QU|SK|YT)\b' AND Prov = UPPER(Prov)),  -- YING LUO: ADD CHECK CONSTRAINT TO ALLOW UPPER ALPHABETS ONLY
	CONSTRAINT CK_Employee_PostalCode CHECK (PostalCode LIKE '[A-Z][0-9][A-Z] [A-Z][0-9][A-Z]'),  -- Ying LUO: ADD CHECK CONSTRAINT TO MEET THE FORMAT
	CONSTRAINT CK_Employee_Phone CHECK (Phone LIKE '((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}'),
	CONSTRAINT CK_Employee_SIN CHECK (SIN LIKE '[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9]'),
	CONSTRAINT CK_Employee_DOB CHECK (DOB LIKE '^\d{1,2}\/\d{1,2}\/\d{4}$'),
	CONSTRAINT CK_StartDate CHECK (StartDate LIKE '^\d{1,2}\/\d{1,2}\/\d{4}$'),
	CONSTRAINT CK_EndDate CHECK (EndDate LIKE '^\d{1,2}\/\d{1,2}\/\d{4}$'),
	CONSTRAINT CK_Review CHECK (Review LIKE '^\d{1,2}\/\d{1,2}\/\d{4}$'),
	CONSTRAINT CK_Employee_Cell CHECK (Cell LIKE '((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}'),
	CONSTRAINT CK_Employee_Salary CHECK (Salary >= 0),
	CONSTRAINT CK_Employee_HourlyRate CHECK (HourlyRate >= 0),
	CONSTRAINT CK_HireValid CHECK (EndDate > StartDate),
	CONSTRAINT CK_ReivewValid CHECK (Review > StartDate)
;
Go


/***** Table No. 9 - HumanResources.EmployeeTraining ****/
ALTER TABLE HumanResources.EmployeeTraining
ADD
	CONSTRAINT FK_EmployeeTraining_Class FOREIGN KEY (ClassID) REFERENCES HumanResources.Class(ClassID),
	CONSTRAINT FK_EmployeeTraining_Employee FOREIGN KEY (EmpID) REFERENCES HumanResources.Employee(EmpID)
;
go


/***** Table No. 10 - Sales.Customer ****/
ALTER TABLE Sales.Customer
ADD
	CONSTRAINT FK_Customer_HealthPlan FOREIGN KEY (PlanID) REFERENCES Sales.HealthPlan(PlanID),
	CONSTRAINT FK_Customer_HouseHold FOREIGN KEY (HouseID) REFERENCES Sales.HouseHold(HouseID),
	CONSTRAINT CK_Balance CHECK (Balance >= 0),
	CONSTRAINT CK_Customer_Phone CHECK (Phone LIKE '((\(\d{3}\)?)|(\d{3}-))?\d{3}-\d{4}'),
	CONSTRAINT CK_Customer_DOB CHECK (DOB LIKE '^\d{1,2}\/\d{1,2}\/\d{4}$')
;
go

CREATE INDEX ID_CustLast ON Sales.Customer (CustLast);
GO

/***** Table No. 11 - Pharmacy.Rx ****/
ALTER TABLE Pharmacy.Rx
ADD
	CONSTRAINT DF_Rx_AutoRefill DEFAULT (0) for AutoRefill,
	CONSTRAINT FK_Rx_Doctor FOREIGN KEY (DoctorID) REFERENCES Pharmacy.Doctor(DoctorID),
	CONSTRAINT FK_Rx_Customer FOREIGN KEY (CustID) REFERENCES Sales.Customer(CustID),
	CONSTRAINT FK_Rx_Drug FOREIGN KEY (DIN) REFERENCES Pharmacy.Drug(DIN),
	CONSTRAINT CK_Rx_ExpireValid CHECK (ExpireDate > Date),
	CONSTRAINT CK_Rx_Date CHECK (Date LIKE '^\d{1,2}\/\d{1,2}\/\d{4}$'),
	CONSTRAINT CK_Rx_ExpireDate CHECK (ExpireDate LIKE '^\d{1,2}\/\d{1,2}\/\d{4}$'),
	CONSTRAINT CK_Rx_Refill CHECK (RefillsUsed <= Refills)
;
go


/***** Table No. 12 - Pharmacy.Refill ****/
ALTER TABLE Pharmacy.Refill
ADD
	CONSTRAINT FK_Refill_Rx FOREIGN KEY (PrescriptionID) REFERENCES Pharmacy.Rx(PrescriptionID),
	CONSTRAINT FK_Refill_Employee FOREIGN KEY (EmpID) REFERENCES HumanResources.Employee(EmpID)
;
go
