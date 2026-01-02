# Dr.Coffee Admin API

A comprehensive ASP.NET Core Web API for managing the Dr.Coffee shop operations with Admin and Manager roles.

## Features

- ✅ **ASP.NET Core Identity** with Admin and Manager roles
- ✅ **JWT Authentication** for secure API access
- ✅ **Full CRUD Operations** for Products, Categories, and Orders
- ✅ **Repository Pattern** for clean, maintainable code
- ✅ **DTOs** for all API requests and responses
- ✅ **Swagger UI** with JWT Bearer token support
- ✅ **Automatic Seed Data** for roles and default users

## Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB, SQL Server Express, or SQL Server)
- Visual Studio 2022 or VS Code

## Setup Instructions

### 1. Database Setup

First, run the database scripts in the root directory:
1. Execute `DrCoffee_Database.sql` to create the database schema
2. Execute `DrCoffee_Data_Population.sql` to populate initial data

### 2. Update Connection String

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=DrCoffeeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

For SQL Server LocalDB (default):
```
Server=(localdb)\\mssqllocaldb;Database=DrCoffeeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

### 3. Configure JWT Settings

The JWT settings are already configured in `appsettings.json`. For production, change the JWT Key to a secure, randomly generated key (at least 32 characters).

### 4. Run Database Migrations (Optional)

If you want to use EF Core Migrations instead of the SQL scripts:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger` (or root URL in development)

## Default Users

The application automatically creates default users on first startup:

- **Admin User:**
  - Email: `admin@drcoffee.com`
  - Password: `Admin@123`
  - Role: Admin

- **Manager User:**
  - Email: `manager@drcoffee.com`
  - Password: `Manager@123`
  - Role: Manager

You can change these in `appsettings.json` under `SeedData` section.

## API Endpoints

### Authentication

#### Login
```
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@drcoffee.com",
  "password": "Admin@123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "admin@drcoffee.com",
  "firstName": "Admin",
  "lastName": "User",
  "roles": ["Admin"],
  "expiresAt": "2024-01-02T12:00:00Z"
}
```

### Product Management (Admin/Manager)

All product endpoints require JWT authentication with Admin or Manager role.

#### Get All Products
```
GET /api/admin/products
Authorization: Bearer {token}
```

#### Get Product by ID
```
GET /api/admin/products/{id}
Authorization: Bearer {token}
```

#### Create Product
```
POST /api/admin/products
Authorization: Bearer {token}
Content-Type: application/json

{
  "productCode": "new_product",
  "nameEn": "New Product",
  "nameAr": "منتج جديد",
  "imageUrl": "/images/product.jpg",
  "categoryId": 1,
  "caffeineIndex": 5,
  "isCustomizable": true,
  "isActive": true,
  "prices": [
    { "size": "medium", "price": 5000 },
    { "size": "large", "price": 6500 }
  ],
  "tags": ["Cold"],
  "flavors": ["Vanilla", "Caramel"]
}
```

#### Update Product (including prices and availability)
```
PUT /api/admin/products/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "nameEn": "Updated Product Name",
  "isActive": false,
  "prices": [
    { "size": "medium", "price": 5500 },
    { "size": "large", "price": 7000 }
  ]
}
```

#### Delete Product (soft delete)
```
DELETE /api/admin/products/{id}
Authorization: Bearer {token}
```

### Category Management (Admin/Manager)

#### Get All Categories
```
GET /api/admin/categories
Authorization: Bearer {token}
```

#### Get Category by ID
```
GET /api/admin/categories/{id}
Authorization: Bearer {token}
```

#### Create Category
```
POST /api/admin/categories
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "New Category",
  "displayOrder": 10
}
```

#### Update Category
```
PUT /api/admin/categories/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Updated Category Name",
  "displayOrder": 5,
  "isActive": true
}
```

#### Delete Category (soft delete)
```
DELETE /api/admin/categories/{id}
Authorization: Bearer {token}
```

### Order Management (Admin/Manager)

#### Get All Orders
```
GET /api/admin/orders
Authorization: Bearer {token}
```

#### Get Order by ID
```
GET /api/admin/orders/{id}
Authorization: Bearer {token}
```

#### Update Order Status
```
PUT /api/admin/orders/{id}/status
Authorization: Bearer {token}
Content-Type: application/json

{
  "orderStatus": "Completed"
}
```

Valid statuses: `Pending`, `Confirmed`, `Preparing`, `Ready`, `Completed`, `Cancelled`

#### Get Orders by Status
```
GET /api/admin/orders/status/{status}
Authorization: Bearer {token}
```

## Using Swagger UI

1. Navigate to the Swagger UI (root URL in development mode)
2. Click the **Authorize** button at the top
3. Enter: `Bearer {your_jwt_token}`
4. Click **Authorize**
5. Now you can test all protected endpoints directly from Swagger

## Project Structure

```
DrCoffee_BackEnd/
├── Controllers/
│   ├── AdminController.cs      # Full CRUD for Products, Categories, Orders
│   └── AuthController.cs        # Login endpoint
├── Data/
│   └── ApplicationDbContext.cs # EF Core DbContext with Identity
├── DTOs/
│   ├── AuthDTOs.cs             # Login, Register, AuthResponse
│   ├── CategoryDTOs.cs         # Category request/response DTOs
│   ├── OrderDTOs.cs             # Order request/response DTOs
│   └── ProductDTOs.cs           # Product request/response DTOs
├── Models/
│   ├── ApplicationUser.cs       # Identity user model
│   ├── Category.cs              # Category model
│   ├── Order.cs                  # Order, OrderItem, Customer models
│   └── Product.cs                # Product, ProductPrice, ProductTag, ProductFlavor models
├── Repositories/
│   ├── ICategoryRepository.cs   # Category repository interface
│   ├── CategoryRepository.cs     # Category repository implementation
│   ├── IOrderRepository.cs      # Order repository interface
│   ├── OrderRepository.cs        # Order repository implementation
│   ├── IProductRepository.cs    # Product repository interface
│   └── ProductRepository.cs     # Product repository implementation
├── appsettings.json             # Configuration (connection string, JWT, seed data)
└── Program.cs                   # Application startup and configuration
```

## Security Notes

1. **JWT Key**: Change the JWT key in production to a secure, randomly generated key
2. **Password Policy**: Adjust password requirements in `Program.cs` if needed
3. **CORS**: The current CORS policy allows all origins. Restrict this in production
4. **HTTPS**: Always use HTTPS in production
5. **Connection String**: Never commit connection strings with sensitive data to version control

## Troubleshooting

### Database Connection Issues
- Verify SQL Server is running
- Check connection string in `appsettings.json`
- Ensure database `DrCoffeeDB` exists (run the SQL scripts first)

### Authentication Issues
- Verify JWT settings in `appsettings.json`
- Check that roles are created (they should be auto-created on startup)
- Ensure you're using the correct email/password for login

### Migration Issues
- If using migrations, ensure EF Core tools are installed: `dotnet tool install --global dotnet-ef`
- Delete `Migrations` folder and recreate if needed

## Next Steps

1. **Add Validation**: Enhance DTOs with more validation attributes
2. **Add Logging**: Implement structured logging with Serilog
3. **Add Caching**: Implement caching for frequently accessed data
4. **Add Pagination**: Add pagination to list endpoints
5. **Add Filtering/Sorting**: Add query parameters for filtering and sorting
6. **Add Unit Tests**: Create unit tests for repositories and controllers
7. **Add Integration Tests**: Create integration tests for API endpoints

## Support

For issues or questions, please check:
- ASP.NET Core Documentation: https://docs.microsoft.com/aspnet/core
- Entity Framework Core Documentation: https://docs.microsoft.com/ef/core
- JWT Authentication: https://jwt.io



