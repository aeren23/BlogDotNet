# Blog Project

This project is a blog site application developed using ASP.NET Core. It incorporates modern software development techniques and several popular technologies. Below are the details of the project and the technologies used.

---

## Project Purpose
This project provides a platform for users to create, edit, and publish blog posts. It also offers a secure structure with authentication and authorization systems.

---

## Technologies and Tools Used

### 1. **ASP.NET Core**
- The core framework of the web application.
- Integrated Authentication and Authorization (Role-Based Authentication) systems.

### 2. **Entity Framework Core**
- **Code-First Approach** is utilized.
- Provides ORM (Object-Relational Mapping) for database operations.

### 3. **Layered Architecture**
- **Core Layer**: Contains common entity definitions and base features.
- **Entity Layer**: Defines specific entities and their properties.
- **Data Access Layer (DAL)**: Handles database operations and includes DbContext.
- **Service Layer**: Implements business logic. Uses the UnitOfWork pattern to connect DbContext and services.
- **UI Layer**: Manages the user interface and user interactions.

### 4. **Fluent Validation**
- Used to define data validation rules.

### 5. **AutoMapper**
- Handles DTO (Data Transfer Object) and Entity transformations.

### 6. **Toast Notification**
- Provides notifications to enhance user experience.

---

## Project Setup

### Requirements
- **.NET 6 SDK** or a newer version
- **SQL Server**
- An IDE (Visual Studio or Visual Studio Code)

### Setup Steps
1. **Clone the Project**
   ```bash
   git clone <repository-link>
   cd BlogProject
   ```

2. **Configure Database Connection**
   - Update the database connection string (“ConnectionString”) in the `appsettings.json` file.

3. **Create the Database**
   ```bash
   dotnet ef database update
   ```

4. **Run the Project**
   ```bash
   dotnet run
   ```

---

## Project Structure

### Layers
- **Core Layer**: Contains common entity definitions and base class structures.
- **Entity Layer**: Defines all specific entities and inherits from the base class in the Core Layer.
- **Data Access Layer (DAL)**: Handles database operations using Entity Framework.
- **Service Layer**: Implements business logic and the UnitOfWork pattern.
- **UI Layer**: Contains the user interface and controllers.

---

## Usage

### 1. User Authorization
- Role-Based Authentication is implemented.
- Different authorization levels exist for Admin and User roles.

### 2. Blog Posts
- Supports creating, editing, and deleting blog posts.

### 3. Notifications
- Toast Notifications provide feedback during user operations.

---

## Contribution
To contribute, please create a **pull request** or open an **issue**.

---

## License
This project is licensed under the MIT License. For more information, see the LICENSE file.

---

## Contact
For questions or feedback, please reach out at [your email address].

