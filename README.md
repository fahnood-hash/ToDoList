# ToDo List (ASP.NET MVC)

## Features

- User sign up and login
- Create, edit, and delete tasks
- Update task status (Pending, In Progress, Complete)
- Set due dates
- Assign categories (Work, Study, Personal, Urgent)
- Search tasks
- View dashboard with task summary

---

## Tech Stack

- ASP.NET MVC (C#)
- Entity Framework
- SQL Server
- HTML, CSS, Bootstrap
- JavaScript, jQuery

---

## 1. How to Run

```bash
git clone https://github.com/fahnood-hash/ToDoList.git
```

### 2. Open the project

- Open `ToDoList.sln` in Visual Studio  
- Allow NuGet packages to restore automatically  

### 3. Configure the database

Open `Web.config` and verify the connection string:

```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="YOUR_SQL_SERVER_CONNECTION"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

You may use **LocalDB** (default in Visual Studio).

### 4. Apply database migrations

Open Package Manager Console and run:

```powershell
Update-Database
```

This will create the required database schema.

### 5. Run the application

Press `Ctrl + F5` to start the application.

Access the app via:

```text
http://localhost:xxxx/Account/Signup
```

---

## Notes

- Username must not exceed 20 characters and must not contain spaces  
- Password must be at least 8 characters long and include:
  - One uppercase letter  
  - One numeric digit  
  - One special character  
