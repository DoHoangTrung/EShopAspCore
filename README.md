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
-tạo tầng application:
	+SOLID: D(dependencies inject)
	+tạo 2 interface product: admin (manage) vs web(public)
	+moi C, U, R, D co mot DTO
	
	
-create api 
add connection string to appsetting.developement.json

config DI (dependence inject dot net core)

-add swagger
.net5 to now : project api auto intergrate swagger

-add solution file : (logical file, it's not show in location folder)
-fromform vs frombody : when use swagger 
fromquery : path?p=1
if path/{id} : dont need from query(it will add a text value when
use swagger)
-bug: if we dont create user-content folder in wwwroot
productimage cant save

--feature/login
create system/users folder
register request has fields that app user need
define constructor for userservice 
UserService: authenticate use all async method
	should rename it to au...async
	use iconfiguration: (add attribute tokens in app.json)
	
	services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<EshopDbContext>()
                .AddDefaultTokenProviders();
--add authorization header bearer in swagger UI
	add app.authentication() 
	
- fluent validation .net core