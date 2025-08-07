# Library Management System (ASP.NET Core MVC + SQLite)

## Project Overview
This project is the **Library Management System** for **CSCI 6809: Project (a)**, built using **ASP.NET Core MVC** and **Entity Framework Core**, with **SQLite** as the database backend. The system enables full **CRUD (Create, Read, Update, Delete)** functionality for managing books, authors, library branches, and customers.

### **Enhancements**
- The **Book** model has been extended to include a **Genre** field.
- All database operations are handled via **Entity Framework Core**, ensuring data integrity and ease of querying.

---

## 📂 Project Structure
```
📂 LibraryManagement
 ┣ 📂 Controllers
 ┃ ┣ 📜 BookController.cs
 ┃ ┣ 📜 AuthorController.cs
 ┃ ┣ 📜 CustomerController.cs
 ┃ ┣ 📜 HomeController.cs
 ┃ ┗ 📜 LibraryBranchController.cs
 ┣ 📂 Models
 ┃ ┣ 📜 Book.cs  (✅ Includes Genre field)
 ┃ ┣ 📜 Author.cs
 ┃ ┣ 📜 Customer.cs
 ┃ ┣ 📜 ErrorViewModel.cs
 ┃ ┗ 📜 LibraryBranch.cs
 ┃ ┣ 📜 LibraryDbContext.cs   
 ┣ 📂 ViewModels
 ┃ ┣ 📜 BookViewModel.cs
 ┃ ┣ 📜 AuthorViewModel.cs
 ┃ ┣ 📜 CustomerViewModel.cs
 ┃ ┗ 📜 LibraryBranchViewModel.cs
 ┣ 📂 Views
 ┃ ┣ 📂 Book
 ┃ ┃ ┣ 📜 Index.cshtml
 ┃ ┃ ┣ 📜 Details.cshtml
 ┃ ┃ ┣ 📜 Delete.cshtml 
 ┃ ┃ ┣ 📜 Create.cshtml
 ┃ ┃ ┗ 📜 Edit.cshtml
 ┃ ┣ 📂 Author
 ┃ ┃ ┣ 📜 Index.cshtml
 ┃ ┃ ┣ 📜 Details.cshtml
 ┃ ┃ ┣ 📜 Delete.cshtml  
 ┃ ┃ ┣ 📜 Create.cshtml
 ┃ ┃ ┗ 📜 Edit.cshtml
 ┃ ┣ 📂 Customer
 ┃ ┃ ┣ 📜 Index.cshtml
 ┃ ┃ ┣ 📜 Details.cshtml
 ┃ ┃ ┣ 📜 Delete.cshtml  
 ┃ ┃ ┣ 📜 Create.cshtml
 ┃ ┃ ┗ 📜 Edit.cshtml 
 ┃ ┣ 📂 LibraryBranch
 ┃ ┃ ┣ 📜 Index.cshtml
 ┃ ┃ ┣ 📜 Details.cshtml
 ┃ ┃ ┣ 📜 Delete.cshtml  
 ┃ ┃ ┣ 📜 Create.cshtml
 ┃ ┃ ┗ 📜 Edit.cshtml 
 ┃ ┣ 📂 Home
 ┃ ┃ ┣ 📜 Index.cshtml
 ┃ ┃ ┣ 📜 Privacy.cshtml 
 ┣ 📂 Database
 ┃ ┣ 📜 library.db
 ┃ ┗ 📂 Migrations
 ┣ 📜 Program.cs
 ┣ 📜 appsettings.json
 ┗ 📜 LibraryManagement.csproj
 ┣ 📜 README.md 
```

---

## **How to Run**

### **1️⃣ Install .NET SDK**
**Ensure .NET SDK is installed**
- Download: [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)

### **2️⃣ Navigate to the project directory**
```sh
cd LibraryManagement
```

### **3️⃣ Restore dependencies**
```sh
dotnet restore
```

### **4️⃣ Apply database migrations**
```sh
dotnet ef database update
```

### **5️⃣ Run the project**
```sh
dotnet run
```
**By default, the project runs on `http://localhost:5199`**

---

##  **Database Information**
- **Database Type:** SQLite
- **Database File Path:** `LibraryManagement/Database/library.db`
- **Tables:**
  - `Books` (✅ Includes `Genre` field)
  - `Authors`
  - `Customers`
  - `LibraryBranches`

### **Viewing Database Data**
1. **Using SQLite CLI**
   ```sh
   sqlite3 LibraryManagement/Database/library.db
   SELECT * FROM Books;
   ```
2. **Using DB Browser for SQLite**
   - Download: [https://sqlitebrowser.org/](https://sqlitebrowser.org/)
   - Open `library.db`, browse tables and records

---

## **Features**
✅ **Full MVC architecture**
✅ **Books, Authors, Library Branches, and Customers management**
✅ **Database CRUD operations**
✅ **SQLite-backed data persistence**
✅ **Genre field added to Books model**

---

## **Notes**
- Ensure `library.db` exists; if not, run `dotnet ef database update`.
- The project requires `.NET 9.0+`.
- The application should start successfully using `dotnet run`.



 **This project is fully implemented and ready for submission!**

