-- ============================================
-- Add Cost Price and Selling Price to Medicine Inventory
-- ============================================
-- Purpose: Add separate columns for purchase cost and selling price
-- Author: Hospital Management System
-- Date: 2025-12-22

USE HospitalDB;
GO

-- ============================================
-- Step 1: Add new columns
-- ============================================
PRINT 'Adding CostPrice and SellingPrice columns...';

ALTER TABLE Medicines
ADD CostPrice DECIMAL(10,2) NULL,
    SellingPrice DECIMAL(10,2) NULL;
GO

PRINT '✓ Columns added successfully!';
GO

-- ============================================
-- Step 2: Migrate existing data
-- ============================================
PRINT '';
PRINT 'Migrating existing UnitPrice data...';

-- Copy UnitPrice to SellingPrice
-- Estimate CostPrice as 70% of SellingPrice (you can adjust this)
UPDATE Medicines
SET SellingPrice = ISNULL(UnitPrice, 0),
    CostPrice = ISNULL(UnitPrice, 0) * 0.70
WHERE SellingPrice IS NULL;
GO

PRINT '✓ Data migrated successfully!';
PRINT '  - SellingPrice = UnitPrice';
PRINT '  - CostPrice = UnitPrice * 0.70 (estimated)';
GO

-- ============================================
-- Step 3: Add constraints (optional but recommended)
-- ============================================
PRINT '';
PRINT 'Adding constraints...';

-- Ensure prices are not negative
ALTER TABLE Medicines
ADD CONSTRAINT CK_Medicines_CostPrice CHECK (CostPrice >= 0);

ALTER TABLE Medicines
ADD CONSTRAINT CK_Medicines_SellingPrice CHECK (SellingPrice >= 0);
GO

PRINT '✓ Constraints added successfully!';
GO

-- ============================================
-- Step 4: (Optional) Drop old UnitPrice column
-- ============================================
-- UNCOMMENT BELOW IF YOU WANT TO REMOVE UnitPrice COLUMN
-- WARNING: This will permanently delete the UnitPrice column!

/*
PRINT '';
PRINT 'Dropping old UnitPrice column...';

ALTER TABLE Medicines
DROP COLUMN UnitPrice;
GO

PRINT '✓ UnitPrice column removed!';
*/

-- ============================================
-- Step 5: Show sample data
-- ============================================
PRINT '';
PRINT 'Sample data after migration:';
GO

SELECT TOP 5
    MedicineName,
    CostPrice,
    SellingPrice,
    (SellingPrice - CostPrice) AS Profit,
    CASE 
        WHEN SellingPrice > 0 
        THEN CAST(((SellingPrice - CostPrice) / SellingPrice * 100) AS DECIMAL(5,2))
        ELSE 0 
    END AS MarginPercent
FROM Medicines
WHERE IsActive = 1
ORDER BY MedicineName;
GO

PRINT '';
PRINT '✓ Medicine Inventory pricing structure updated successfully!';
PRINT '';
PRINT 'IMPORTANT NOTES:';
PRINT '1. CostPrice has been estimated as 70% of the original UnitPrice';
PRINT '2. Please update the actual CostPrice values based on your purchase records';
PRINT '3. UnitPrice column is still available (not dropped) for backward compatibility';
PRINT '4. Uncomment Step 4 in this script if you want to remove UnitPrice column';
GO
