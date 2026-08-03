# Order Product Feature — Implementation Plan

## Overview

Based on the diagram, the **Order Product** flow consists of 4 steps executed in sequence:

```
Order Product → Create Sale → Create Sale Detail → Voucher Generate (include SaleId)
```

All DB tables (`Tbl_Product`, `Tbl_Sale`, `Tbl_SaleDetail`) already exist in `AppDbContext`. The goal is to:

1. Add **request/response models** for Sale and SaleDetail
2. Build **`OrderController`** with endpoints to handle the order flow
3. Add **List & ById** endpoints for Sale and SaleDetail
4. Generate a **step-by-step guide** markdown file

---

## Open Questions

> [!IMPORTANT]
> **Voucher Number Format**: What format should the voucher number follow?
> The plan assumes `"VCH-{yyyyMMdd}-{SaleId}"` (e.g., `VCH-20260802-5`). Let me know if you want a different format.

> [!NOTE]
> **Single Endpoint vs Separate Endpoints**: The diagram shows `Create Sale → Create Sale Detail` as two steps. The plan implements this as a **single `POST /api/order/create`** endpoint that does both steps atomically in one transaction, returning the voucher at the end. This is the cleanest approach. If you want separate endpoints, let me know.

---

## Proposed Changes

### Models Layer

#### [NEW] `Models/Order/OrderItemRequestModel.cs`
Each item in the order (ProductId + Qty).

#### [MODIFY] [OrderRequestModel.cs](file:///c:/Users/Htet%20Min%20Lu/source/repos/DotNetJune2026/MiniPOSApplication/Models/Order/OrderRequestModel.cs)
Add `List<OrderItemRequestModel> Items` to the request body.

#### [MODIFY] [OrderResponseModel.cs](file:///c:/Users/Htet%20Min%20Lu/source/repos/DotNetJune2026/MiniPOSApplication/Models/Order/OrderResponseModel.cs)
Add fields: `IsSuccess`, `Message`, `SaleId`, `VoucherNumber`, `TotalAmount`, `CreatedDate`, `Items`.

---

### Controller Layer

#### [MODIFY] [OrderController.cs](file:///c:/Users/Htet%20Min%20Lu/source/repos/DotNetJune2026/MiniPOSApplication/Controllers/OrderController.cs)
Add 4 endpoints:
| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/api/order/create` | Create Sale + SaleDetails + return Voucher |
| `GET` | `/api/order` | List all Sales |
| `GET` | `/api/order/{id}` | Get Sale by ID (with details) |
| `GET` | `/api/order/detail/{saleDetailId}` | Get SaleDetail by ID |

**Logic inside `POST /api/order/create`:**
1. Validate request (items must not be empty)
2. For each item → validate ProductId exists and has enough StockQty
3. **Create `TblSale`** (SaleDate = now, VoucherNumber = placeholder, TotalAmount = 0)
4. Save to get `SaleId`
5. **Create `TblSaleDetail`** rows: UnitPrice from product, SubTotal = Qty × UnitPrice, deduct StockQty
6. Sum all SubTotals → update `TblSale.TotalAmount`
7. **Generate VoucherNumber** = `"VCH-{yyyyMMdd}-{SaleId}"`
8. Save all changes
9. Return response with SaleId, VoucherNumber, TotalAmount

---

## Verification Plan

### Manual Verification (Swagger UI)
1. Run the app → open Swagger
2. `POST /api/order/create` with valid product IDs and quantities
3. Check response contains `SaleId`, `VoucherNumber`, `TotalAmount`
4. `GET /api/order/{id}` → verify Sale + Details are returned
5. Check product `StockQty` was decremented via `GET /api/product/{id}`
