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
```
---

## 🖼️ Visual Walkthrough & Technical Proof

### 1. User Authentication & Security
The system uses **ASP.NET Core Identity** to manage user accounts and secure sensitive operations like posting reviews and voting.

| Registration Page | User Session |
|---|---|
| ![Register](docs/screenshots/register.jpg) | ![Authenticated Session](docs/screenshots/Review.jpg) |
*Citations:*

### 2. Book Management & Advanced Filtering
The main dashboard provides a clean interface for managing the library with real-time filtering capabilities.

![Books List and Ratings](docs/screenshots/BookList.jpg)
> **Featured**: Filtering by **Genre**, **Year**, and **Average Rating**.

### 3. Review & Voting System
Interactive engagement tools allowing users to share feedback and evaluate other users' contributions.

![Review and Voting System](docs/screenshots/Review.jpg)
*Citations:*

### 4. API Documentation (Swagger)
Complete REST API implementation with interactive documentation for easy integration.

![Swagger UI](docs/screenshots/Swagger.jpg)
*Citations:*

### 5. Backend & Data Integrity
Evidence of a clean **Code-First** implementation and secure database configuration.

| Database Schema | Migration History |
|---|---|
| ![SQL Server](docs/screenshots/image_6f13cc.png) | ![Migrations](docs/screenshots/image_6f0fcc.png) |
*Citations:*

> **Security Note**: Connection strings use **Trusted Connection** for secure local development.

### 6. Quality Assurance (Unit Testing)
Core business logic (like average rating calculations) is verified using **xUnit** tests.

![Unit Tests Results](docs/screenshots/image_eead3c.png)
*Citations:*
