-- ============================================
-- Fix MedicalRecords Table - Add Missing Columns
-- ============================================
-- Purpose: Add IsActive and ChiefComplaint columns to MedicalRecords
-- Date: 2025-12-22

USE HospitalDB;
GO

PRINT 'Adding missing columns to MedicalRecords table...';

-- Add IsActive column if not exists
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MedicalRecords') AND name = 'IsActive')
BEGIN
    ALTER TABLE MedicalRecords
    ADD IsActive BIT NOT NULL DEFAULT 1;
    PRINT '✓ IsActive column added';
END
ELSE
    PRINT '- IsActive column already exists';

-- Add ChiefComplaint column if not exists
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('MedicalRecords') AND name = 'ChiefComplaint')
BEGIN
    ALTER TABLE MedicalRecords
    ADD ChiefComplaint NVARCHAR(500) NULL;
    PRINT '✓ ChiefComplaint column added';
END
ELSE
    PRINT '- ChiefComplaint column already exists';

GO

PRINT '';
PRINT '✓ MedicalRecords table schema updated successfully!';
GO
