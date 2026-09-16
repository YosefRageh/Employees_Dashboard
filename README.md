# Employees Management System

A modern and responsive Employee Management System built with **ASP.NET Core MVC**, **C#**, **Entity Framework Core**, and **SQL Server**.

The project provides a clean and professional dashboard for managing employee records, employee images, user accounts, sessions, and workforce statistics.

---

## Overview

Employees Management System is an ASP.NET Core MVC web application designed to provide a simple, organized, and professional way to manage employee information.

The application uses **ASP.NET Core Identity** for authentication and **Entity Framework Core with SQL Server** for database management.

Users must authenticate before accessing the employee management workspace. After successful login, users can access the dashboard and manage employees through the available features.

---

## Features

### Authentication

- User Registration
- User Login
- Remember Me
- Password validation
- Secure Logout
- ASP.NET Core Identity
- Authorization for protected employee pages

### Dashboard

- Total Employees
- Average Employee Age
- Employees With Images
- Employees Without Images
- Recent Employees
- Quick access to employee management features

### Employee Management

- View all employees
- Add new employees
- Edit employee information
- Delete employees
- Automatic employee ID generation by the database
- Employee name and age validation

### Employee Images

- Upload employee profile images
- Image preview before submission
- Drag and drop image upload
- Download employee images
- Display employee profile images
- Remove employee images when deleting an employee

### Session Management

- Store a selected employee in Session
- Retrieve employee information from Session
- Display the currently selected employee

### User Interface

- Premium dark theme
- Light theme support
- Responsive layout
- Modern dashboard
- Sidebar navigation
- Top navigation bar
- Search interface
- Notification interface
- Account dropdown
- Mobile navigation
- Smooth UI interactions and animations
- Bootstrap Icons

---

## Technologies

### Backend

- C#
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server

### Frontend

- HTML5
- CSS3
- JavaScript
- Razor Views
- Bootstrap Icons

### Tools

- Visual Studio
- SQL Server Management Studio (SSMS)
- Git
- GitHub

---

## Project Structure

```text
Employees
│
├── Employees.BLL
│   │
│   ├── Mapper
│   │   └── EmployeeMapper.cs
│   │
│   ├── Models
│   │   ├── AccountVM
│   │   │   ├── LoginVM.cs
│   │   │   └── RegisterVM.cs
│   │   │
│   │   └── EmployeeVM
│   │       ├── CreateEmployeeVM.cs
│   │       └── GetAllEmployeeVM.cs
│   │
│   └── Services
│
├── Employees.DAL
│   │
│   ├── DataBase
│   │   └── EmployeesDbContext.cs
│   │
│   ├── Entities
│   │   ├── ApplicationUser.cs
│   │   └── Employee.cs
│   │
│   └── Repository
│       ├── Abstractions
│       └── Implementations
│
├── Employees.PL
│   │
│   ├── Controllers
│   │   ├── AccountController.cs
│   │   ├── DashboardController.cs
│   │   ├── EmployeeController.cs
│   │   ├── HomeController.cs
│   │   └── ThemeController.cs
│   │
│   ├── Filters
│   │   └── ThemeActionFilter.cs
│   │
│   ├── Helpers
│   │   ├── Upload.cs
│   │   └── SessionExtensions.cs
│   │
│   ├── Views
│   │   ├── Account
│   │   │   ├── Login.cshtml
│   │   │   └── Register.cshtml
│   │   │
│   │   ├── Employee
│   │   │   ├── Dashboard.cshtml
│   │   │   ├── GetAll.cshtml
│   │   │   ├── Create.cshtml
│   │   │   ├── Edit.cshtml
│   │   │   └── SessionEmployee.cshtml
│   │   │
│   │   └── Shared
│   │       └── _Layout.cshtml
│   │
│   └── wwwroot
│       ├── css
│       │   └── site.css
│       │
│       ├── js
│       │   └── site.js
│       │
│       └── Files
│
└── README.md
```

---

## Application Flow

```text
Application Start
       │
       ▼
     Login
       │
       ├── Existing Account
       │        │
       │        ▼
       │      Login
       │        │
       │        ▼
       │     Dashboard
       │
       └── No Account
                │
                ▼
             Register
                │
                ▼
              Login
                │
                ▼
             Dashboard
                │
                ▼
       Employee Management
```

---

## Employee Management Flow

```text
Dashboard
    │
    ├── Employees
    │     ├── View Employees
    │     ├── Edit Employee
    │     ├── Delete Employee
    │     └── Download Image
    │
    ├── Add Employee
    │     ├── Name
    │     ├── Age
    │     └── Profile Image
    │
    └── Session
          └── Store / Retrieve Selected Employee
```

---

## Database

The project uses **SQL Server** with **Entity Framework Core**.

### Employee Entity

```text
Employee
├── Id
├── Name
├── Age
├── Salary
└── PathImage
```

The employee `Id` is generated automatically by the database when creating a new employee.

Authentication data is managed using ASP.NET Core Identity.

---

## Image Management

Employee profile images are stored in:

```text
wwwroot/Files
```

The application supports:

- Image upload
- Image preview
- Drag and drop upload
- Image download
- Image removal

Uploaded images receive unique file names to help avoid conflicts.

---

## Setup

### 1. Clone the Repository

```bash
git clone YOUR_REPOSITORY_URL
```

Open the solution in Visual Studio.

### 2. Configure SQL Server

Make sure SQL Server is installed and running.

Configure the application's connection string according to your local SQL Server environment.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EmployeesDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Use your own connection string when running the project locally.

### 3. Apply Database Migrations

Open **Package Manager Console** in Visual Studio and run:

```powershell
Update-Database
```

If the project requires creating a new migration:

```powershell
Add-Migration InitialCreate
Update-Database
```

### 4. Run the Application

Run the project from Visual Studio.

The application starts with the authentication flow.

Create a new account or log in using an existing account.

After successful authentication, the Dashboard becomes available.

---

## Security

The application uses ASP.NET Core Identity to handle:

- Authentication
- Registration
- Password validation
- Login sessions
- Secure logout
- Authorization

Protected employee management pages require an authenticated user.

Sensitive information such as API keys, passwords, and other secrets should not be committed to the repository.

---

## UI Design

The application follows a premium enterprise design system based on:

- Graphite and black surfaces
- Champagne gold accents
- Soft white typography
- Muted gray text
- Subtle violet highlights
- Elegant borders
- Layered shadows
- Rounded panels
- Responsive layouts
- Smooth micro-interactions

The interface is designed for:

- Desktop
- Tablet
- Mobile

---

## Future Improvements

Possible future improvements include:

- Integrated AI Assistant
- Advanced employee search and filtering
- More dashboard analytics
- Role-based authorization
- Extended employee information
- Reporting features
- Additional UI customization

---

## Project Purpose

This project was created to practice and demonstrate practical experience with:

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Repository and business-layer organization
- MVC architecture
- Session management
- File upload and download
- Responsive frontend development
- Modern UI/UX design

---

## Author

Developed as an ASP.NET Core MVC learning and portfolio project.
