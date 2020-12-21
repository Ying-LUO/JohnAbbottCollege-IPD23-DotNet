/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 07, 2020
	Developed By: Ying LUO
*/

USE master;
GO

-- switch to the AragonPharmacy20V1 database
USE AragonPharmacy20V1
;
GO 

/********* PROCEDURE 1 - Pharmacy.TotalSalesUSP *******/

-- How many sales did the pharmacy do last year?
-- USING INPUT AND OUTPUT PARAMETER TO CALCULATE THE TOTAL SALES
-- TOTAL SALES = DRUGS (PRICE + FEE - COST) * ( QUANTITY OF DRUGS IN PRESCRIPTION + QUANTITY OF DRUGS OF REFILL )
-- CALCULATE BY YEAR
CREATE PROCEDURE Pharmacy.TotalSalesUSP
    -- DECLARE PARAMETERS
    @Year INT,
    @TotalSalesPerYear MONEY OUTPUT
AS
    SET NOCOUNT ON;
    SELECT [YEAR], SUM([TotalSales]) AS 'Total Sales'
    FROM
    -- UNION SALES FROM PRESCRIPTION AND REFILL
        (SELECT YEAR(PRX.Date) AS 'YEAR', SUM(CAST(PRX.Quantity * (PD.Price+PD.Fee-PD.Cost) AS MONEY)) AS 'TotalSales'
        FROM Pharmacy.Rx AS PRX
        INNER JOIN Pharmacy.Drug AS PD
        ON PD.DIN = PRX.DIN
        GROUP BY YEAR(PRX.Date)
    UNION
        SELECT YEAR(PR.RefillDate) AS 'YEAR', SUM(CAST(PRX.Quantity * (PD.Price+PD.Fee-PD.Cost) AS MONEY)) AS 'TotalSales'
        FROM Pharmacy.Refill AS PR
        INNER JOIN Pharmacy.Rx AS PRX
        ON PRX.PrescriptionID = PR.PrescriptionID
        INNER JOIN Pharmacy.Drug AS PD
        ON PD.DIN = PRX.DIN
        GROUP BY YEAR(PR.RefillDate)) AS T 
    WHERE [YEAR]=@Year
    GROUP BY [YEAR];
    --Populate the output variable @TotalSalesPerYear
    SET @TotalSalesPerYear = (
        SELECT SUM([TotalSales])
    FROM
        (SELECT YEAR(PRX.Date) AS 'YEAR', SUM(CAST(PRX.Quantity * (PD.Price+PD.Fee-PD.Cost) AS MONEY)) AS 'TotalSales'
        FROM Pharmacy.Rx AS PRX
        INNER JOIN Pharmacy.Drug AS PD
        ON PD.DIN = PRX.DIN
        GROUP BY YEAR(PRX.Date)
    UNION
        SELECT YEAR(PR.RefillDate) AS 'YEAR', SUM(CAST(PRX.Quantity * (PD.Price+PD.Fee-PD.Cost) AS MONEY)) AS 'TotalSales'
        FROM Pharmacy.Refill AS PR
        INNER JOIN Pharmacy.Rx AS PRX
        ON PRX.PrescriptionID = PR.PrescriptionID
        INNER JOIN Pharmacy.Drug AS PD
        ON PD.DIN = PRX.DIN
        GROUP BY YEAR(PR.RefillDate)) AS T 
    WHERE [YEAR]=@Year
    GROUP BY [YEAR]
    );
GO

--- EXECUTE PROCEDURE -----
--- Total Sales in Year 2016
DECLARE @TotalSales MONEY;
EXECUTE Pharmacy.TotalSalesUSP 2016, @TotalSales OUTPUT
BEGIN
---- YING LUO: PRINT STRING WILL BE SHOW IN MESSAGE SECTION BESIDE OF RESULTS SECTION
    PRINT 'The Total Sales Amount in Year 2016 is $'+ RTRIM(CAST(@TotalSales AS NVARCHAR(10)))+'.'                
END;
GO

/********* PROCEDURE 2 - Pharmacy.DrugPercentageUSP *******/

-- What percentage of the customer purchased at least one drug?
-- Count Drugs numbers by Customer first, then Count Customer numbers by Drug Numbers
CREATE PROCEDURE Pharmacy.CountCustomerNumbersByDrugNumbersUSP
AS
    SET NOCOUNT ON;
    SELECT DrugNumbers, CustomerNumbers, CAST(CustomerNumbers*100.0/sum(CustomerNumbers) OVER() AS NUMERIC(4,2)) AS 'Percentage of Total'
    FROM (SELECT COUNT(CustID) AS 'CustomerNumbers', [DrugNumbers]
    FROM (
        SELECT CustID, COUNT(Rx.DIN) AS 'DrugNumbers'
        FROM Pharmacy.RX
        GROUP BY CustID
    ) AS T
    GROUP BY DrugNumbers) AS TX
    group by CustomerNumbers, DrugNumbers;
GO

--- EXECUTE PROCEDURE -----
EXECUTE Pharmacy.CountCustomerNumbersByDrugNumbersUSP;
GO

------ YING LUO: For Practice Purpose ONLY ----------
--- STEP 1: Count how many customers in total who bought drugs by function return output value
---------Scalar-Valued Function----------
CREATE FUNCTION Pharmacy.TotalCustomerNumberWhoBoughtDrugsFn()
RETURNS INT
AS 
BEGIN
    DECLARE @TotalCustomer INT
    SELECT @TotalCustomer = SUM(Customers)
    FROM
    (SELECT COUNT(CustID) AS 'Customers', [Drugs]
        FROM (
            SELECT CustID, COUNT(Rx.DIN) AS 'Drugs'
            FROM Pharmacy.RX
            GROUP BY CustID
            ) AS T 
    GROUP BY Drugs) AS TX
    IF (@TotalCustomer IS NULL)   
        SET @TotalCustomer = 0;
    RETURN @TotalCustomer
END; 
GO

--- STEP 2: Show how many customers bought one drug or two drugs or three drugs, category by drugs numbers
CREATE FUNCTION Pharmacy.CustomerNumbersByDrugNumbersFn()
RETURNS @CountCustomer TABLE
(
    CustomerNumbers INT,
    DrugNumbers INT,
    TotalCustomerNumbers INT
)
AS
BEGIN
    INSERT INTO @CountCustomer
        SELECT COUNT(CustID) AS 'CustomerNumbers', [DrugNumbers], Pharmacy.TotalCustomerNumberWhoBoughtDrugsFn() AS 'TotalCustomerNumber'
        FROM (
                SELECT CustID, COUNT(Rx.DIN) AS 'DrugNumbers'
                FROM Pharmacy.RX
                GROUP BY CustID
                ) AS T 
        GROUP BY DrugNumbers
    RETURN
END;
GO

SELECT * FROM  Pharmacy.CustomerNumbersByDrugNumbersFn();
GO

/********* Function 3 - Pharmacy.GreatestNumberOfDrugsUSP *******/

-- What was the greatest number of drugs purchased by any one individual?
CREATE FUNCTION Pharmacy.GreatestNumberOfDrugsFn()
RETURNS INT
AS 
BEGIN
    DECLARE @GreatestNumberOfDrugs INT
    SELECT @GreatestNumberOfDrugs = MAX([DrugNumbers])
    FROM (
        SELECT CustID, COUNT(Rx.DIN) AS 'DrugNumbers'
        FROM Pharmacy.RX
        GROUP BY CustID
        ) AS T 
    IF (@GreatestNumberOfDrugs IS NULL)   
        SET @GreatestNumberOfDrugs = 0;
    RETURN @GreatestNumberOfDrugs
END; 
GO

--- EXECUTE PROCEDURE -----
DECLARE @GreatestNumber INT;
EXECUTE @GreatestNumber = Pharmacy.GreatestNumberOfDrugsFn;
BEGIN
    PRINT 'The Greatest Number of Drugs is '+ RTRIM(CAST(@GreatestNumber AS NVARCHAR(5))) + '.'  
END;
GO

/********* PROCEDURE 4 - Pharmacy.HealthPlanCustomerProportionUSP *******/

-- What proportion of the pharmacy customers are members of each health plan?

CREATE PROCEDURE Sales.HealthPlanCustomerProportionUSP
AS
    SET NOCOUNT ON;
    SELECT T.PlanID, SH.PlanName, T.CustomerNumbers, CAST(T.CustomerNumbers*100.0/SUM(T.CustomerNumbers) OVER() AS NUMERIC(4,2)) AS 'Percentage of Total'
    FROM(
            SELECT Count(CustID) AS 'CustomerNumbers', PlanID FROM Sales.Customer
            GROUP BY PlanID
        ) AS T
    INNER JOIN Sales.HealthPlan AS SH
    ON SH.PlanID = T.PlanID
    ORDER BY T.PlanID;
GO

--- EXECUTE PROCEDURE -----
EXECUTE Sales.HealthPlanCustomerProportionUSP;
GO
