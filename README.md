# Self-Service Facility Ticket System

Power App that allows employees to independently review facility service tickets and create new tickets. The Power App will interface with a Web API running in Azure, which provides controlled access to a Azure SQL database.

![Solution Overview](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/architecture.png)

Situation:
* This Fusion scenario is based on a request from an insurance company, but can be used in many different industries.
* A simple and scalable solution was developed to make it easier to handle facility management requests from employees.
* Facility Management, as the business stakeholder, approached the IT department for a suitable solution that they could customize and maintain themselves.

Solution:
* This solution has 2 interfaces, one for all employees (non-admin interface) that any employee can create and manage ticket requests through. One admin interface where requests can be managed individually.
* Due to the high number of ticket requests, a SQL database managed by API queries was used.
* API Management as a secure and consistent data strategy was implemented and supported with a simple and easy generation of custom connectors for Power Apps.



## What you need?
* Power Apps environment - [Get started with Power Apps canvas apps](https://docs.microsoft.com/en-us/learn/modules/get-started-with-powerapps/)
* Azure SQL database - [Getting started with single databases in Azure SQL Database](https://docs.microsoft.com/en-us/azure/azure-sql/database/quickstart-content-reference-guide?view=azuresql)
* Visual Studio .NET 6 web API - [Create a minimal web API with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio)
* API Management - [About API Management](https://docs.microsoft.com/en-us/azure/api-management/api-management-key-concepts)

## Create an Azure SQL database
![CreateAzureSQLdatabase](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/createAzureSQLDatabase.gif)

> Please make sure to note down your server admin login data and password

Check out our [Quickstart: Create an Azure SQL Database single database](https://docs.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&tabs=azure-portal) for more detailed information.

## Modify your SQL database
First you need to create a database table
Run the following query for this:
```
CREATE TABLE dbo.facilityrequests
(
[id] int IDENTITY(1,1) NOT NULL,
[id_status] varchar(30) NULL,
[id_type] varchar(30) NULL,
[id_requestor] varchar(30) NULL,
[id_requestor_email] varchar(100) NULL,
[id_requestor_department] varchar(30) NULL,
[id_requestor_phone] varchar(30) NULL,
[id_assignment] varchar(100) NULL,
)
```
![CreateTable](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/createtable.gif)

Next you will add rows to your database, so you will have content in your database.
Run the following query for this:
```
INSERT INTO [dbo].[facilityrequests]
       ( [id_status]
       , [id_type]
       , [id_requestor]
       , [id_requestor_email]
       , [id_requestor_department]
       , [id_requestor_phone]
       , [id_assignment]
       )
VALUES
       ('OPEN'
       ,'Mail Room'
       ,'Julia Test'
       ,'julia@contoso.com'
       ,'Dev Div'
       ,'+1000000000'
       ,'julia@contoso.com'
       );
```
![CreateRows](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/addRows.gif)

## Create your minimal web API with ASP.NET Core 
For this we are using a Visual Studio web API template. This will automatically create a base structure for our scenario. Based on the template structure, we will then build our API logic.

![minimalAPIVSnew](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/minimalAPIVSnew.gif)

For the following modifications, make sure you have the following [NuGet packages](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#add-nuget-packages) installed:
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.SqlServer
* Swashbuckle.AspNetCore

> Also make sure you have your ConnectionString configured with your credentials from [Create an Azure SQL database](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System#create-an-azure-sql-database)

![apiGeneralConfig](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/apiGeneralConfig.gif)

## Add your model and database context classes
Find more general information about the minimal API context [here. - Minimal APIs overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0). Or check out our [Tutorial: Create a minimal web API with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio).

Our fusion scenario contains the following model:
```
class FacilityRequest
{
    [Column("id")]
    public int Id { get; set; }
    [Column("id_status")]
    public string? IdStatus { get; set; }
    [Column("id_type")]
    public string? IdType { get; set; }
    [Column("id_requestor")]
    public string? IdRequestor { get; set; }
    [Column("id_requestor_email")]
    public string? IdRequestorEmail { get; set; }
    [Column("id_requestor_department")]
    public string? IdRequestorDepartment { get; set; }
    [Column("id_requestor_phone")]
    public string? IdRequestorPhone { get; set; }
    [Column("id_assignment")]
    public string? IdAssignment { get; set; }

}
```

Our fusion scenario also contains the following database context class:
```
class FacilityRequetsDb : DbContext
{
    public FacilityRequetsDb(DbContextOptions<FacilityRequetsDb> options)
        : base(options) { }

    public DbSet<FacilityRequest> FacilityRequests => Set<FacilityRequest>();
}
```
![addModelDatabaseContext](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/addModelDatabaseContext.gif)


## Add your HTTP request pipeline
In the following, we will create our GET, POST, PUT, DELETE methods. HTTP methods allow to make particular type of calls to servers (in our case our Azure SQL database). Web APIs help to support complex operations and accessing data.

![httpPipeline](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/httpPipeline.gif)

> Have a look at our [Program.cs](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/program.cs) file for the final implementation.

## Publish to Azure API Management
As a next step, we will publish our API to Azure App Services and API Management.
Why API Management - it helps meet these challenges:
* Abstract backend architecture diversity and complexity from API consumers
* Securely expose services hosted on and outside of Azure as APIs
* Protect, accelerate, and observe APIs
* Enable API discovery and consumption by internal and external users