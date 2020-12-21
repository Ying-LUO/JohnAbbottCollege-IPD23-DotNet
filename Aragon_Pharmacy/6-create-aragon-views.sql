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

/********* VIEW 1 - PharmacistListView *******/

CREATE VIEW HumanResources.[PharmacistListView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.Salary
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE UPPER(HJ.Title) LIKE UPPER('%Pharmacist%')
;
GO

/********* VIEW 2 - EmployeeListView *******/

CREATE VIEW HumanResources.[EmployeeListView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.Salary
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE UPPER(HJ.Title) LIKE UPPER('%Pharmacist%')
OR UPPER(HJ.Title) LIKE UPPER('%Owner%')
OR UPPER(HJ.Title) LIKE UPPER('%Manager%')
;
GO

/********* VIEW 3 - FirstEmployeeHiredListView *******/

CREATE VIEW HumanResources.[FirstEmployeeHiredListView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    'First Hired Employee' AS 'Type',
    HE.Salary,
    HE.HourlyRate
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE HE.StartDate = (SELECT MIN(StartDate) FROM HumanResources.Employee)
UNION
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    'Recent Hired Employee' AS 'Type',
    HE.Salary,
    HE.HourlyRate
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE HE.StartDate = (SELECT MAX(StartDate) FROM HumanResources.Employee)
;
GO

/********* VIEW 4 - EmployeePhoneListView *******/

CREATE VIEW HumanResources.[EmployeePhoneListView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.Phone AS 'Phone(Asc)',
    HE.Cell
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
ORDER BY HE.Phone ASC offset 0 rows   -- YING LUO: NOT SURE ABOUT THE INDEX COLUMN
;
GO

/********* VIEW 5 - EmployeeHourlyRateView *******/

CREATE VIEW HumanResources.[EmployeeHourlyRateView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.HourlyRate AS 'Hourly Rate(Desc)'
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE HJ.Title LIKE '%Cashier%' 
OR HJ.Title LIKE '%Technician%'         -- YING LUO: RESTRICTED BY TITLE
AND HE.Salary = 0                       -- YING LUO: NON-SALARIED EMPLOYEE
ORDER BY HE.HourlyRate DESC offset 0 rows
;
GO

/********* VIEW 6 - HourlyRateAnalysisView *******/

CREATE VIEW HumanResources.[HourlyRateAnalysisView]
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


/********* VIEW 7 - SpeakSpanishView *******/

CREATE VIEW HumanResources.[SpeakSpanishView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.DOB, 'd', 'en-us') AS 'Birthday',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.Phone,
    HE.Cell,
    HE.Memo
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE UPPER(HE.Memo) LIKE UPPER('%Spanish%')
;
GO

/********* VIEW 8 - ReprimandListView *******/

CREATE VIEW HumanResources.[ReprimandListView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.DOB, 'd', 'en-us') AS 'Birthday',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.Phone,
    HE.Cell,
    HE.Memo
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
WHERE UPPER(HE.Memo) LIKE UPPER('%Reprimand%')
;
GO

/********* VIEW 9 - StartDateListView *******/

CREATE VIEW HumanResources.[StartDateListView]
AS
SELECT
    HE.EmpID AS 'Employee ID',
    CONCAT_WS(' ', HE.EmpFirst, HE.EmpMi, HE.EmpLast) AS 'Employee Full Name',
    HJ.Title AS 'Job Title',
    HE.SIN AS 'SIN Number',
    FORMAT (HE.DOB, 'd', 'en-us') AS 'Birthday',
    FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date(Desc)',
    CASE WHEN HE.EndDate IS NULL THEN 'In service'
        ELSE 'Former Employee'
    END AS 'Status',
    HE.Phone,
    HE.Cell,
    HE.Memo
FROM HumanResources.Employee AS HE
	INNER JOIN HumanResources.JobTitle AS HJ
		ON HJ.JobID = HE.JobID
--WHERE HE.StartDate BETWEEN CONVERT(datetime, '01/01/2012') AND CONVERT(datetime, '01/01/2020')
WHERE HE.StartDate BETWEEN '01/01/2011' AND '01/01/2020'   -- YING LUO: CHANGE THE DATE RANGE TO 2011 OTHERWISE NO RECORDS OUT
ORDER BY HE.StartDate DESC offset 0 rows
;
GO