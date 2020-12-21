/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 05, 2020
	Developed By: Ying LUO
*/

USE master;
GO

-- switch to the AragonPharmacy20V1 database
USE AragonPharmacy20V1
;
GO


/********* VIEW 1 - DuplicatePostalCodesView *******/

CREATE VIEW HumanResources.[DuplicatePostalCodesView]
AS
SELECT
    EmpID,
    CONCAT_WS(' ', EmpFirst, EmpMi, EmpLast) AS 'Employee Full Name',
    CONCAT_WS(', ', Address, City, Prov) AS 'Employee Full Address',
    FORMAT (StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    PostalCode,
    ROW_NUMBER() OVER (
                        PARTITION BY LEFT(PostalCode, 3)
                        ORDER BY LEFT(PostalCode, 3)
                        )
                        AS 'Carpool in Same Neighborhood',
    Phone,
    Cell
FROM HumanResources.Employee
;
GO

/********* VIEW 2 - NoTrainingView *******/

CREATE VIEW HumanResources.[NoTrainingView]
AS
SELECT
    HE.EmpID,
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    FORMAT(HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    HE.Phone,
    HE.Cell
FROM HumanResources.Employee AS HE 
    LEFT OUTER JOIN HumanResources.EmployeeTraining AS HET
    ON HET.EmpID = HE.EmpID
WHERE HET.EmpID is NULL 
;
GO

/********* VIEW 3 - EmployeeTrainingView *******/

CREATE VIEW HumanResources.[EmployeeTrainingView]
AS
SELECT
    HE.EmpID,
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    FORMAT(HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    HC.Description AS 'Attend Class Description',
    FORMAT (HET.Date, 'd', 'en-us') AS 'Attend Class Date',
    HE.Phone,
    HE.Cell
FROM HumanResources.Employee AS HE 
    LEFT OUTER JOIN HumanResources.EmployeeTraining AS HET ON HET.EmpID = HE.EmpID
    LEFT OUTER JOIN HumanResources.Class AS HC ON HC.ClassID = HET.ClassID
;
GO


/********* VIEW 4 - UpToDateView *******/

CREATE VIEW HumanResources.[UpToDateView]
AS
SELECT
    HE.EmpID,
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    FORMAT(HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    HC.Description AS 'Attend Class Description',
    FORMAT (HET.Date, 'd', 'en-us') AS 'Attend Class Date',
    DATEDIFF(YEAR, HET.Date, GETDATE()) AS 'Years After Class',
    HC.Renewal AS 'Renewal In Years',
    CASE 
        WHEN DATEDIFF(YEAR, HET.Date, GETDATE()) > HC.Renewal
               THEN 'Expired'
               ELSE 'Effective'
        END as 'Certification Status',
    HE.Phone,
    HE.Cell
FROM HumanResources.Employee AS HE 
    INNER JOIN HumanResources.EmployeeTraining AS HET ON HET.EmpID = HE.EmpID
    INNER JOIN HumanResources.Class AS HC ON HC.ClassID = HET.ClassID
WHERE DATEDIFF(YEAR, HET.Date, GETDATE()) > HC.Renewal
AND UPPER(HC.Description) LIKE UPPER('%CPR%') OR UPPER(HC.Description) LIKE UPPER('%defibrillator%')
;
GO

/********* VIEW 5 - Top5HourlyRatesView *******/

CREATE VIEW HumanResources.[Top5HourlyRatesView]
AS
SELECT
    TOP(5)HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HE.HourlyRate AS 'Hourly Rate',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT(HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    DATEDIFF(YEAR, HE.StartDate, GETDATE()) AS 'Years in Service',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status'
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE HE.Salary = 0                       -- YING LUO: NON-SALARIED EMPLOYEE
ORDER BY HE.HourlyRate DESC
;
GO

/********* VIEW 6 - MaxMinAvgHourlyRate *******/

CREATE VIEW HumanResources.[MaxMinAvgHourlyRate]
AS
SELECT
    HJ.JobID,
    HJ.Title,
    CAST(MIN(HE.HourlyRate) AS numeric(5,2)) AS 'Minimum Hourly Rate',
    CAST(AVG(HE.HourlyRate) AS numeric(5,2)) AS 'Average Hourly Rate',
    CAST(MAX(HE.HourlyRate) AS numeric(5,2)) AS 'Maximum Hourly Rate'
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE HE.Salary = 0                       -- YING LUO: NON-SALARIED EMPLOYEE
GROUP BY HJ.Title, HJ.JobID
ORDER BY HJ.JobID ASC offset 0 rows
;
GO

/********* VIEW 7 - EmployeeAgeView *******/

CREATE VIEW HumanResources.[EmployeeAgeView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.DOB, 'd', 'en-us') AS 'Birthday',
    CAST(DATEDIFF(MONTH, HE.DOB, GETDATE())/12.0 AS numeric(3,1)) AS 'Current Age(Desc)',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.Phone AS 'Phone(Asc)',
    HE.Cell
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
ORDER BY 'Current Age(Desc)' DESC offset 0 rows 
;
GO

/********* VIEW 8 - AvgEmployeeAgeView *******/

CREATE VIEW HumanResources.[AvgEmployeeAgeView]
AS
SELECT
    HJ.JobID,
    HJ.Title,
    CAST(AVG(DATEDIFF(MONTH, HE.DOB, GETDATE())/12.0) AS numeric(3,1)) AS 'Average Employee Age'
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
GROUP BY HJ.Title, HJ.JobID
ORDER BY HJ.JobID ASC offset 0 rows
;
GO