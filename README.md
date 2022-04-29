# Self-Service Facility Ticket System

Power App that allows employees to independently review facility service tickets and create new tickets. The Power App will interface with a Web API running in Azure, which provides controlled access to a Azure SQL database.

![Solution Overview](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/architecture.png)

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

For the following modifications, make sure you have the following packages installed:
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.SqlServer
* Swashbuckle.AspNetCore

> Also make sure you have your ConnectionString configured with your credentials from [Create an Azure SQL database](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System#create-an-azure-sql-database)

![apiGeneralConfig](https://github.com/juliajuju93/Self-Service-Facility-Ticket-System/blob/main/pictures/apiGeneralConfig.gif)

## Configure your HTTP request pipeline
In the following, we will create our GET, POST, PUT, DELETE methods. HTTP methods allow to make particular type of calls to servers (in our case our Azure SQL database). Web APIs help to support complex operations and accessing data.