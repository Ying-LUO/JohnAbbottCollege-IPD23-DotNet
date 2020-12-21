/* Purpose: create the database AragonPharmacy20V1
	Script Date: November 09, 2020
	Developed By: Ying LUO
*/

USE master;
GO

-- switch to the AragonPharmacy20V1 database
USE AragonPharmacy20V1
;
GO 

/********* CREATE TEST USER-DEFINED STORE PROCEDURE TO VERIFY THE TRANSACTIONS **********/

CREATE PROCEDURE TransactionTestUSP
    -- DECLARE PARAMETERS
    @DIN NCHAR(8),
	@Quantity NUMERIC(5,2),
	@Unit NVARCHAR(10),
	@Date DATETIME,
	@ExpireDate DATETIME,
	@Refills SMALLINT,
	@AutoRefill BIT,
	@RefillsUsed SMALLINT,
	@Instructions NVARCHAR(50),
	@CustID int,
	@DoctorID INT,
    @OldPrescriptionID INT  
AS  
DECLARE @INSERR INT  
DECLARE @DELERR INT  
DECLARE @MAXERR INT  
DECLARE @TranCounter INT;  
    SET @TranCounter = @@TRANCOUNT;   ---Returns the number of BEGIN TRANSACTION statements that have occurred on the current connection.
    SET @MAXERR = 0  
    IF @TranCounter > 0   
        SAVE TRANSACTION ProcedureSave;  
    ELSE  
        BEGIN TRANSACTION;

-- Add a Prescription  
INSERT INTO Pharmacy.Rx (DIN, Quantity, Unit, Date, ExpireDate, Refills, AutoRefill, RefillsUsed, Instructions, CustID, DoctorID)  
VALUES(@DIN, @Quantity, @Unit, @Date, @ExpireDate, @Refills, @AutoRefill, @RefillsUsed, @Instructions, @CustID, @DoctorID)  
  
-- Save error number returned from Insert statement  
SET @INSERR = @@error     ---Returns the error number for the last Transact-SQL statement executed.
IF @INSERR > @MAXERR  
SET @MAXERR = @INSERR  
  
-- Delete a Prescription  
DELETE FROM Pharmacy.Rx  
WHERE PrescriptionID = @OldPrescriptionID  

-- Save error number returned from Delete statement  
SET @DELERR = @@error  
IF @DELERR > @MAXERR  
SET @MAXERR = @DELERR  

--IF @TranCounter = 0    
--COMMIT TRANSACTION;  

IF @MAXERR <> 0  
BEGIN  
    ROLLBACK  
    PRINT 'Transaction rolled back'  
END  
ELSE  
    BEGIN  
        COMMIT  
        PRINT 'Transaction committed'  
    END  
PRINT 'INSERT error number:'+ CAST(@INSERR AS NVARCHAR(8))  
PRINT 'DELETE error number:'+ CAST(@DELERR AS NVARCHAR(8))  
RETURN @MAXERR
GO

-- EXCUTE PROCEDURE
EXECUTE TransactionTestUSP '00094684',	'1.00',	'mg', '9/10/2020', '6/10/2021', '2', '1',	'0','1 pill every 6 hours', '16', '13', '1'

SELECT * FROM Pharmacy.RX;


/*******    CHECK TRANSACTION RECORDS   ********/
SELECT
    [Current LSN],
    [Transaction ID],
    [Operation],
    [Transaction Name],
    [CONTEXT],
    [AllocUnitName],
    [Page ID],
    [Slot ID],
    [Begin Time],
    [End Time],
    [Number of Locks],
    [Lock Information]
FROM sys.fn_dblog(NULL,NULL)
WHERE Operation IN 
   ('LOP_INSERT_ROWS','LOP_MODIFY_ROW','LOP_DELETE_ROWS','LOP_BEGIN_XACT','LOP_COMMIT_XACT')  
AND [Begin Time]>'2020/11/09'
AND [Transaction Name] LIKE '%Transaction%'

--Get what all steps SQL Server performs during a single Page Split occurrence.
SELECT 
 [Current LSN],
 [Transaction ID],
 [Operation],
  [Transaction Name],
 [CONTEXT],
 [AllocUnitName],
 [Page ID],
 [Slot ID],
 [Begin Time],
 [End Time],
 [Number of Locks],
 [Lock Information]
FROM sys.fn_dblog(NULL,NULL)
WHERE [Transaction ID]='0000:00000f70' 
