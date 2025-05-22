# Product Management Catalogue

A web-based product management system built with ASP.NET Core (.NET 9), Entity Framework Core, and SQLite.  
This solution includes a Web API, an MVC UI, and a shared domain/application layer.

---

## Setup Instructions

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- (Optional) [DB Browser for SQLite](https://sqlitebrowser.org/) for inspecting the database

### Getting Started

1. **Clone the repository:**
   git clone <your-repo-url> cd ProductManagementCatalogue
   
2. **Configure the database:**
   - The solution uses SQLite. The connection string is set in `appsettings.json` for both API and UI projects.
   - By default, the database file is located at `ProductManagementCatalogue.API/ProductManagementCatalogue.db`.

3. **Apply database migrations:** 
```bash
    dotnet ef database update --startup-project ProductManagementCatalogue.API --project ProductManagementCatalogue.Infrastructure
```

   > This will create/update the SQLite database with the latest schema.

4. **Run the solution:**
   - Set the startup project to either the API or UI project as needed.
   - Press `F5` or use:

UI Project
```
 dotnet run --project ProductManagementCatalogue.UI
```

API Project
```
 dotnet run --project ProductManagementCatalogue.API
```

5. **Access the application:**
   - **UI:** [https://localhost:7270/](https://localhost:7270/)
   - **API:** [http://localhost:5069/](http://localhost:5069/) (adjust port as needed)

---

## Assumptions & Incomplete Areas

- **Assumptions:**
  - The database file is shared between API and UI projects via a common connection string.
  - The UI project uses Bootstrap for styling and jQuery unobtrusive validation for client-side validation.
  - Both API and UI use the same domain and application layers for business logic and validation.
  - Product validation rules:
    - Price must be greater than 0 (decimal supported).
    - Stock must be 0 or above.
    - Name and description cannot be empty.
    - Name: up to 250 characters; Description: up to 1000 characters.
  - There is no limit on the number of products; server-side filtering and pagination are used for scalability.

- **Incomplete Areas / To-Do:**
  - Unit and integration tests are not yet implemented.
  - Error handling and validation may need further refinement for production scenarios.
  - API documentation (e.g., Swagger/OpenAPI) is not included by default.
  - Product image upload and management are not included.

---

## Extras & Enhancements

- **Result Pattern:**  
  Implements a robust Result pattern for domain and application validation, supporting clear error and not-found handling throughout the stack.

- **Unit of Work & Repository Patterns:**  
  Uses Unit of Work and Repository patterns for clean, testable, and maintainable data access.

- **Pagination & Filtering:**  
  The product list supports efficient server-side pagination and filtering for large datasets.

- **Separation of Concerns:**  
  Follows clean architecture principles with separate projects for API, UI, domain, application, and infrastructure layers.

- **Dependency Injection:**  
  All services, repositories, and DbContext are registered and resolved via ASP.NET Core¡¯s built-in dependency injection.

- **Bootstrap Integration:**  
  The UI leverages Bootstrap for responsive design and enhanced user experience.

---