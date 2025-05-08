# **SafeVault - ASP.NET Core Identity Application**

🚀 Secure authentication system with **ASP.NET Core Identity** supporting **Role-Based Access Control (RBAC)**.

## **📌 Features**

✅ **User Authentication** (Login, Logout, Registration)  
✅ **Role-Based Access Control (RBAC)** (`Admin` & `User` roles)  
✅ **Secure Password Hashing**  
✅ **InMemory Database for Testing**  
✅ **Identity UI Integration**

## **🔧 Setup Instructions**

### **1️⃣ Clone Repository**

```sh
git clone https://github.com/your-repo/SafeVault.git
cd SafeVault
```

### **2️⃣ Install Dependencies**

```sh
dotnet restore
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.UI
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

### **3️⃣ Run the Application**

```sh
dotnet run
```

App will start at **`http://localhost:5000`**.

---

## **🛠️ Database Seeding**

- The app seeds two users:  
  🔹 `admin@example.com` → **Admin Role**  
  🔹 `user@example.com` → **User Role**
- Modify `Program.cs` to add custom roles/users.

---

## **🔐 Role-Based Access Control**

| Role      | Permissions            |
| --------- | ---------------------- |
| **Admin** | Access Admin Dashboard |
| **User**  | Access User Dashboard  |

👤 **Restrict Controller Actions:**

```csharp
[Authorize(Roles = "Admin")]
public IActionResult AdminDashboard() { return View(); }

[Authorize(Roles = "User")]
public IActionResult UserDashboard() { return View(); }
```

---

## **🧪 Running Tests**

### **1️⃣ Install Testing Dependencies**

```sh
dotnet add package NUnit
dotnet add package Moq
dotnet add package Microsoft.NET.Test.Sdk
```

### **2️⃣ Run Tests**

```sh
dotnet test
```

✅ **Includes:**

- Authentication tests (`Login`, `Logout`)
- Role-based access control tests (`Admin/User`)

---

## **📜 License**

MIT License - Feel free to modify & contribute! 🚀

---

### **💬 Need Help?**

If you run into issues, open a GitHub issue or contact me. Happy coding! 🚀

---
