# 🚀 Git Commands to Upload to GitHub

## Step-by-Step Instructions

### 1. Open PowerShell or Command Prompt
Navigate to your project directory:
```powershell
cd "C:\Users\Marke\Desktop\C# hospital\HospitalMS"
```

### 2. Initialize Git Repository
```powershell
git init
```

### 3. Add All Files
```powershell
git add .
```

### 4. Create First Commit
```powershell
git commit -m "Initial commit: Hospital Management System v1.0 with Login feature"
```

### 5. Add Remote Repository
```powershell
git remote add origin https://github.com/kitthiphatn/HospitalMS.git
```

### 6. Push to GitHub
```powershell
git branch -M main
git push -u origin main
```

---

## 📋 All Commands in One Block (Copy & Paste)

```powershell
cd "C:\Users\Marke\Desktop\C# hospital\HospitalMS"
git init
git add .
git commit -m "Initial commit: Hospital Management System v1.0 with Login feature"
git remote add origin https://github.com/kitthiphatn/HospitalMS.git
git branch -M main
git push -u origin main
```

---

## ⚠️ If You Get Errors:

### Error: "fatal: remote origin already exists"
```powershell
git remote remove origin
git remote add origin https://github.com/kitthiphatn/HospitalMS.git
```

### Error: "Updates were rejected"
```powershell
git pull origin main --allow-unrelated-histories
git push -u origin main
```

### Error: "Authentication failed"
You may need to use a Personal Access Token instead of password.
1. Go to GitHub Settings → Developer settings → Personal access tokens
2. Generate new token with 'repo' permissions
3. Use the token as your password when prompted

---

## ✅ After Successful Upload

Visit your repository:
https://github.com/kitthiphatn/HospitalMS

You should see:
- ✅ README.md displayed on the main page
- ✅ All project files uploaded
- ✅ .gitignore working (no bin/obj folders)

---

## 🎯 Next Steps (Optional)

### Add a License
1. Go to your GitHub repository
2. Click "Add file" → "Create new file"
3. Name it `LICENSE`
4. Choose MIT License template
5. Commit

### Add Screenshots
1. Take screenshots of your Login form
2. Create a `screenshots/` folder
3. Upload images
4. Update README.md with actual screenshot paths

---

## 📝 Future Updates

When you make changes:
```powershell
git add .
git commit -m "Description of changes"
git push
```

---

**Good luck with your upload! 🚀**
