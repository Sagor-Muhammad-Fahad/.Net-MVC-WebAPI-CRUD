# 🚀 ASP.NET Web API 2 with OAuth2 & File Upload

A robust **ASP.NET Web API 2** application built using **.NET Framework 4.x** and **Entity Framework Code First**. This project demonstrates role-based security using OWIN OAuth2 Bearer Tokens and handles complex multipart form data (JSON + Image Bytes) via a Custom `MediaTypeFormatter`.

---

## ✨ Key Features

- **🔐 OAuth2 Authorization & Authentication:** Integrated OWIN Middleware for issuing Bearer Access Tokens via `/token` endpoint with role-based access control (`admin`, `user`).
- **📁 Custom MediaTypeFormatter (`DataConverter`):** Efficiently handles `multipart/form-data` containing both serialized JSON (`Order`) and binary file streams (`byte[]` ImageFile).
- **🛍️ Order & OrderItem Management:** Complete CRUD operations supporting One-to-Many relational models with cascading deletes and image path handling.
- **🌐 CORS Enabled:** Configured using OWIN Cross-Origin Resource Sharing for seamless frontend integration (Angular/React/Vue).

---

## 🛠️ Tech Stack & Dependencies

- **Framework:** ASP.NET Web API 2 (.NET Framework 4.x)
- **ORM:** Entity Framework 6 (Code First)
- **Security:** OWIN OAuth2 Server (`Microsoft.Owin.Security.OAuth`)
- **JSON Handler:** Newtonsoft.Json

---

## 📁 Project Architecture

```text
├── Controllers/
│   └── OrdersController.cs         # Secured CRUD endpoints
├── Models/
│   ├── User.cs                     # Auth Model
│   ├── Order.cs & OrderItem.cs     # Main Domain Models
│   └── OrderDBContext.cs           # Entity Framework Context
├── Models/DTO/
│   ├── OrderRequest.cs             # Request Payload DTO
│   └── DataConverter.cs            # Custom Multipart Formatter
├── Providers/
│   └── AppAuth.cs                  # OAuth Authorization Server Provider
├── Repository/
│   └── UserRepo.cs                 # User Validation Layer
├── Startup.cs                      # OWIN Startup Configuration
└── WebApiConfig.cs                 # API Routing Configuration
