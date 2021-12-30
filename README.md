## Asp.net 5 MVC study 
## Technologies
## Youtube tutorial
## How to configure and run
## How to contribute
## My note
branch master-main only include README.md file
branch develope - base: main
branch feature/db_design - base: develope

in EshopData: add nuget: 
	efcore.sqlserver
	efcore.design
	efcore.tool

config table dbset:
	attribute configuration
	fluent api

migration database:
- branch feature/database_design:
	+ init appsettings.json -> add connection string
	+ add class dbcontext factory
	+ install nuget: configuration file + json
- migration:
	+add-migration initial
	+update-database
- create seeding data
-add class + config: IdentityUserClaim, IdentityUserRole, IdentityUserLogin, IdentityRoleClaim, IdentityUserToken
-seeding data: 