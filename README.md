## Asp.net 5 MVC study 
## Technologies
## Youtube tutorial
https://www.youtube.com/playlist?list=PLRhlTlpDUWsyN_FiVQrDWMtHix_E2A_UD
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
- call api from call admin app
	service.addhttpclient()
	if you want to use validation -> config fluent validation 
	
-cookie authentication without identity

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option=>
                {
                    option.LoginPath = "/User/login";
                    option.AccessDeniedPath = "/User/Forbidden";
                });
				
app.UseAuthentication(); befor routing() and authorization()

--get list user 
create userviewModel: entity appuser mustnt has email

-25: recompile razor page runtime

-28: override on action execute (base controller)

-30: pagination view component .net core

-35: set default language:
	defaut language id: app setting (common) -> set it to session (when login)
	add select language on layout page -> new form
	move navigation in layout to component view 
	
	create model : navagation
		current language
		list language
		
	create language view model: 
		
	get all language ()
	
	when onchange select, post language method, if it's successfull, redirect to action
	
	//optimize code
	create base apiclient (protected constructor)
	DI when call api: generic type function return, parameter Url
	for post call: use multiple generic type
	
	when select lg ->submit form
-when have both inheritant and interface implement: parent class , then interface

-36: Admin product list
	product controller
	get product paging
	
	manage product request must have a language id for show language
	in product service, get product view by language id
	
-api swagger didnt run: Fetch errorundefined /swagger/v1/swagger.json
https://www.thecodebuzz.com/resolved-failed-to-load-api-definition-undefined-swagger-v1-swagger-json/
	missing [http get]
	
-37:create product with file upload
	create() controller
	-> need view() : model: create product request
	->form encrype
	-> post create function()
	-> api client
	-> backend api product controller : create

	view: upload file -> send to controller (Iformfile)-> using form encrype
	send Iformfile to backend api -> convert to binary 
	-> do the same as other feature

	service product:
		get token session
		get client
		base address
		authorize
		
		MultipartFormDataContent()
		convert thumbnail to binary
	
-38: CK editor

	download or cdn 
	
	put in script section
	
	add id for field
	https://ckeditor.com/docs/ckeditor5/latest/features/html-embed.html#configuration

-39: filter product 

	create select in form get product/index
	get all category -> send to select through viewbag
	
	
-why identity cookie still work after close browser .net core
https://stackoverflow.com/questions/24530362/persistent-cookie-being-deleted-on-browser-close-identity-2-0
https://stackoverflow.com/questions/31946582/how-ispersistent-works-in-owin-cookie-authentication/46659752#46659752

change default setting of chrome 

it didnt work: because my chrome still open :v 

-42:
build multi-cultural Asp.Net (old) 
XLocalizer for Asp.Net Core (new) 

1.package: LazZiya.ExpressLocalization 
			LazZiya.TagHelpers
			

-43:
missing or declared service which is not nessessary can raise error (ex: ISlideService in WebApp need database connect)

-43:
use localize-content taghelper or <localize></>

##???
-cant login in first time after init migration