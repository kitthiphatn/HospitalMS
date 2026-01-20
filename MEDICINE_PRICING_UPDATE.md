# 💊 Medicine Pricing Update Guide

## 🎯 Overview
Added **Cost Price** and **Selling Price** columns to Medicine Inventory for better profit tracking and financial reporting.

---

## 📋 Changes Made

### 1. Database Changes
**File:** `Database/15_Add_Medicine_Pricing.sql`

**New Columns:**
- `CostPrice` - ราคาทุน/ราคารับเข้า (Purchase cost from supplier)
- `SellingPrice` - ราคาขาย (Selling price to patients)

**Migration:**
- Existing `UnitPrice` → copied to `SellingPrice`
- `CostPrice` → estimated as 70% of `SellingPrice`
- `UnitPrice` column kept for backward compatibility

### 2. Code Changes
**File:** `InvoiceFormDialog.cs`

Updated medicine query to use:
```csharp
ISNULL(SellingPrice, UnitPrice) AS UnitPrice
```

This ensures backward compatibility while using new pricing structure.

---

## 🚀 Implementation Steps

### Step 1: Run SQL Script
```sql
-- Execute in SSMS:
Database/15_Add_Medicine_Pricing.sql
```

### Step 2: Update Medicine Prices
After running the script, update the actual cost prices:

```sql
-- Example: Update cost prices for specific medicines
UPDATE MedicineInventory
SET CostPrice = 7.50,
    SellingPrice = 15.00
WHERE MedicineName = 'Paracetamol 500mg';

-- Or update all at once
UPDATE MedicineInventory
SET CostPrice = [your_actual_cost],
    SellingPrice = [your_actual_selling_price]
WHERE MedicineID = [id];
```

### Step 3: (Optional) Update Medicine Form UI
To show both prices in the Medicine management form:

**Option A: Keep current UnitPrice field**
- Rename label to "Selling Price"
- UnitPrice field will save to SellingPrice column

**Option B: Add separate fields**
- Add `txtCostPrice` field
- Add `txtSellingPrice` field
- Update save/load logic

---

## 📊 Benefits

### 1. Profit Calculation
```sql
SELECT 
    MedicineName,
    CostPrice,
    SellingPrice,
    (SellingPrice - CostPrice) AS Profit,
    ((SellingPrice - CostPrice) / SellingPrice * 100) AS MarginPercent
FROM MedicineInventory;
```

### 2. Financial Reports
- Track total cost of inventory
- Calculate profit margins
- Analyze pricing strategies

### 3. Invoice Accuracy
- Invoices now use correct selling price
- Cost tracking for internal reporting

---

## ⚠️ Important Notes

1. **CostPrice is estimated** - Please update with actual purchase prices
2. **UnitPrice still exists** - For backward compatibility
3. **Medicine Form** - May need UI update to show both prices
4. **Reports** - Can now include profit analysis

---

## 🔄 Next Steps (Optional)

### 1. Update Medicine Form
Add fields for Cost Price and Selling Price in `MedicineFormDialog`

### 2. Create Profit Reports
- Medicine Profit Report
- Inventory Value Report
- Margin Analysis Report

### 3. Remove UnitPrice Column
Once fully migrated, uncomment Step 4 in the SQL script to remove old column

---

## 📝 Sample Data

After migration, your data will look like:

| Medicine | CostPrice | SellingPrice | Profit | Margin% |
|----------|-----------|--------------|--------|---------|
| Paracetamol | 5.00 | 10.00 | 5.00 | 50% |
| Amoxicillin | 15.00 | 25.00 | 10.00 | 40% |
| Ibuprofen | 8.00 | 15.00 | 7.00 | 46.67% |

---

## ✅ Testing Checklist

- [ ] Run SQL script successfully
- [ ] Verify new columns exist
- [ ] Check data migration (SellingPrice = old UnitPrice)
- [ ] Update actual CostPrice values
- [ ] Test Invoice → Add Item → Medicine selection
- [ ] Verify correct price is used in invoices
- [ ] (Optional) Update Medicine Form UI

---

**Ready to implement!** 🚀
