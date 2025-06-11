# LinkedIt API Application

## 📌 Overview
LinkedIt API is sophisticated, robust web application using cutting-edge technologies to provide a seamless user experience. The application adopts an **N-tier architecture**, leveraging ASP.NET Core 9, Entity Framework Core. The RESTful API ensures efficient communication, while JWT authentication enhances security, Make it **scalable, secure, and high-performance API** 

# Technologies
* ASP.NET Core 9
* Entity Framework Core
* RESTful API
* MS SQL Server
* JWT Authentication
* Swagger/Scalar/OpenAPI
* Identity for User Management

## 🏗 Project Architecture & Structure
LinkedIt API follows **robust N-tier architecture**, ensuring maintainability and scalability.

### **🔹 Layered Architecture**
- **API Layer** – Handles HTTP requests, controllers, and routing (**LinkedIt.API**).
- **Business/Domain Layer** – Encapsulates business logic / Manages **Entities**, **API Response** and **API Results** (**LinkedIt.Core**).
- **Data Access Layer** – Manages database operations (**MS SQL Server**) using the **Repository Pattern** (**LinkedIt.DataAccess**).
- **Service Layer** – Controller Services / Authentication Service With JWT / Uses **AutoMapper** for DTO-Entity conversion (**LinkedIt.Services**).

### **🔹 LinkedIt API Provide**
- **modular and scalable** project structure.  
- **Repository & Service pattern** for better code maintainability.  
- **ASP.NET Identity** for authentication & role-based authorization.  
- **AutoMapper** for DTO mapping.  
- **Repository Pattern** for Organizes data access logic.
- **Unit of Work Pattern** for Manages transactions.
- **Dependency Injection** for Enhances code modularity and testability.
- **Scalar & Swagger API documentation integration** for improved OpenAPI documentation.  

  ## 📂 Project Structure
```md

LinkedIt/
├── LinkedIt.API/                    (Presentation Layer)
│   ├── Controllers/
│   ├── appsettings.json
│   └── Program.cs
├── LinkedIt.Core/                   (Mixed Business/Domain Layer)
│   ├── Constants/
│   ├── DTOs/
│   │   └── [Entity Subfolders]/
│   ├── Enums/
│   ├── Mapper/
│   ├── Models/                      (Entities)
│   │   ├── Whisper/
│   │   └── WhisperTalk/
│   ├── Response/                    (API Response Models)
│   └── Results/                     (API Result Models)
├── LinkedIt.DataAccess/             (Data Access Layer)
│   ├── ApplicationDbContext.cs
│   ├── Migrations/
│   └── Repository/
│       └── Interfaces/
└── LinkedIt.Services/               (Service Layer)
    ├── ControllerServices/
    │   └── Interfaces/
    ├── JWTService/
    │   └── Interface/
    └── ServiceRegistration/
```

---

## **⚙ Key Features & Technologies**

### **🔐 Secure Authentication – JWT & Role-Based Authorization**
- **JWT-based authentication**.
- **Role-based access control**.
- **Custom PermissionAuthorizationHandler** for fine-grained security.

### **🔄 AutoMapper – DTO & Entity Mapping**
- **Automatically maps** API request DTOs to database entities.
- Keeps **controllers clean** and **reduces boilerplate code**.
- Prevents **exposing database structures** in API responses.

### **📑 API Documentation – Scalar UI & Swagger UI & OpenAPI**
- Auto-generated **Scalar documentation** and **Swagger documentation** for all endpoints.
- Interactive UI to test API methods easily.
- Simplifies **frontend and third-party integration**.
  
---

## The RESTful APIs provide JSON responses. Key APIs include:

### User Management and Authentication
* User registration and login.
* Link and unlink other users.
* Check if one user is linking another.

### Signal Management
* Create, update, and delete signals.
* UpVote and DownVote signals.
* Comment and ReSignal signals.

### Whisper With PhantomSignals Management
* Whisper Mutual Linking Users.
* Whisper with Old/New Signals.
* Users Talks On Wisper.




