/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 04, 2020
	Developed By: Ying LUO
*/

/****** BULK INSERT INTO AZURE DATA STUDIO BY 
        Importing data from a file in Azure blob storage
        referenced by information :
        https://www.sqlshack.com/use-bulk-insert-import-data-locally-azure/
        'Change Access Level' to blob before import to avoid no access permission
*****/

CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Test1!Insert';

CREATE DATABASE SCOPED CREDENTIAL MyAzurecredCredential
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 
'LTfK8IQKm60OWLNYiFyQ9mu+Bky2qe+n2BGi0dIrmbDw5I0qRvNymhL5n+N19fMprin8SVO6rA1liNOCm/aPGg==';

CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH 
(
    TYPE = BLOB_STORAGE,
    LOCATION = 'https://bulkinsertstorageaccount.blob.core.windows.net/bulkinsertcontainer1',
    CREDENTIAL = MyAzurecredCredential
);

USE master;
GO

-- switch to the AragonPharmacy20V1 database
USE AragonPharmacy20V1
;
GO


/***** Table No. 1 - HumanResources.JobTitle ****/
BULK INSERT HumanResources.JobTitle
FROM 'JobTitle.txt'
WITH
( 
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 2 - HumanResources.Class ****/
BULK INSERT HumanResources.Class
FROM 'Class.txt'
WITH
( 
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 3 - Sales.HouseHold ****/
BULK INSERT Sales.Household
FROM 'Household.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO


/***** Table No. 4 - Sales.HealthPlan ****/
BULK INSERT Sales.HealthPlan
FROM 'HealthPlan.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 5 - Pharmacy.Clinic ****/
BULK INSERT Pharmacy.Clinic
FROM 'Clinic.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 6 - Pharmacy.Drug ****/
BULK INSERT Pharmacy.Drug
FROM 'Drug.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 7 - Pharmacy.Doctor ****/
BULK INSERT Pharmacy.Doctor
FROM 'Doctor.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 8 - HumanResources.Employee ****/
BULK INSERT HumanResources.Employee
FROM 'Employee.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO


/***** Table No. 9 - HumanResources.EmployeeTraining ****/
BULK INSERT HumanResources.EmployeeTraining
FROM 'EmployeeTraining.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO


/***** Table No. 10 - Sales.Customer ****/
BULK INSERT Sales.Customer
FROM 'Customer.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 11 - Pharmacy.Rx ****/
BULK INSERT Pharmacy.Rx
FROM 'Rx.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO

/***** Table No. 12 - Pharmacy.Refill ****/
BULK INSERT Pharmacy.Refill
FROM 'Refill.txt'
WITH
(   
    DATA_SOURCE = 'MyAzureStorage',
    FIRSTROW = 2,
    ROWTERMINATOR ='\n',
    FIELDTERMINATOR =','
);
GO


SELECT * FROM HumanResources.JobTitle;
GO

SELECT * FROM HumanResources.Class;
GO

SELECT * FROM Sales.Household;
GO

SELECT * FROM Sales.HealthPlan;
GO

SELECT * FROM Pharmacy.Clinic;
GO

SELECT * FROM Pharmacy.Drug;
GO

SELECT * FROM Pharmacy.Doctor;
GO

SELECT * FROM HumanResources.Employee;
GO

SELECT * FROM HumanResources.EmployeeTraining;
GO

SELECT * FROM Sales.Customer;
GO

SELECT * FROM Pharmacy.Rx;
GO

SELECT * FROM Pharmacy.Refill;       
GO


-- YING LUO: INCONSISTANT DATA BETWEEN TABLE RX AND REFILL
/* E.G. 
    PrescriptionID=2 in Table Refill has 2 records in diff. refilled date
    But
    In table Rx, RefillUsed field value=1 when PrescriptionID=2
*/

---Refill Times Count

SELECT PrescriptionID, COUNT(PrescriptionID) AS 'COUNT' 
FROM Pharmacy.Refill
GROUP BY PrescriptionID;       
GO

---CHECK 
--            1) IF Prescription Refill within authorized times
--            2) IF last Refill date within expired date
--            3) IF first Refill date early than start date

SELECT PR.PrescriptionID, COUNT(PR.PrescriptionID) AS 'Refill COUNT', PRX.RefillsUsed, 
        PRX.Refills, PRX.AutoRefill, PRX.DIN, 
        MIN(PR.RefillDate) AS 'First Refill Date', PRX.Date, MAX(PR.RefillDate) AS 'Last Refill Date', PRX.ExpireDate,
        IIF( MAX(PR.RefillDate) > PRX.ExpireDate, 'TRUE', 'FALSE' ) AS 'Out of ExpireDate',
        IIF( MIN(PR.RefillDate) < PRX.Date, 'TRUE', 'FALSE' ) AS 'Early than Create'
FROM Pharmacy.Refill AS PR
INNER JOIN Pharmacy.Rx AS PRX
ON  PRX.PrescriptionID = PR.PrescriptionID
GROUP BY PR.PrescriptionID, PRX.DIN, PRX.Date, PRX.ExpireDate, PRX.Refills, PRX.AutoRefill, PRX.RefillsUsed
;
GO


---COUNT PRESCRIPTION AND REFILL BY YEAR

SELECT PRX.PrescriptionID, PRX.DIN, PRX.Quantity, PRX.Date, PD.Price, CAST(PRX.Quantity * PD.Price AS MONEY) AS 'Sales'
FROM Pharmacy.Rx AS PRX
INNER JOIN Pharmacy.Drug AS PD
ON PD.DIN = PRX.DIN
WHERE YEAR(Date) = '2016'
UNION
SELECT PRX.PrescriptionID, PRX.DIN, PRX.Quantity, PR.RefillDate, PD.Price, CAST(PRX.Quantity * PD.Price AS MONEY) AS 'Sales'
FROM Pharmacy.Rx AS PRX
LEFT OUTER JOIN Pharmacy.Refill AS PR
ON  PR.PrescriptionID = PRX.PrescriptionID
INNER JOIN Pharmacy.Drug AS PD
ON PD.DIN = PRX.DIN
WHERE YEAR(PR.RefillDate) = '2016'
;
GO

---COUNT PERCENTAGE OF Customers by buying numbers of drugs

SELECT Drugs, Customers, CAST(Customers*100.0/sum(Customers) OVER() AS NUMERIC(4,2)) AS 'Percentage of Total'
FROM (SELECT COUNT(CustID) AS 'Customers', [Drugs]
FROM (
    SELECT CustID, COUNT(Rx.DIN) AS 'Drugs'
    FROM Pharmacy.RX
    GROUP BY CustID
) AS T
GROUP BY Drugs) AS TX
group by Customers, Drugs
;
GO