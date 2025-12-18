-- ===================================
-- เช็คโครงสร้างตาราง Medicines
-- ===================================

-- ดูโครงสร้างตาราง
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Medicines'
ORDER BY ORDINAL_POSITION;
GO

-- ดูข้อมูลทั้งหมด
SELECT TOP 5 * FROM Medicines;
GO
