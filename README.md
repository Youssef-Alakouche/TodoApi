**TodoApi**

---

**Description:**

TodoApi is a simple project developed using .NET 8 and ASP.NET Core Web API. It provides a basic CRUD (Create, Read,
Update, Delete) functionality for managing todo items. The project utilizes Controller Base API for handling HTTP
requests and responses. Entity Framework Core is used for interacting with the SQL Server database. JWTBearer
authentication is implemented for securing the API endpoints.

---

**Setup Instructions:**

1. **Prerequisites:**
    - .NET 8 SDK
    - SQL Server
    - IDE (Visual Studio or Visual Studio Code)

2. **Clone the Repository:**
   ```bash
   git clone https://github.com/Youssef-Alakouche/TodoApi.git
   ```

3. **Database Configuration:**
    - Create a SQL Server database.
    - Update the connection string in `appsettings.json` file under `TodoApi` project to point to your SQL Server instance.

4. **Run Migrations:**
   ```
   dotnet ef database update
   ```

5. **Run the Application:**
   ```
   dotnet run
   ```

---

**Improvements Needed:**

1. **Login User Database Table:**
    - Implement a database table to store user information such as username, password hash, and other relevant details.
    - Use a secure hashing algorithm like bcrypt to hash passwords before storing them in the database.

2. **Configure JWTBearer Refresh Token:**
    - Implement a mechanism to issue refresh tokens along with access tokens during user authentication.
    - Set up token expiration and refresh token rotation policies to enhance security.

---

**Contributing:**

Contributions to TodoApi are welcome! If you have any suggestions, improvements, or bug fixes, feel free to open an
issue or submit a pull request.

---

**License:**

This project is licensed under the MIT License. See the `LICENSE` file for more details.

---


**Acknowledgments:**

Special thanks to the ASP.NET Core community and Microsoft for providing excellent documentation and resources for
building web APIs.
