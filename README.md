# **Matech QuickStart** - ASPNET Core 8 & Angular 17 

Matech QuickStart is a cutting-edge **ASP.NET Core / Angular / Admin** Starter Project designed for **Quick Application Development**. It features a robust SOLID architecture, advanced authentication and authorization, comprehensive user and role management, and a suite of essential services. This powerful tool is engineered to accelerate your development process and elevate your projects to new heights.

🚀 The mission of developing our **ASP.NET & Angular Admin Template** is to empower developers with a comprehensive, efficient, and user-friendly foundation for building robust web applications. This template aims to streamline the development process, boost productivity, and ensure adherence to best practices from the outset. Our commitment is to facilitate rapid development while maintaining high standards of code quality, security, and performance.


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
To deploy the database, follow these steps:

## Open the Solution
1. Navigate to the **QuickStart** folder.
2. Locate the solution file.
3. Open the solution file, which will automatically launch Visual Studio.

 <img src="/img/18.png" />

### Configure Connection String
1. In the **QuickStart** project, open `appsettings.json`.
2. Modify the default connection string if necessary:

    ```json
    {
       "ConnectionStrings": {
        "sqlConnection": "server=.; database=QuickStartDb; Integrated Security=true;TrustServerCertificate=true"
      },
    }
    ```

### Apply Migrations
The application uses the Code First approach of Entity Framework Core. All migration code is located in the **QuickStart/Migrations** folder.

1. Open the **Package Manager Console** in Visual Studio.
2. Set **QuickStart** as the default project.
3. Run the following command to update the database:

    ```powershell
    Update-Database
    ```

  <img src="/img/16.png" />

This will apply the migrations and set up your database. 

After running the migrations, your database tables will be created as shown below:

  <img src="/img/11.png" />


### Run API Host

Once you've configured the application, you can run the server-side application. The server-side application contains only APIs. The API is running on [https://localhost:5001](https://localhost:5001). Paste this URL into your browser to access the application and you will see the login page below:

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
Drag and drop **QuickStartUI** folder into VS code IDE 

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
    - Run `ng build` to build the Angular application. This will create a `dist` folder containing the compiled Angular files.

2. **Move Built Files to ASP.NET Core Project**:
    - Copy the contents of the `dist` folder.
    - Paste these contents into a new folder named `wwwroot` inside the `QuickStart/QuickStart/wwwroot` folder. The `wwwroot` folder is the default location for static files in an ASP.NET Core application.

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

