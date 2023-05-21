# Ecomerece( .Net7 and Angular )

#Section 2
 
* Creating the web API Project 
. To make new folder 
 - makdir ....
. To know the .Net Version you can click in command line
 - dotnet --info
. To know the command line for all .Net
 - dotnet -h
. To Create .Net Project by command line
- dotnet new sln -> create solution then click ls
- dotnet new webapi -n (API -> for name) 
- C:\Users\FGhd76\Desktop\Ecomerece(.Net&Angular)\Ecommerce>dotnet sln add API
Project `API\API.csproj` added to the solution.
- dotnet sln list known all list of the solutions in folder
. To run project and see the change dynamic as Angular
 - dotnet watch
 - Run project with ordinary way is dotnet run 
. To Make migration you should install dotnet ef--
- the command : dotnet tool install --global dotnet-ef --version 7.0.0
- dotnet ef migrations add 
 dotnet ef migrations add IntialCreate -o Data/Migrations
After that you need to run : dotnet ef database  update

. To Create class library project
- dotnet new classlib -n Infrastructure
. Add it to your solution
- dotnet sln add Core -> Infrastructure
. To add new Reference of your solution will be 
 - dotnet add reference ../Infrastructure

-----------------------------------------------------------------------------

#Section 3

The repository pattern :
1- Decouple business code from data access
2- Separation of concerns
3- Minimise duplicate query logic
4- Testability
5- Increase level of abstraction
6- Increased Maitainability , Flexiblity , Testability


