-- ===================================
-- สร้างตาราง Medicines
-- ===================================

-- ลบตารางเก่าถ้ามี
IF OBJECT_ID('Medicines', 'U') IS NOT NULL
    DROP TABLE Medicines;
GO

-- สร้างตารางใหม่
CREATE TABLE Medicines (
    MedicineID INT PRIMARY KEY IDENTITY(1,1),
    MedicineCode NVARCHAR(20) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    Manufacturer NVARCHAR(200),
    UnitPrice DECIMAL(10,2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    ReorderLevel INT NOT NULL DEFAULT 10,
    Description NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME
);
GO

-- เพิ่มข้อมูลตัวอย่าง
INSERT INTO Medicines (MedicineCode, Name, Category, Manufacturer, UnitPrice, StockQuantity, ReorderLevel, Description, IsActive, CreatedDate)
VALUES 
('M0000001', 'Paracetamol 500mg', 'Painkiller', 'GPO', 5.00, 500, 50, 'Pain relief and fever reducer', 1, GETDATE()),
('M0000002', 'Amoxicillin 500mg', 'Antibiotic', 'Pfizer', 15.00, 250, 30, 'Antibiotic for bacterial infections', 1, GETDATE()),
('M0000003', 'Ibuprofen 400mg', 'Painkiller', 'GSK', 8.00, 300, 40, 'Anti-inflammatory pain reliever', 1, GETDATE()),
('M0000004', 'Vitamin C 1000mg', 'Vitamin', 'Blackmores', 12.00, 150, 20, 'Immune system support', 1, GETDATE()),
('M0000005', 'Omeprazole 20mg', 'Other', 'AstraZeneca', 20.00, 100, 15, 'Reduces stomach acid', 1, GETDATE());
GO

-- แสดงข้อมูลทั้งหมด
SELECT * FROM Medicines WHERE IsActive = 1;
GO

PRINT 'Medicines table created successfully!';
