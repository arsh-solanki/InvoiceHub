# InvoiceHub – Multi-Entity Business Operations API

A comprehensive ASP.NET Core Web API built with .NET 10 demonstrating enterprise-level architectural patterns and best practices.

## 📋 Overview

Built an ASP.NET Core REST API with Repository Pattern architecture for customer, invoice, and inventory management, featuring async file uploads, real-time stock validations, and complete REST endpoints with Swagger documentation.

## 🎯 Features

- **Multi-Entity Management**
  - Customer profiles management
  - Product catalog with real-time inventory tracking
  - Invoice generation with multi-line details
  - Automated invoice-detail reconciliation
  - Stock level validations

- **File Upload System**
  - Async file upload pipeline
  - Configurable file storage service
  - Database persistence tracking
  - Scalable concurrent upload handling

- **Enterprise Architecture**
  - Repository Pattern for data abstraction
  - Multi-layered service tier for business logic separation
  - Dependency Injection configuration
  - Full REST endpoints with comprehensive CRUD operations

- **API Documentation**
  - Swagger/OpenAPI integration
  - Interactive API testing
  - Complete endpoint documentation

## 🛠️ Tech Stack

- **.NET 10** Web API
- **ASP.NET Core** REST API Framework
- **Entity Framework Core** (ORM)
- **SQL Server** (Database)
- **Swagger/OpenAPI** (API Documentation)
- **Dependency Injection** (Loose coupling)

## 📁 Project Structure

```
RepositoryPatternDemo/
├── Controllers/          # API endpoints
│   ├── CustomerController.cs
│   ├── ProductController.cs
│   ├── InvoiceMasterController.cs
│   ├── InvoiceDetailController.cs
│   ├── InventoryController.cs
│   └── FileUploadController.cs
├── Repositories/         # Data access layer
│   ├── BaseRepository.cs
│   ├── CustomerRepository.cs
│   ├── ProductRepository.cs
│   └── IFileUploadRepository.cs
├── Services/             # Business logic layer
│   ├── CustomerService.cs
│   ├── ProductService.cs
│   ├── FileStorageService.cs
│   └── FileUploadService.cs
├── Models/               # Data models
│   ├── Customer.cs
│   ├── Product.cs
│   ├── InvoiceMaster.cs
│   └── Inventory.cs
├── Data/                 # Database context
│   └── AppDbContext.cs
└── Program.cs           # Dependency injection & middleware configuration
```

## 🏗️ Architecture Pattern

### Repository Pattern
- Abstracts data access logic
- Enables easy testing with mock repositories
- Centralizes database queries

### Service Layer
- Contains business logic
- Validates data before persistence
- Separates concerns from controllers

### Dependency Injection
- Loose coupling between layers
- Easy to swap implementations
- Testable components

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (local or cloud)
- Visual Studio 2026 or Visual Studio Code

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/yourusername/RepositoryPatternDemo.git
   cd RepositoryPatternDemo
   ```

2. Restore NuGet packages
   ```bash
   dotnet restore
   ```

3. Configure database connection in `appsettings.json`
   ```json
   "ConnectionStrings": {
	   "DefaultConnection": "your_connection_string_here"
   }
   ```

4. Run database migrations
   ```bash
   dotnet ef database update
   ```

5. Run the application
   ```bash
   dotnet run
   ```

6. Access Swagger UI
   ```
   https://localhost:5001/swagger
   ```

## 📚 API Endpoints

### Customers
- `GET /api/customer` - Get all customers
- `GET /api/customer/{id}` - Get customer by ID
- `POST /api/customer` - Create new customer
- `PUT /api/customer/{id}` - Update customer
- `DELETE /api/customer/{id}` - Delete customer

### Products
- `GET /api/product` - Get all products
- `POST /api/product` - Create new product
- `PUT /api/product/{id}` - Update product
- `DELETE /api/product/{id}` - Delete product

### Invoices
- `GET /api/invoicemaster` - Get all invoices
- `POST /api/invoicemaster` - Create new invoice
- `GET /api/invoicedetail/{invoiceId}` - Get invoice details

### File Upload
- `POST /api/fileupload/upload` - Upload file
  - **Content-Type**: `multipart/form-data`
  - **Response**: File ID and filename

### Inventory
- `GET /api/inventory` - Get inventory items
- `PUT /api/inventory` - Update stock levels

## 🔑 Key Design Patterns

| Pattern | Implementation | Benefit |
|---------|-----------------|---------|
| **Repository Pattern** | `BaseRepository` + Specific Repos | Data abstraction & testability |
| **Dependency Injection** | `Program.cs` Configuration | Loose coupling & flexibility |
| **Service Layer** | Business logic separation | Testable, maintainable code |
| **Async/Await** | `async/await` throughout | Scalable, non-blocking I/O |
| **Configuration Management** | `appsettings.json` | Externalized settings |

## 🧪 Testing

Run unit tests (when added):
```bash
dotnet test
```

## 📝 Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=.;Database=InvoiceHubDB;Trusted_Connection=true;"
  },
  "FileStorage": {
	"UploadFolder": "uploads"
  }
}
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👤 Author
ARSH
