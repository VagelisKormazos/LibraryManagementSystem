# 📚 Library Management System

A modern, full-stack Web Application for managing a digital library, built with **.NET 9**. This project was developed as part of a technical assessment, focusing on clean architecture, RESTful services, and user engagement.

---

## 🚀 Features

### 💻 Web UI (MVC)
* **Book Management**: Full CRUD operations for the library's collection.
* **Advanced Filtering**: Search books by *Genre*, *Publication Year*, and *Average Rating*.
* **Interactive Reviews**: Users can write reviews (10-500 characters) and provide a star rating (1-5).
* **Voting System**: Authenticated users can Like or Dislike reviews, with a restriction of one vote per user.

### 🌐 REST API
* **Minimal APIs**: Optimized endpoints for high performance.
* **Endpoints**:
    * `GET /api/books` - Retrieve all books.
    * `POST /api/books` - Add a new book.
    * `POST /api/reviews` - Submit a review.
    * `POST /api/reviews/{id}/vote` - Cast a vote on a review.
* **Swagger Documentation**: Fully interactive API docs available at `/swagger`.

### 🛡️ Security & Quality
* **Identity**: Secure user registration and login system.
* **Unit Testing**: Core logic verified with **xUnit**, ensuring correct calculation of average ratings.
* **Data Integrity**: Handled multiple cascade paths and schema optimizations in Entity Framework Core.

---

## 🛠️ Tech Stack

| Layer | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core 9 (MVC & Minimal APIs) |
| **Database** | SQL Server with Entity Framework Core (Code-First) |
| **Authentication** | ASP.NET Core Identity |
| **Testing** | xUnit |
| **UI** | Bootstrap 5 & Razor Views |

---

## 🏁 Getting Started

### Prerequisites
* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* SQL Server (LocalDB)
* Visual Studio 2022 (latest version)

### Installation & Setup

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/your-username/library-management-system.git](https://github.com/your-username/library-management-system.git)
    cd library-management-system
    ```

2.  **Update Database**
    Ensure your connection string in `appsettings.json` is correct for your local SQL Server instance, then run:
    ```bash
    dotnet ef database update
    ```

3.  **Run the Application**
    ```bash
    dotnet run
    ```
    The application will be available at `https://localhost:5001` (or the port specified in your `launchSettings.json`).

---

## 🧪 Testing
To run the unit tests and verify the business logic:
```bash
dotnet test
