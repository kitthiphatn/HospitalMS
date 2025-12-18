# 📦 Git Update - Patient & Appointment Management

## 🌿 สร้าง Branch ใหม่

```powershell
# 1. ตรวจสอบ Branch ปัจจุบัน
git branch

# 2. สร้าง Branch ใหม่สำหรับ Feature นี้
git checkout -b feature/patient-appointment-management

# 3. ตรวจสอบว่าอยู่ใน Branch ใหม่แล้ว
git branch
```

---

## 📝 Commit Changes

```powershell
# 4. ดูไฟล์ที่เปลี่ยนแปลง
git status

# 5. เพิ่มไฟล์ทั้งหมด
git add .

# 6. Commit พร้อม Message
git commit -m "feat: Add Patient and Appointment Management

- ✅ Patient List Form with CRUD operations
- ✅ Patient Form Dialog (Add/Edit)
- ✅ Appointment List Form with filtering
- ✅ Appointment Form Dialog with Patient/Doctor selection
- ✅ Date/Time pickers for appointments
- ✅ Status management (Pending/Confirmed/Completed/Cancelled)
- ✅ Search and filter functionality
- ✅ Dashboard integration
- ✅ Logout functionality
- 📚 Learning materials for both modules"
```

---

## 🚀 Push ไปยัง GitHub

```powershell
# 7. Push Branch ใหม่ไปยัง GitHub
git push -u origin feature/patient-appointment-management
```

---

## 🔀 (Optional) Merge กลับไป Main

**ถ้าต้องการ Merge เข้า Main:**

```powershell
# 8. กลับไป Main Branch
git checkout main

# 9. Merge Feature Branch เข้ามา
git merge feature/patient-appointment-management

# 10. Push Main Branch
git push origin main
```

---

## 📊 ตรวจสอบบน GitHub

1. ไปที่ `https://github.com/kitthiphatn/HospitalMS`
2. ดู **Branches** → ควรเห็น `feature/patient-appointment-management`
3. สามารถสร้าง **Pull Request** ได้ถ้าต้องการ

---

## 🎯 สรุป Git Workflow

```
main (stable)
  └── feature/patient-appointment-management (new features)
        ├── Patient Management
        ├── Appointment Management
        └── Dashboard Updates
```

---

## 💡 Tips

- **Branch Name Convention:**
  - `feature/` = Feature ใหม่
  - `bugfix/` = แก้ Bug
  - `hotfix/` = แก้ด่วน

- **Commit Message Convention:**
  - `feat:` = Feature ใหม่
  - `fix:` = แก้ Bug
  - `docs:` = เอกสาร
  - `refactor:` = ปรับโครงสร้างโค้ด

---

**พร้อมรันคำสั่งเลยครับ!** 🚀
