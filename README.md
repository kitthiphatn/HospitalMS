# 🏥 Hospital Management System

A comprehensive Hospital Management System built with C# .NET Framework and SQL Server, designed for learning and demonstration purposes.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Express-red)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Database Schema](#database-schema)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Usage](#usage)
- [Screenshots](#screenshots)
- [Project Structure](#project-structure)
- [Learning Materials](#learning-materials)
- [Contributing](#contributing)
- [License](#license)

---

## ✨ Features

### Current Features (v1.0)
- ✅ **User Authentication** - Login system with role-based access
- ✅ **Database Management** - Complete hospital database with 11 tables
- ✅ **3-Tier Architecture** - Separation of UI, Business Logic, and Data Access
- ✅ **Patient Management** - Store and manage patient information
- ✅ **Doctor Management** - Manage doctor profiles and specializations
- ✅ **Appointment System** - Schedule and track appointments
- ✅ **Medicine Inventory** - Track medicine stock and details
- ✅ **Billing System** - Generate and manage bills

### Planned Features
- 🔄 Dashboard with statistics
- 🔄 Advanced search and filtering
- 🔄 Report generation
- 🔄 Password hashing and security improvements
- 🔄 Prescription management
- 🔄 Medical records management

---

## 🛠️ Technologies

- **Framework:** .NET Framework 4.7.2
- **Language:** C# 7.3
- **Database:** SQL Server Express 2025
- **UI:** Windows Forms
- **ORM:** ADO.NET (with optional Dapper support)
- **Architecture:** 3-Tier Architecture

---

## 🏗️ Architecture

This project follows a **3-Tier Architecture** pattern:

```
┌─────────────────────────────────────┐
│   Presentation Layer (UI)           │
│   - Windows Forms                   │
│   - User Interface                  │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Business Logic Layer (BLL)        │
│   - Business Rules                  │
│   - Validation                      │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Data Access Layer (DAL)           │
│   - DatabaseHelper                  │
│   - Repositories                    │
│   - Models                          │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Database (SQL Server)             │
│   - HospitalDB                      │
└─────────────────────────────────────┘
```

---

## 🗄️ Database Schema

The system uses **11 interconnected tables**:

1. **Roles** - User roles (Admin, Doctor, Nurse, etc.)
2. **Users** - System users with authentication
3. **Patients** - Patient information and medical history
4. **Doctors** - Doctor profiles and specializations
5. **Appointments** - Appointment scheduling
6. **MedicalRecords** - Patient medical records
7. **Medicines** - Medicine inventory
8. **Prescriptions** - Prescription management
9. **Billing** - Billing information
10. **BillDetails** - Detailed billing items
11. **ActivityLogs** - System activity tracking

### Entity Relationship Diagram

```
Users ──┬── Patients ──── Appointments ──── Doctors
        │                      │
        └── ActivityLogs       ├── MedicalRecords
                               │
                               └── Prescriptions ──── Medicines
                                         │
                                         └── Billing ──── BillDetails
```

---

## 🚀 Getting Started

### Prerequisites

- **Windows OS** (Windows 10 or later)
- **Visual Studio 2022** (Community Edition or higher)
- **SQL Server Express 2025** (or any SQL Server version)
- **SQL Server Management Studio (SSMS)** - Optional but recommended

### Installation

#### 1. Clone the Repository

```bash
git clone https://github.com/kitthiphatn/HospitalMS.git
cd HospitalMS
```

#### 2. Set Up the Database

**Option A: Using SSMS (Recommended)**

1. Open **SQL Server Management Studio**
2. Connect to your SQL Server instance (`.\SQLEXPRESS`)
3. Open and execute `Database/01_CreateDatabase.sql`
4. Execute `Database/02_InsertSampleData.sql`
5. Execute `Database/09_CompleteUpdateToEnglish.sql` (optional - updates to English)

**Option B: Using Command Line**

```powershell
cd Database
sqlcmd -S .\SQLEXPRESS -i "01_CreateDatabase.sql"
sqlcmd -S .\SQLEXPRESS -i "02_InsertSampleData.sql"
sqlcmd -S .\SQLEXPRESS -i "09_CompleteUpdateToEnglish.sql"
```

#### 3. Configure Connection String

Open `HospitalMS.UI/App.config` and verify the connection string:

```xml
<connectionStrings>
  <add name="HospitalDB" 
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

#### 4. Build and Run

1. Open `HospitalManagementSystem.sln` in Visual Studio
2. Build the solution (`Ctrl + Shift + B`)
3. Run the project (`F5`)

---

## 📖 Usage

### Default Login Credentials

| Username | Password | Role |
|----------|----------|------|
| `admin` | `admin123` | Administrator |
| `doctor1` | `doctor123` | Doctor |
| `nurse1` | `nurse123` | Nurse |
| `reception1` | `reception123` | Receptionist |
| `pharma1` | `pharma123` | Pharmacist |

### First Steps

1. **Login** with `admin` / `admin123`
2. Explore the system features
3. Check the database for sample data

---

## 📸 Screenshots

### Login Screen
(<img width="383" height="293" alt="image" src="https://github.com/user-attachments/assets/7a67f639-2bd7-4bf4-9fc3-64277142290d" />)](https://eu-central.storage.cloudconvert.com/tasks/a112e219-9805-4c55-803c-dbcb31665369/Screenshot%202026-01-20%20112639.webp?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=cloudconvert-production%2F20260120%2Ffra%2Fs3%2Faws4_request&X-Amz-Date=20260120T043221Z&X-Amz-Expires=86400&X-Amz-Signature=2ff8b8d4a8a7a7b0552f2e2df436776e6506a9e74b94c74941468606a20827fe&X-Amz-SignedHeaders=host&response-content-disposition=inline%3B%20filename%3D%22Screenshot%202026-01-20%20112639.webp%22&response-content-type=image%2Fwebp&x-id=GetObject)

### Dashboard
([<img width="1180" height="691" alt="image" src="https://github.com/user-attachments/assets/91f7bb28-7f14-46eb-a337-ba6012bf7d2c" />](https://eu-central.storage.cloudconvert.com/tasks/f853b19a-7809-42d8-a8ba-81cb897b3c08/Screenshot%202026-01-20%20112628.webp?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=cloudconvert-production%2F20260120%2Ffra%2Fs3%2Faws4_request&X-Amz-Date=20260120T043221Z&X-Amz-Expires=86400&X-Amz-Signature=ddd17c7d121b272742c12c4db52a5a6168fd328e6edfdecde717ad661979c123&X-Amz-SignedHeaders=host&response-content-disposition=inline%3B%20filename%3D%22Screenshot%202026-01-20%20112628.webp%22&response-content-type=image%2Fwebp&x-id=GetObject)

---

## 📁 Project Structure

```
HospitalMS/
├── 00_LearningMaterials/          # Learning guides and documentation
│   ├── 01_CSharp_Basics.md
│   ├── 02_Getting_Started.md
│   ├── 03_DatabaseHelper_Explained.md
│   ├── 04_Database_Setup.md
│   └── 05_DatabaseHelper_Guide.md
│
├── Database/                       # SQL scripts
│   ├── 01_CreateDatabase.sql
│   ├── 02_InsertSampleData.sql
│   ├── 06_ConvertToEnglish.sql
│   ├── 07_UpdateUsersToEnglish.sql
│   ├── 08_UpdateDoctorsToEnglish.sql
│   ├── 09_CompleteUpdateToEnglish.sql
│   └── README_Database.md
│
├── HospitalMS.UI/                  # Presentation Layer
│   ├── Forms/
│   │   └── Login/
│   │       ├── LoginForm.cs
│   │       └── LoginForm.Designer.cs
│   ├── Program.cs
│   └── App.config
│
├── HospitalMS.BLL/                 # Business Logic Layer
│
├── HospitalMS.DAL/                 # Data Access Layer
│   ├── DatabaseHelper.cs
│   ├── Models/
│   │   ├── User.cs
│   │   ├── Patient.cs
│   │   ├── Doctor.cs
│   │   ├── Appointment.cs
│   │   └── Medicine.cs
│   └── Repositories/
│       └── UserRepository.cs
│
├── HospitalMS.Common/              # Shared Utilities
│
└── HospitalManagementSystem.sln    # Solution file
```

---

## 📚 Learning Materials

This project includes comprehensive learning materials for beginners:

- **C# Basics** - Introduction to C# programming
- **Getting Started** - Step-by-step project setup
- **DatabaseHelper Guide** - Understanding database connectivity
- **Database Setup** - Complete database configuration guide

All materials are in the `00_LearningMaterials/` directory.

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author

**Kitthiphat N.**
- GitHub: [@kitthiphatn](https://github.com/kitthiphatn)

---

## 🙏 Acknowledgments

- Built as a learning project for C# and SQL Server
- Inspired by real-world hospital management systems
- Special thanks to the .NET community

---

## 📞 Support

If you have any questions or need help, please:
- Open an issue on GitHub
- Check the learning materials in `00_LearningMaterials/`
- Review the database documentation in `Database/README_Database.md`

---

**⭐ If you find this project helpful, please give it a star!**
