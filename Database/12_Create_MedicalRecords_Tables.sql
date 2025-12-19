-- ===================================
-- สร้างตาราง Medical Records Module
-- ===================================

USE HospitalDB;
GO

-- ===================================
-- 1. ตาราง Medical Records (ประวัติการรักษา)
-- ===================================

IF OBJECT_ID('MedicalRecords', 'U') IS NOT NULL
    DROP TABLE MedicalRecords;
GO

CREATE TABLE MedicalRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID),
    AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
    VisitDate DATETIME NOT NULL DEFAULT GETDATE(),
    ChiefComplaint NVARCHAR(500),           -- อาการสำคัญ
    Diagnosis NVARCHAR(500),                -- การวินิจฉัย
    Treatment NVARCHAR(1000),               -- การรักษา
    Prescription NVARCHAR(1000),            -- ใบสั่งยา
    Notes NVARCHAR(MAX),                    -- บันทึกเพิ่มเติม
    DoctorID INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME
);
GO

-- ===================================
-- 2. ตาราง Chronic Diseases (โรคประจำตัว)
-- ===================================

IF OBJECT_ID('ChronicDiseases', 'U') IS NOT NULL
    DROP TABLE ChronicDiseases;
GO

CREATE TABLE ChronicDiseases (
    ChronicDiseaseID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID),
    DiseaseName NVARCHAR(200) NOT NULL,     -- ชื่อโรค
    DiagnosedDate DATE,                     -- วันที่วินิจฉัย
    Severity NVARCHAR(50),                  -- ความรุนแรง (Mild/Moderate/Severe)
    Status NVARCHAR(50),                    -- สถานะ (Active/Controlled/Remission)
    Notes NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME
);
GO

-- ===================================
-- 3. ตาราง Allergies (ประวัติการแพ้)
-- ===================================

IF OBJECT_ID('Allergies', 'U') IS NOT NULL
    DROP TABLE Allergies;
GO

CREATE TABLE Allergies (
    AllergyID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID),
    AllergyType NVARCHAR(100),              -- ประเภท (Drug/Food/Environmental)
    AllergyName NVARCHAR(200) NOT NULL,     -- ชื่อสิ่งที่แพ้
    Reaction NVARCHAR(500),                 -- อาการที่เกิด
    Severity NVARCHAR(50),                  -- ความรุนแรง (Mild/Moderate/Severe)
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME
);
GO

-- ===================================
-- เพิ่มข้อมูลตัวอย่าง
-- ===================================

-- Medical Records ตัวอย่าง
INSERT INTO MedicalRecords (PatientID, AppointmentID, VisitDate, ChiefComplaint, Diagnosis, Treatment, Prescription, DoctorID)
VALUES 
(1, 1, '2024-12-18 09:00:00', 'Fever and cough for 3 days', 'Acute Upper Respiratory Tract Infection', 'Rest, drink plenty of water', 'Paracetamol 500mg 3x daily, Amoxicillin 500mg 3x daily for 7 days', 1),
(2, 2, '2024-12-18 10:30:00', 'Regular checkup', 'Hypertension - controlled', 'Continue medication, low salt diet', 'Losartan 50mg 1x daily', 2);
GO

-- Chronic Diseases ตัวอย่าง
INSERT INTO ChronicDiseases (PatientID, DiseaseName, DiagnosedDate, Severity, Status, Notes)
VALUES 
(1, 'Diabetes Type 2', '2020-05-15', 'Moderate', 'Controlled', 'Blood sugar levels stable with medication'),
(2, 'Hypertension', '2018-03-20', 'Mild', 'Controlled', 'Blood pressure controlled with Losartan'),
(3, 'Asthma', '2015-07-10', 'Mild', 'Active', 'Uses inhaler as needed');
GO

-- Allergies ตัวอย่าง
INSERT INTO Allergies (PatientID, AllergyType, AllergyName, Reaction, Severity)
VALUES 
(1, 'Drug', 'Penicillin', 'Skin rash, itching', 'Moderate'),
(2, 'Food', 'Peanuts', 'Anaphylaxis', 'Severe'),
(3, 'Drug', 'Aspirin', 'Stomach upset', 'Mild'),
(4, 'Environmental', 'Dust mites', 'Sneezing, runny nose', 'Mild');
GO

-- ===================================
-- แสดงข้อมูลทั้งหมด
-- ===================================

PRINT '=== Medical Records ===';
SELECT * FROM MedicalRecords WHERE IsActive = 1;
GO

PRINT '=== Chronic Diseases ===';
SELECT * FROM ChronicDiseases WHERE IsActive = 1;
GO

PRINT '=== Allergies ===';
SELECT * FROM Allergies WHERE IsActive = 1;
GO

PRINT '✅ Medical Records Module tables created successfully!';
