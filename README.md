# Social Network Application

This project is a Social Network developed using **ASP.NET Core MVC** (.NET 8) following the **Onion Architecture** pattern.

## 📖 About the Project

This Social Network allows users to:

- **Login and Registration**:
  - User authentication and account creation with email activation.
  - Password reset functionality via email notification.
- **Posts (Home)**:
  - Users can create text posts, upload images, or share YouTube videos.
  - Posts support comments and replies.
  - Edit and delete own posts.
- **Friends Management**:
  - Add and remove friends.
  - View friends' posts and interact with them.
- **Profile Management**:
  - Update personal information, including uploading a new profile picture.
- **Security**:
  - Unauthorized access is restricted; users must be logged in to access posts, friends, and profile.

## 🖥️ Technologies Used

### Frontend
- HTML
- CSS
- Bootstrap
- ASP.NET Razor Pages

### Backend
- C# with ASP.NET Core MVC 8
- Microsoft Entity Framework Core
- Microsoft Entity Framework Core SQL Server
- Microsoft Entity Framework Core Tools
- Microsoft Entity Framework Core Design
- Entity Framework Code First

### Architecture
- Onion Architecture
- ViewModels and Fluent Validation
- AutoMapper
- Generic Repository and Service Pattern
- Email service through Shared Layer

### ORM
- Entity Framework Core

### Database
- SQL Server

## 📸 Project Images

### Login Screen
![Login Screen](https://github.com/Jaqz23/Social-Network-Application/blob/296fc23bca17d12d144cfb65972b5d49f96be8bc/images/login.png)

### Home Feed (Posts)
![Home Feed](https://github.com/Jaqz23/Social-Network-Application/blob/296fc23bca17d12d144cfb65972b5d49f96be8bc/images/home-feed.png)

### Friends List
![Friends List](https://github.com/Jaqz23/Social-Network-Application/blob/296fc23bca17d12d144cfb65972b5d49f96be8bc/images/Friends%20List.png)

### Profile Management
![Profile Management](https://github.com/Jaqz23/Social-Network-Application/blob/296fc23bca17d12d144cfb65972b5d49f96be8bc/images/profile-management.png)

## 📋 Prerequisites

Before running the Social Network app, make sure you have the following installed:

- Visual Studio 2022 or later
- ASP.NET Core 8
- SQL Server (local or remote)

## 🚀 Installation

1. **Download** the project or clone it.
   
2. **Open** the project in Visual Studio 2022.

3. **Configure** the database connection:
   - Open the `appsettings.Development.json` file.
   - Update the `Server` name to match your local environment, for example:

     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=YourServerName;"
       }
       // other configurations...
     }
     ```

4. In Visual Studio, go to:

   - **Tools > NuGet Package Manager > Package Manager Console**

5. In the **Package Manager Console**:
   - Make sure the Web Application project is selected as the **Default Project**.
   - You can select it from the dropdown menu in the Package Manager Console.

6. In the console, run the following command:

   ```bash
   Update-Database

7. Run the project:
    - Press F5 or click the Run button. The application will launch automatically in your default web browser.
