# Dr.Coffee Database Schema Documentation

## Overview
This database schema is designed for the Dr.Coffee ASP.NET backend. It supports a complete e-commerce coffee shop system with products, orders, customers, and customizations.

## Database Files

1. **DrCoffee_Database.sql** - Main database schema (tables, indexes, stored procedures, views)
2. **DrCoffee_Data_Population.sql** - Data population script (categories, products, prices, flavors)

## Installation Instructions

### Step 1: Create the Database
1. Open SQL Server Management Studio (SSMS)
2. Connect to your SQL Server instance
3. Open `DrCoffee_Database.sql`
4. **Important**: Uncomment and modify the database creation section at the top if needed:
   ```sql
   IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DrCoffeeDB')
   BEGIN
       CREATE DATABASE DrCoffeeDB;
   END
   GO
   USE DrCoffeeDB;
   GO
   ```
5. Execute the entire script (F5)

### Step 2: Populate Initial Data
1. Open `DrCoffee_Data_Population.sql`
2. Make sure the `USE DrCoffeeDB;` statement points to your database name
3. Execute the script (F5)
4. Verify data was inserted by checking the verification queries at the end

## Database Structure

### Core Tables

#### Categories
Stores product categories (Iced Coffee, Hot Coffee, Tea, etc.)
- `CategoryId` (PK)
- `Name` - Category name
- `DisplayOrder` - For sorting
- `IsActive` - Soft delete flag

#### Products
Main menu items/products
- `ProductId` (PK)
- `ProductCode` - Unique identifier (e.g., "iced_mocha")
- `NameEn` - English name
- `NameAr` - Arabic name
- `CategoryId` (FK)
- `CaffeineIndex` - 0-7 scale
- `IsCustomizable` - Whether item can be customized
- `ImageUrl` - Product image path

#### ProductPrices
Stores prices for different sizes
- `ProductPriceId` (PK)
- `ProductId` (FK)
- `Size` - "small", "medium", "large", "with_cream", etc.
- `Price` - Price in IQD

#### ProductFlavors
Stores available flavors for products that have multiple flavor options
- `ProductFlavorId` (PK)
- `ProductId` (FK)
- `FlavorName` - "Hazelnut", "Caramel", etc.

#### ProductTags
Tags for products (Hot/Cold)
- `ProductTagId` (PK)
- `ProductId` (FK)
- `Tag` - "Hot" or "Cold"

#### CustomizationOptions
Available customization options (Extra Syrup, Almond Milk, etc.)
- `CustomizationOptionId` (PK)
- `OptionId` - Unique code (e.g., "extra_syrup")
- `NameEn` - English name
- `NameAr` - Arabic name
- `Price` - Additional price

### Order Management Tables

#### Customers
Customer information (optional - can be null for guest orders)
- `CustomerId` (PK)
- `Name`, `PhoneNumber`, `Email`, `Address`, `WhatsAppNumber`

#### Orders
Order header information
- `OrderId` (PK)
- `OrderNumber` - Unique order number (auto-generated)
- `CustomerId` (FK, nullable)
- `CustomerName`, `CustomerPhone`, `CustomerWhatsApp`, `CustomerAddress` - For guest orders
- `OrderStatus` - Pending, Confirmed, Preparing, Ready, Completed, Cancelled
- `PaymentStatus` - Pending, Paid, Refunded
- `PaymentMethod` - Cash, Card, Online
- `SubTotal`, `TaxAmount`, `TotalAmount`
- `OrderDate`, `CreatedAt`, `UpdatedAt`

#### OrderItems
Individual items in an order
- `OrderItemId` (PK)
- `OrderId` (FK)
- `ProductId` (FK)
- `ProductCode`, `ProductNameEn`, `ProductNameAr` - Stored for historical reference
- `Size` - Selected size
- `UnitPrice` - Base price
- `Quantity`
- `CustomizationTotal` - Total price of all customizations
- `ItemTotal` - Final item total
- `Flavor` - Selected flavor (if applicable)

#### OrderItemCustomizations
Customizations applied to each order item
- `OrderItemCustomizationId` (PK)
- `OrderItemId` (FK)
- `CustomizationOptionId` (FK)
- `OptionId`, `NameEn`, `NameAr`, `Price` - Stored for historical reference

## Stored Procedures

### sp_GetProductDetails
Retrieves complete product information including prices, flavors, and tags.
```sql
EXEC sp_GetProductDetails @ProductId = 1;
```

### sp_CreateOrder
Creates a new order and returns the order number and ID.
```sql
DECLARE @OrderNumber NVARCHAR(50);
DECLARE @OrderId INT;
EXEC sp_CreateOrder 
    @CustomerName = 'John Doe',
    @CustomerPhone = '+9647772270005',
    @OrderNumber = @OrderNumber OUTPUT,
    @OrderId = @OrderId OUTPUT;
SELECT @OrderNumber, @OrderId;
```

## Views

### vw_ProductCatalog
Complete product catalog with all details, tags, and flavors in a single view.

### vw_OrderSummary
Order summary with customer information and item counts.

## Usage Examples

### Get All Products in a Category
```sql
SELECT * FROM Products 
WHERE CategoryId = 1 AND IsActive = 1;
```

### Get Product with All Prices
```sql
SELECT p.*, pp.Size, pp.Price
FROM Products p
INNER JOIN ProductPrices pp ON p.ProductId = pp.ProductId
WHERE p.ProductCode = 'iced_mocha';
```

### Get All Orders for Today
```sql
SELECT * FROM vw_OrderSummary
WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY OrderDate DESC;
```

### Get Order with Items
```sql
SELECT 
    o.OrderNumber,
    o.OrderDate,
    o.TotalAmount,
    oi.ProductNameEn,
    oi.Size,
    oi.Quantity,
    oi.ItemTotal
FROM Orders o
INNER JOIN OrderItems oi ON o.OrderId = oi.OrderId
WHERE o.OrderId = 1;
```

## Entity Framework Integration

For ASP.NET Core with Entity Framework Core, you can:

1. Install Entity Framework Core:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

2. Create DbContext:
   ```csharp
   public class DrCoffeeDbContext : DbContext
   {
       public DbSet<Category> Categories { get; set; }
       public DbSet<Product> Products { get; set; }
       public DbSet<ProductPrice> ProductPrices { get; set; }
       public DbSet<Order> Orders { get; set; }
       // ... other DbSets
   }
   ```

3. Use Code First Migrations or Database First approach with EF Core Power Tools.

## Notes

- All prices are stored in IQD (Iraqi Dinar)
- The database uses soft deletes (`IsActive` flag) for products
- Order numbers are auto-generated in format: `ORD-YYYYMMDD-####`
- Product information is stored in OrderItems for historical reference (even if product is deleted)
- The schema supports both registered customers and guest orders

## Next Steps

1. Create your ASP.NET Web API project
2. Set up Entity Framework Core or ADO.NET data access
3. Create API controllers for:
   - Products (GET all, GET by category, GET by ID)
   - Orders (POST create, GET by ID, GET all, PUT update status)
   - Customers (POST create, GET by phone)
4. Implement authentication/authorization if needed
5. Add validation and error handling

## Support

If you encounter any issues:
1. Check that all foreign key constraints are satisfied
2. Verify that categories are inserted before products
3. Ensure products are inserted before prices/flavors/tags
4. Check SQL Server version compatibility (SQL Server 2012+ recommended)



