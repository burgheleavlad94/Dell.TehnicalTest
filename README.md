# Dell Technical test: Customers application

The application purpose is to enable operators to add and/or update new or existing Customers storing following information: “Name”, “Age” and “E-mail address”; it has three components:
1. Database containing a single table (Customers) having above fields (columns), including an internal identifier (ID, auto-generated and incremental) and a Record versioning used for optimistic concurrently data access;
2. a (RESTful) Web API which enables potential client applications to store and retrieve stored information into and from database.
When one customer is added/updated, this returns a response with Customer fully populated and a generated customer ID.
3. Front-end application where operators will visualize and handle new or existing information.
## Prerequisites
For running the application you must following few steps:
-	Install MS Visual Studio (I used Visual Studio Community 2017);
-	Install MS SQL Server (I used Express edition); the solution contains the database;
-	Download the project from the GitHub site on you **C** or your chosen partition. 
## Installing & running the software
 For a better understanding, I've atached a video with the steps you need to follow, to run the application.                             
 You’ll find 2 solutions which must run in this order: first Dell.TehnicalTest.WebApi.sln and then Dell.TehnicalTest.WPF.sln.
## Running Dell.TehnicalTest.WebApi.sln:
- Open **Dell.WebApi** project, locate and open **appsettings.json** file;
- Modify in **Config --> Connections --> (localDb) ConnectionString** the path **E:\\repos** with your partition letter (e.g.: **C:\\**) and save the file;
- Same procedure as above for **Dell.TestCustomersDB** project (it has an identical json file); these operations will ensure locating database file for both API and test project.
- Press F5 / CTRL + F5 to run the solution; a new browser web page should appear displaying json information, having the following address **http://localhost:49911/api/customers**.

## Running Dell.TehnicalTest.WPF
Open this solution and press F5 / CTRL + F5 to run the solution after WebAPI is running
The database is pre-populated with few record which are loaded asynchronously into main window.
Please use “Add new” and “Update” for add new records or altering information of existing ones.
### Observations: 
when altering Customer information, the application doesn’t allow duplicated e-mail addresses (a message will be displayed: “Another customer is registered with same e-mail!”) but allows duplicated Customer Names.

## Running the tests
For running the tests you must:
-	open solution Dell.TehnicalTest.WebApi , folder Tests, find project Dell.TestCustomersDb; UnitTest1.cs class contains testing methods;
-	select Test menu, Windows and click Text explorer item;
-	the Text explorer window will be open on one side of your IDE (left probably);
-	click Run All and see the result.
Here we can test Insert and Update methods from CustomersDb class. We run this test to check the stored procedures execute, connections open, reading the parameters. The methods will return an integer “1” if the operation succeeded and “-1” if failed.
## Build with
- .Net Core/ C# , ASP.Net Core Web API;
- WPF (front end);
- MS SQL Server / T-SQL.
## Author 
**Vlad Burghelea**



