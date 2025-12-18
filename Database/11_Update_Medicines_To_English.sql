-- ===================================
-- อัพเดตตาราง Medicines ให้เป็นภาษาอังกฤษ
-- ===================================

USE HospitalDB;
GO

-- ลบข้อมูลเก่าทั้งหมด
DELETE FROM Medicines;
GO

-- เพิ่มข้อมูลใหม่เป็นภาษาอังกฤษ
INSERT INTO Medicines (MedicineName, Category, Manufacturer, UnitPrice, StockQuantity, ReorderLevel, Description, IsActive, CreatedDate)
VALUES 
-- Painkillers
('Paracetamol 500mg', 'Painkiller', 'GPO', 5.00, 500, 50, 'Pain relief and fever reducer', 1, GETDATE()),
('Ibuprofen 400mg', 'Painkiller', 'GSK', 8.00, 300, 40, 'Anti-inflammatory pain reliever', 1, GETDATE()),
('Aspirin 100mg', 'Painkiller', 'Bayer', 6.50, 200, 30, 'Pain relief and blood thinner', 1, GETDATE()),

-- Antibiotics
('Amoxicillin 500mg', 'Antibiotic', 'Pfizer', 15.00, 250, 30, 'Antibiotic for bacterial infections', 1, GETDATE()),
('Azithromycin 250mg', 'Antibiotic', 'Teva', 25.00, 150, 20, 'Broad-spectrum antibiotic', 1, GETDATE()),
('Ciprofloxacin 500mg', 'Antibiotic', 'Bayer', 30.00, 100, 15, 'Fluoroquinolone antibiotic', 1, GETDATE()),

-- Antivirals
('Oseltamivir 75mg', 'Antiviral', 'Roche', 45.00, 80, 10, 'Influenza treatment', 1, GETDATE()),
('Acyclovir 400mg', 'Antiviral', 'GSK', 35.00, 120, 15, 'Herpes virus treatment', 1, GETDATE()),

-- Vitamins
('Vitamin C 1000mg', 'Vitamin', 'Blackmores', 12.00, 400, 50, 'Immune system support', 1, GETDATE()),
('Vitamin D3 2000IU', 'Vitamin', 'Nature Made', 18.00, 300, 40, 'Bone health support', 1, GETDATE()),
('Multivitamin Complex', 'Vitamin', 'Centrum', 22.00, 250, 30, 'Complete daily vitamins', 1, GETDATE()),
('Vitamin B Complex', 'Vitamin', 'Blackmores', 16.00, 200, 25, 'Energy and nerve support', 1, GETDATE()),

-- Supplements
('Calcium 600mg + D3', 'Supplement', 'Caltrate', 20.00, 180, 20, 'Bone strength supplement', 1, GETDATE()),
('Omega-3 Fish Oil', 'Supplement', 'Nordic Naturals', 28.00, 150, 20, 'Heart and brain health', 1, GETDATE()),
('Glucosamine 1500mg', 'Supplement', 'Schiff', 32.00, 100, 15, 'Joint health support', 1, GETDATE()),

-- Other
('Omeprazole 20mg', 'Other', 'AstraZeneca', 20.00, 200, 25, 'Reduces stomach acid', 1, GETDATE()),
('Metformin 500mg', 'Other', 'Merck', 10.00, 350, 40, 'Diabetes medication', 1, GETDATE()),
('Atorvastatin 20mg', 'Other', 'Pfizer', 25.00, 180, 20, 'Cholesterol management', 1, GETDATE()),
('Losartan 50mg', 'Other', 'Merck', 18.00, 220, 25, 'Blood pressure medication', 1, GETDATE()),
('Cetirizine 10mg', 'Other', 'UCB', 8.00, 300, 35, 'Antihistamine for allergies', 1, GETDATE());
GO

-- แสดงข้อมูลทั้งหมด
SELECT 
    MedicineID,
    MedicineName,
    Category,
    Manufacturer,
    UnitPrice,
    StockQuantity,
    ReorderLevel,
    Description
FROM Medicines 
WHERE IsActive = 1
ORDER BY Category, MedicineName;
GO

-- สรุปจำนวนยาแต่ละหมวด
SELECT 
    Category,
    COUNT(*) AS TotalMedicines,
    SUM(StockQuantity) AS TotalStock,
    AVG(UnitPrice) AS AvgPrice
FROM Medicines 
WHERE IsActive = 1
GROUP BY Category
ORDER BY Category;
GO

PRINT '✅ Medicines table updated to English successfully!';
PRINT '📊 Total medicines: 20 items';
