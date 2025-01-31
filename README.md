Technical assignment for Back-end developer in Alza.cz.

Simple api with 2 versions to manage products

Functionality:
- get all products (v1)
- get product by id (v1)
- update product's description (v1)
- get all products with pagination (v2)
  
Database used: MySQL

ORM used: Entity Framework

Documentation: Swagger

Prerequisites to run the projects:
- .NET 8.0
- PostgreSQL
- Rider or VS IDE
  
Before running application seed of database needs to be done: run entityframework migration and then update database

To do this use commands:
1.dotnet ef migrations add InitialSeed
2.dotnet ef database update

In connection string use your credentials(in appsettings.json)
