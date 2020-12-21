/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 06, 2020
	Developed By: Ying LUO
*/

USE master;
GO

-- switch to the AragonPharmacy20V1 database
USE AragonPharmacy20V1
;
GO 

/********* FUNCTION 1 - ClassCostAnalysisFn *******/

---------Table-Valued Function----------
CREATE FUNCTION HumanResources.ClassCostAnalysisFn()
RETURNS @AnnualCost TABLE   
(
    ClassID INT,
    Description NVARCHAR(100),
    AnnualCost MONEY,
    Year INT
)
AS
BEGIN
    INSERT INTO @AnnualCost
    SELECT
        HC.ClassID,
        HC.Description,
        SUM(HC.Cost) AS 'Annual Cost',
        YEAR(HET.Date) AS 'Year of Class'
    FROM HumanResources.Class AS HC
        LEFT OUTER JOIN HumanResources.EmployeeTraining AS HET ON HC.ClassID = HET.ClassID
        LEFT OUTER JOIN HumanResources.Employee AS HE ON HET.EmpID = HE.EmpID
    GROUP BY HC.ClassID, HC.Description, YEAR(HET.Date)
    ORDER BY HC.ClassID offset 0 rows;
    RETURN;
END;
GO

---------Execute Table-Valued Function----------------
SELECT * FROM HumanResources.ClassCostAnalysisFn();
GO

---------Scalar-Valued Function----------
CREATE FUNCTION HumanResources.ClassCostAnalysisByYearAndClassFn
(
    @Year AS INT,
    @Class AS INT
)  
RETURNS MONEY
AS 
BEGIN
    DECLARE @AnnualCost MONEY
    SELECT @AnnualCost = SUM(HC.Cost)
    FROM HumanResources.Class AS HC
        LEFT OUTER JOIN HumanResources.EmployeeTraining AS HET ON HC.ClassID = HET.ClassID
        LEFT OUTER JOIN HumanResources.Employee AS HE ON HET.EmpID = HE.EmpID
    WHERE YEAR(HET.Date) = @Year AND HC.ClassID = @Class
    GROUP BY HC.ClassID, HC.Description, YEAR(HET.Date)
    ORDER BY HC.ClassID offset 0 rows;
    IF (@AnnualCost IS NULL)   
        SET @AnnualCost = 0;
    RETURN @AnnualCost
END; 
GO

---------Execute Scalar-Valued Function----------------
SELECT DISTINCT HC.ClassID, HC.Description, HumanResources.ClassCostAnalysisByYearAndClassFn(YEAR(HET.Date), HC.ClassID) AS 'Annual Cost', YEAR(HET.Date) AS 'Year'
FROM HumanResources.Class AS HC
    LEFT OUTER JOIN HumanResources.EmployeeTraining AS HET ON HC.ClassID = HET.ClassID
ORDER BY HC.ClassID, [Year] ASC;
GO


/********* FUNCTION 2 - EmpHistoryFn *******/

---------Table-Valued Function----------
CREATE FUNCTION HumanResources.EmpHistoryFn()
RETURNS TABLE
AS
RETURN
(
    SELECT
        *
    FROM HumanResources.Employee
    WHERE EndDate IS NOT NULL
);
GO

---------Execute Table-Valued Function----------------
SELECT IDENTITY(INT,1,1) AS EHID, -- YING LUO: CHANGE EMPID INTO NEW INDEX EHID
        EmpFirst, EmpMi, EmpLast, SIN, DOB, StartDate, EndDate, Address, City, Prov, PostalCode, JobID, Memo, Phone, Cell, Salary, HourlyRate, Review 
INTO HumanResources.EmployeeHistory
FROM HumanResources.EmpHistoryFn()
;
GO

SELECT * FROM HumanResources.EmployeeHistory;
GO


/********* FUNCTION 3 - EmployeeTrainingHistoryFn *******/

---------Table-Valued Function----------
CREATE FUNCTION HumanResources.EmployeeTrainingHistoryFn (@ExpireDate DATETIME)
RETURNS TABLE
AS
RETURN
(
    SELECT
        HE.EmpID,
        CONCAT_WS(' ', HE.EmpFirst, ISNULL(HE.EmpMi, ''), HE.EmpLast) AS 'Employee Full Name',
        HC.ClassID,
        HC.Description AS 'Attend Class Description',
        FORMAT (HET.Date, 'd', 'en-us') AS 'Attend Class Date'
    FROM HumanResources.Employee AS HE 
        INNER JOIN HumanResources.EmployeeTraining AS HET ON HET.EmpID = HE.EmpID
        INNER JOIN HumanResources.Class AS HC ON HC.ClassID = HET.ClassID
    WHERE HET.Date <= @ExpireDate   -- YING LUO: ONLY CHECK DATE WITHOUT CHECK THE CLASS NAME
);
GO

---------Execute Table-Valued Function----------------
SELECT *
INTO HumanResources.EmployeeTrainingHistory
FROM HumanResources.EmployeeTrainingHistoryFn('01/01/2017')
;
GO

SELECT * FROM HumanResources.EmployeeTrainingHistory;
GO

/********* FUNCTION 4 - ObsoleteClassesFn *******/

---------Table-Valued Function----------
CREATE FUNCTION HumanResources.ObsoleteClassesFn
(
    @ExpireDate as DATETIME,
    @Class as NVARCHAR(100)   --YING LUO: CHECK DATE AS WELL AS CLASS NAME
)
RETURNS @Obsolete TABLE
(
    EmpID INT,
    EmployeeFullName NVARCHAR(62),
    ClassID INT,
    Description NVARCHAR(100),
    Date DATETIME
)
AS
BEGIN
    INSERT INTO @Obsolete
        SELECT
        HE.EmpID,
        CONCAT_WS(' ', HE.EmpFirst, ISNULL(HE.EmpMi, ''), HE.EmpLast) AS 'Employee Full Name',
        HC.ClassID,
        HC.Description AS 'Attend Class Description',
        FORMAT (HET.Date, 'd', 'en-us') AS 'Attend Class Date'
        FROM HumanResources.Employee AS HE 
            INNER JOIN HumanResources.EmployeeTraining AS HET ON HET.EmpID = HE.EmpID
            INNER JOIN HumanResources.Class AS HC ON HC.ClassID = HET.ClassID
        WHERE UPPER(HC.Description) LIKE '%'+@Class+'%'
        AND HET.Date <= @ExpireDate
    RETURN
END;
GO

---------Execute Table-Valued Function----------------
SELECT * FROM HumanResources.ObsoleteClassesFn('01/01/2017', 'CPR');
GO

SELECT * FROM HumanResources.ObsoleteClassesFn('01/01/2017', 'Defibrillator');
GO

/********* FUNCTION 5 - DeleteClassesView *******/
CREATE FUNCTION HumanResources.DeleteClassesView (@ExpireDate DATETIME)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HumanResources.EmployeeTraining
    WHERE DATE <= @ExpireDate
);
GO

---------Execute Table-Valued Function----------------
SELECT * FROM HumanResources.DeleteClassesView('01/01/2017');   -- YING LUO: ONLY SELECT OUT FOR VIEW BEFORE DELETE 
GO

/********* FUNCTION 6 - TechnicianRaiseFn *******/

CREATE FUNCTION HumanResources.TechnicianRaiseFn
(
    @Title NVARCHAR(30),
    @Rate NUMERIC(3,2)
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        HE.EmpID AS 'Employee ID',
        CONCAT_WS(' ', HE.EmpFirst, ISNULL(HE.EmpMi, ''), HE.EmpLast) AS 'Employee Full Name',
        HJ.Title AS 'Job Title',
        HE.SIN AS 'SIN Number',
        FORMAT (HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
        CASE WHEN HE.EndDate IS NULL THEN 'In service'
            ELSE 'Former Employee'
        END AS 'Status',
        CAST(HE.HourlyRate AS DECIMAL(6,2)) AS 'Before Raise Rate',
        CAST(HE.HourlyRate*(@Rate+1) AS DECIMAL(6,2)) AS 'After Raise Rate'
    FROM HumanResources.Employee AS HE
        INNER JOIN HumanResources.JobTitle AS HJ
            ON HJ.JobID = HE.JobID
    WHERE HE.EndDate IS NULL
    AND UPPER(HJ.Title) LIKE UPPER('%'+@Title+'%')
);
GO

---------Execute Table-Valued Function----------------
SELECT * FROM HumanResources.TechnicianRaiseFn('Technician', 0.05);  
GO

/********* FUNCTION 7 - RetirementView *******/

CREATE FUNCTION HumanResources.RetirementView
(
    @Year INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        HE.EmpID AS 'Employee ID',
        CONCAT_WS(' ', HE.EmpFirst, ISNULL(HE.EmpMi, ''), HE.EmpLast) AS 'Employee Full Name',
        HE.HourlyRate AS 'Hourly Rate',
        HJ.Title AS 'Job Title',
        HE.SIN AS 'SIN Number',
        FORMAT(HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
        ABS(DATEDIFF(YEAR, HE.StartDate, GETDATE())) AS 'Years in Service',
        CASE WHEN ABS(DATEDIFF(YEAR, HE.StartDate, GETDATE())) >@Year THEN 'Eligible'
            ELSE 'Not Eligible'
        END AS '401K-Retirement'
    FROM HumanResources.Employee AS HE
        INNER JOIN HumanResources.JobTitle AS HJ
            ON HJ.JobID = HE.JobID
    WHERE HE.EndDate IS NULL
);
GO

---------Execute Table-Valued Function----------------
SELECT * FROM HumanResources.RetirementView(1);  -- SET YEAR EQUAL TO ONE
GO

/********* FUNCTION 8 - Top3SalariesFn *******/
CREATE FUNCTION HumanResources.Top3SalariesFn
(
    @Top INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        TOP(@Top)HE.EmpID AS 'Employee ID',
        CONCAT_WS(' ', HE.EmpFirst, ISNULL(HE.EmpMi, ''), HE.EmpLast) AS 'Employee Full Name',
        '$'+HE.Salary AS 'Salary',
        HJ.Title AS 'Job Title',
        HE.SIN AS 'SIN Number',
        FORMAT(HE.StartDate, 'd', 'en-us') AS 'Start Working Date',
        ABS(DATEDIFF(YEAR, HE.StartDate, GETDATE())) AS 'Years in Service'
    FROM HumanResources.Employee AS HE
        INNER JOIN HumanResources.JobTitle AS HJ
            ON HJ.JobID = HE.JobID
    WHERE HE.HourlyRate = 0 AND HE.EndDate IS NULL                     -- YING LUO: SALARIED IN SERVICE EMPLOYEE
    ORDER BY HE.Salary DESC
);
GO

---------Execute Table-Valued Function----------------
SELECT * FROM HumanResources.Top3SalariesFn(3);  -- SET YEAR EQUAL TO ONE
GO