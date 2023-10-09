# UserWebApi

![Project Preview](https://github.com/SaBog/UserWebAPI/blob/master/images/preview.png)

- **ASP .NET WEB API (.NET 7)** + **EF Core** + **Swagger** + **JWT**
- Contains 2 tables User (Id, Name, Email, Age) and Roles (Id, Name) with many-to-many relations;
- 3 layer architecture (Controller - Service - Repository);
- Controller User supports CRUD operations;
- Pagination (elements on page  and offset);
- Filters and sotring by multiple properties;
- **/login** - to get JSON Web Token (JWT)  by email;
- **delete /api/users/{id}** - requires "Admin" or "SuperAdmin" role to execute;

# Setup

1. In Visual Studio open Package Manager Console and use command:
> Update-Database

2. In project explorer find **"appsettings.json"** and set your connection string.
3. Run the project
