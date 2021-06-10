# Finances Core APP

<h1>
<img src="https://github.com/EvertonAlmeida/FinancesCore/tree/main/docs/FinancesCore">
<h1>

## Descripition

The **Finances Core APP** project aims to manage financial Incomings and outcomings by categories.

---
## ✋🏻 Prerequisites

* 
    - **[Visual Studio](https://visualstudio.microsoft.com/downloads/)** or **[Visual Studio Code](https://code.visualstudio.com/)**
    - **[.NET Core 3.1.x](https://dotnet.microsoft.com/download)**
    - Please check if you have installed the same runtime version (SDK) described in global.json
---

### 🚀 Technologies

* [ASP.NET with .NET Core](https://dotnet.microsoft.com/apps/aspnet) - A framework for building web apps and services with .NET and C#
* [ASP.NET MVC Core](https://dotnet.microsoft.com/apps/aspnet/mvc) - ASP.NET Core MVC provides a patterns-based way to build dynamic web applications
* [ASP.NET Identity Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) - Manages users, passwords, profile data, roles, claims, tokens, email confirmation, and more..
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - EF Core is an object-relational mapper (O/RM)
* [.NET Core Native DI](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection) - .NET Core supports the dependency injection (DI) software design pattern, which is a technique for achieving [Inversion of Control (IoC)](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion) between classes and their dependencies.
*  [AutoMapper](https://automapper.org/) - AutoMapper is a library built to map one object to another.
* [Fluent Validator](https://fluentvalidation.net/) - A popular .NET library for building strongly-typed validation rules


---
## 🔥 Run the project

1. Open a terminal;
2. Clone the repository `git clone https://github.com/EvertonAlmeida/FinancesCore.git`;
3. Go to the project folder `cd FinancesCore`;
4. Create the identity tables `dotnet ef database update -c FinanceCoreDbContext --project ./src/FinancesCore.app`;
5. Create the system tables `dotnet ef database update -c FinanceCoreDbContext --project ./src/FinancesCore.app`;
6. Open the project in Visual Studio Code `code .`;
7. Run the project in Visual Studio Code;
8. Register a user in the **Finances Core APP**
9. Run script bellow to associate the Clams to the user created.
    ```SQL
    USE [FinancesCore]
    GO
    IF EXISTS (Select top 1 Id from [dbo].[AspNetUsers])
    BEGIN
    INSERT [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue]) VALUES ((Select top 1 Id from [dbo].[AspNetUsers]), N'Category', N'Add, Edit')
    INSERT [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue]) VALUES ((Select top 1 Id from [dbo].[AspNetUsers]), N'Transaction', N'Add, Edit, Delete')
    END;
    GO
    ```
10.  Run de Inital script to create some data; 
        ```SQL
        USE [FinancesCore]
        GO
        INSERT [dbo].[Categories] ([Id], [CreatedAt], [UpdatedAt], [Title]) VALUES (N'6e0dce79-881a-4eeb-94bf-08d926a672a1', CAST(N'2021-06-03T12:44:37.4978016' AS DateTime2), CAST(N'2021-06-03T15:49:01.5956739' AS DateTime2), N'Services')
        INSERT [dbo].[Categories] ([Id], [CreatedAt], [UpdatedAt], [Title]) VALUES (N'134b5672-9ba0-4ee3-94c1-08d926a672a1', CAST(N'2021-06-03T12:45:08.8949262' AS DateTime2), NULL, N'Books')
        INSERT [dbo].[Categories] ([Id], [CreatedAt], [UpdatedAt], [Title]) VALUES (N'fd05d433-eed0-4eb1-257a-08d929e85ac2', CAST(N'2021-06-07T16:13:38.1471689' AS DateTime2), NULL, N'Sports')
        GO
        INSERT [dbo].[Transactions] ([Id], [CreatedAt], [UpdatedAt], [Title], [Value], [Type], [Active], [CategoryId]) VALUES (N'adf0b722-46e1-44ac-b9c7-08d929e83e06', CAST(N'2021-06-07T16:12:49.9592122' AS DateTime2), NULL, N'Dot Net core App', CAST(1000.00 AS Decimal(18, 2)), 1, 1, N'6e0dce79-881a-4eeb-94bf-08d926a672a1')
        INSERT [dbo].[Transactions] ([Id], [CreatedAt], [UpdatedAt], [Title], [Value], [Type], [Active], [CategoryId]) VALUES (N'5e6f464e-ae08-437d-b9c9-08d929e83e06', CAST(N'2021-06-07T16:17:26.2007387' AS DateTime2), NULL, N'Buiding Microservices with C#', CAST(250.00 AS Decimal(18, 2)), 2, 1, N'134b5672-9ba0-4ee3-94c1-08d926a672a1')
        GO
        ```

---

## Author

Developed by **Everton Almeida** 

---

## License


This project is licensed under the MIT license - see the [LICENSE.md](LICENSE.md) file for details

