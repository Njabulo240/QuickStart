# **Matech QuickStart** - ASPNET Core 8 & Angular 17 

**ASP.NET Core / Angular** starting point project with SOLID Architecture, Advanced Authentication & Authorization, user and role management, and other useful services for **Quick Application Development**.

🚀The mission of developing an ASP.NET Quick Start Template is to empower developers by providing a comprehensive, efficient, and user-friendly foundation for building robust web applications. This template aims to streamline the development process, enhance productivity, and ensure best practices are adhered to from the outset. Our commitment is to facilitate rapid development while maintaining high standards of code quality, security, and performance.


💝If you've found Matech QuickStart helpful, kindly consider [becoming a sponsor](https://github.com/sponsors/njabulo240) . Even a small amount goes a long way to keep the project alive.

👍🏼As a [sponsor](https://github.com/sponsors/njabulo240) you will gain access to the private repos of **Matech QuickStart premium** which have more advanced features, and also priority email support.

[LIVE DEMO](https://matechdemo.azurewebsites.net)

___

## This application consists of:

*   Onion Architecture
*   RESTful API Backend using ASP.NET Core 8 Web API
*   Database using Entity Framework Core
*   Advanced Authentication/Authorization
*   Real-time push notification system (with SignalR integration)
*   Swagger integration
*   Angular Material and TypeScript


## Installation

* [OPTION 1] Clone the [Git Repository](https://github.com/njabulo240/QuickStart.git) and edit with your favorite editor. e.g. Visual Studio, Visual Studio Code.

* [OPTION 2] Download the Zip folder from github

When you open the downloaded zip file, you will see two folders

<img src="/img/14.png"/>

- **QuickStart** folder contains the server side ASP.NET Core solution and configured to work with Visual Studio.
- **QuickStartUI** folder contains the Angular UI application which is configured to work with the angular-cli.

### Merging Client and Server Solutions
Client and Server solutions are designed to work separately by default.


## Deploy Database 

#### Connection String
Open appsettings.json in QuickStart project and change the Default connection string if you want:

    ```json
    {
       "ConnectionStrings": {
        "sqlConnection": "server=.; database=QuickStartDb; Integrated Security=true;TrustServerCertificate=true"
      },
    }
    ```

### Migrations

Use Entity Framework Core's built-in tools for migrations. Open Package Manager Console in Visual Studio, set QuickStart as the Default Project and run the Update-Database command as shown below:

<img src="/img/16.png" />

This command will create your database. Initial data will be inserted when you run the QuickStart project. You can open SQL Server Management Studio to check if database is created:

<img src="/img/11.png" />

You can use EF console commands for development and Migrator.exe for production. But notice that; Migrator.exe supports running migrations in multiple databases at once, which can be useful in development/production for multi tenant applications.

### Run API Host
Once you've done the configuration, you can run the application. Server side application only contains APIs. When you start the application you will see a login page like below:

<img src="/img/17.png" />

## Login

LOGIN WITH USERNAME OR EMAIL ADDRESS
> * **Default Administrator Account**
>   * Username: admin001
>   * Email:    admin001@matech.com
>   * Password: AdminPassword123
> * **Default Standard Account**
>   * Username: user002
>   * Email:    user002@matech.com
>   * Password: UserPassword123

*	For bug reports open an [issue on github](https://github.com/njabulo240/QuickStart/issues)

### Angular Application

### NPM Packages Install
    ```bash
        npm install
    ```
it will install the packages. Make sure that there is no error.


### How to Run?

After install process now you can run local server- local server port is 'http://localhost:4200' For development start use this commend ng serve

    ```bash
        ng serve
    ```

## Hosting Angular App Inside ASP.NET API

To merge and host the Angular application inside the ASP.NET Core API, follow these steps:

1. **Build the Angular Application**:
    - Navigate to the `QuickStartUI` folder.
    - Run `ng build --prod` to build the Angular application. This will create a `dist` folder containing the compiled Angular files.

2. **Move Built Files to ASP.NET Core Project**:
    - Copy the contents of the `dist` folder.
    - Paste these contents into a new folder named `wwwroot` inside the `QuickStart` folder. The `wwwroot` folder is the default location for static files in an ASP.NET Core application.

3. **Configure ASP.NET Core to Serve Angular App**:
    - Open the `Program.cs` file in the `QuickStart` folder.
    - Ensure that the `Configure` method is set up to serve static files and handle fallback routing for the Angular application:
    
    ```csharp
        var builder = WebApplication.CreateBuilder(args);

        app.UseRouting();
        app.MapFallbackToController("Index", "Fallback");

    ```

4. **Run the Combined Solution**:
    - Open the `QuickStart` solution in Visual Studio.
    - Run the solution. The ASP.NET Core API should now serve the Angular application.

By following these steps, you will successfully host the Angular application inside the ASP.NET Core API, creating a unified solution.



## Documentation

*   [Overview of Matech QuickStart](https://www.matechcoding.com)
*   [Conceptual overview of what is ASP.NET Core](https://go.microsoft.com/fwlink/?LinkId=518008)
*   [Working with Data](https://docs.microsoft.com/en-us/ef/#pivot=efcore)
*   [Angular 17 documentation overview](https://angular.io/guide/quickstart)
*   [Getting started with Angular CLI](https://cli.angular.io)
*   [Angular Material](https://material.angular.io)


## Contribution

Matech QuickStart is actively maintained on [GitHub](https://github.com/njabulo240/QuickStart). You can support it by
*   Encouraging the developers by [starring it](https://github.com/njabulo240/QuickStart)
*   Submitting your changes/improvements/features using pull requests
*   Suggesting ideas or areas of improvements
*   Linking to it and recommending it to others


## License

Released under the [MIT License](https://github.com/Njabulo240/QuickStart/blob/master/LICENSE).

