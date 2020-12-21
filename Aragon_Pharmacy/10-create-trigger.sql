/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 08, 2020
	Developed By: Ying LUO -- based on the original script provided by Dr. Daou
*/

USE master;
GO

-- switch to the AragonPharmacy20V1 database
USE AragonPharmacy20V1
;
GO 

/********* TRIGGER 1  Pharmacy.OverExpiredDate *******/

-- CHECK IF REFILL DATE OVER THAN PRESCRIPTION EXPIRED DATE
CREATE TRIGGER Pharmacy.OverExpiredDate ON Pharmacy.Refill
AFTER INSERT, UPDATE
AS  
IF (ROWCOUNT_BIG() = 0)
RETURN;
IF EXISTS (SELECT * 
            FROM Pharmacy.Refill AS PR
            INNER JOIN Pharmacy.Rx AS PRX
            ON  PRX.PrescriptionID = PR.PrescriptionID
            WHERE PR.RefillDate > PRX.ExpireDate
        )  
BEGIN  
    RAISERROR('Refill date should be within the prescription expired date', 10, 1);  
ROLLBACK TRANSACTION;  
RETURN   
END;  
GO  

-- VERIFY IF TRIGGER WORKS BY INSERT A NEW DATA WITH OVER EXPIRED DATE
INSERT INTO Pharmacy.Refill(PrescriptionID, RefillDate, EmpID)
VALUES(1, '11/01/2017', 8);
GO

-- VERIFY IF TRIGGER WORKS BY UPDATE A RECORD TO OVER EXPIRED DATE
UPDATE Pharmacy.Refill  
SET RefillDate = '10/01/2017'  
WHERE PrescriptionID=1;  
GO


/********* TRIGGER 2  Pharmacy.OverRefillTimes *******/

-- CHECK IF REFILL DATE OVER THAN PRESCRIPTION EXPIRED DATE
CREATE TRIGGER Pharmacy.OverRefillTimes ON Pharmacy.Refill
AFTER INSERT
AS  
BEGIN  
    DECLARE @RefillTimes INT, @RefillUsed INT;
    SELECT @RefillTimes = COUNT(PR.PrescriptionID), @RefillUsed = PRX.Refills
    FROM Pharmacy.Refill AS PR
    INNER JOIN Pharmacy.Rx AS PRX
    ON  PRX.PrescriptionID = PR.PrescriptionID
    GROUP BY PRX.PrescriptionID, PRX.Refills;
    IF @RefillTimes > @RefillUsed
    PRINT 'Over refill times, you cannot refill this prescription any more';     --- YING LUO: MESSAGE CONTENT SEEMS NOT CHANGED IF HAPPENED IN THE SAME TABLE
ROLLBACK TRANSACTION;  
RETURN   
END;  
GO  

-- VERIFY IF TRIGGER WORKS BY INSERT A NEW DATA WITH OVER REFILL TIMES
INSERT INTO Pharmacy.Refill(PrescriptionID, RefillDate, EmpID)
VALUES(50, '05/01/2017', 5);
GO
