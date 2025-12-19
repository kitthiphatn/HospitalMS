# ============================================
# Git Update Script - Billing Management Module
# ============================================
# วิธีใช้: คลิกขวาไฟล์นี้ → Run with PowerShell
# หรือเปิด PowerShell แล้วรัน: .\git_update_billing.ps1

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  Git Update - Billing Management Module" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# ไปที่ folder โปรเจค
Set-Location "C:\Users\Marke\Desktop\C# hospital\HospitalMS"

# 1. ตรวจสอบสถานะ
Write-Host "📋 Checking Git status..." -ForegroundColor Yellow
git status
Write-Host ""

# 2. ดู branch ปัจจุบัน
Write-Host "🌿 Current branch:" -ForegroundColor Yellow
git branch
Write-Host ""

# ถามผู้ใช้ว่าต้องการดำเนินการต่อหรือไม่
$continue = Read-Host "Do you want to continue? (Y/N)"
if ($continue -ne "Y" -and $continue -ne "y") {
    Write-Host "❌ Cancelled by user" -ForegroundColor Red
    exit
}

# 3. Add ไฟล์ทั้งหมด
Write-Host ""
Write-Host "➕ Adding all files..." -ForegroundColor Yellow
git add .

# 4. แสดงไฟล์ที่จะ commit
Write-Host ""
Write-Host "📝 Files to be committed:" -ForegroundColor Yellow
git status --short
Write-Host ""

# ถามว่าต้องการ commit หรือไม่
$commitMsg = Read-Host "Enter commit message (or press Enter for default)"
if ([string]::IsNullOrWhiteSpace($commitMsg)) {
    $commitMsg = "feat: Complete Billing Management Module

- ✅ InvoiceListForm (List, Search, Filter, CRUD)
- ✅ InvoiceFormDialog (Create/Edit with auto-calculation)
- ✅ PaymentFormDialog (7 payment methods, auto status update)
- ✅ InvoicePreviewForm (Professional layout, Print, PDF export)
- ✅ Payment Methods display in invoice
- ✅ Billing Security Enhancements (Audit Log, Receipt Numbers, Transactions)
- ✅ Deployment Guide for production use

Features:
- Auto invoice number generation (INV-YYYYMMDD-XXXX)
- Auto calculation (SubTotal, Tax 7%, Discount, Total)
- Payment status tracking (Unpaid → Partial → Paid)
- Soft delete with IsActive flag
- Color-coded status display
- Transaction safety with stored procedures
- User tracking and audit trail
"
}

# 5. Commit
Write-Host ""
Write-Host "💾 Committing changes..." -ForegroundColor Yellow
git commit -m $commitMsg

# 6. ดู commit log ล่าสุด
Write-Host ""
Write-Host "📜 Latest commit:" -ForegroundColor Yellow
git log -1 --oneline
Write-Host ""

# ถามว่าต้องการ push หรือไม่
$push = Read-Host "Do you want to push to remote? (Y/N)"
if ($push -eq "Y" -or $push -eq "y") {
    Write-Host ""
    Write-Host "🚀 Pushing to remote..." -ForegroundColor Yellow
    git push
    Write-Host ""
    Write-Host "✅ Successfully pushed to remote!" -ForegroundColor Green
} else {
    Write-Host ""
    Write-Host "⚠️  Changes committed locally but not pushed" -ForegroundColor Yellow
    Write-Host "   Run 'git push' later to upload to remote" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  ✅ Git Update Complete!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# รอให้ผู้ใช้กด Enter ก่อนปิด
Read-Host "Press Enter to exit"
