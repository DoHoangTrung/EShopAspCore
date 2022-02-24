## Asp.net 5 MVC study 
## How to configure and run
-This project used:  
	visual studio 2019  
	.net 5.0  
	sql server 2019  
-create database:  
	set EshopAspCore.Data as start up project  
	go to appsettings.json file to config connection string  
	open package manager console -> set default project is EshopAspCore.Data  
	type: init-migration init  
	type: update-database  
	
	admin account : 
	username: trung123 
	password: Trung123$ 
	
-config BackendApi project:   
	appsettings.developement.json -> config your connection string  
	
-config AdminApp, Web:  
	appsettings.developement.json -> config baseUrl = BankendApi 's url  
## My note 
branch master-main only include README.md file  
branch develope - base: main  
branch feature/db_design - base: develope  

in EshopData: add nuget:    
	efcore.sqlserver  
	efcore.design  
	efcore.tool  
	Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation  

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
.net5 to present : project api auto intergrate swagger  

add jwt bearer to swagger  
https://www.c-sharpcorner.com/article/authentication-authorization-using-net-core-web-api-using-jwt-token-and/  

-add solution file : (logical file, it's not show in location folder)  
-fromform vs frombody : when use swagger   
fromquery : path?p=1  
if path/{id} : dont need from query(it will add a text value when use swagger)  

-16:   
bug: if we dont create user-content folder in wwwroot  
productimage cant save   
  
FileStorage : IwebhostEvirontment:  
https://stackoverflow.com/questions/68764432/how-to-access-iwebhostenvironment-in-class-library-net-5  

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
-45: 

-48: 
	product service 472: thumbnail = images.FirstOrDefault(x => x.IsDefault).FileUrl ==> ERROR  
sql server : tool -> profiler  

-49: fluent validation .net core  

-51: 
how to use mustache.js: 
1. download mustache.js : if we copy only code, we should set name : mustache.min.js (+ cdn ver)  
2. loop: data={"items": list_data}   
	Mustache.render(template, data)  
3. in template, we should see how object show in console to get exactly name (wrong: IdProduct >< right: idProduct)  

-55: 
deploy on IIS:  
https://stackoverflow.com/questions/62397386/swagger-ui-not-displaying-when-deploying-api-on-iis   
 
https://www.youtube.com/watch?v=Q_A_t7KS5Ss   
install .net core hosting bundle 5.0  
setting iis service  

1.database : console -> Script-Migration  
2. back end api  

cannot connect to db: create new login in sql server -> assign db and role  
run project in product environment(launching setting) -> debug  

-bug: get file image : (dont have url api string -appsettings in webapi)

bypass-invalid-ssl-certificate-in-net-core:  
https://stackoverflow.com/questions/38138952/bypass-invalid-ssl-certificate-in-net-core  
use when create client to call api  

-Email service:
https://xuanthulab.net/asp-net-core-gui-mail-trong-ung-dung-web-asp-net.html

error: https://stackoverflow.com/questions/59026301/sslhandshakeexception-an-error-occurred-while-attempting-to-establish-an-ssl-or 

##???
-cant login in first time after init migration (SOLVED)  
	https://entityframeworkcore.com/knowledge-base/60282522/cannot-login-to-seeded-custom-identityuser-from-passwordhasher 
	by default, identity login by NormalizedUserName   
-description of category in product category page  
-public language nav : return url (always home/index)?  
-razor page: layout = null  
-email == username 
-razor runtime compilation didnt work in production environment
open graph 
--binding checkbox : id + name

##Deloy note
Error 503: delete - republish project
Connection string: see database connection info

error 405 when delete put api
https://tedu.com.vn/kb/aspnet-core/loi-khong-delete-hay-put-duoc-tren-api-server-2.html

Email server:
create email account in plesk
See in for host, port... in http://webmail.galaptrinh.com
change SecureSocketOptions.StartTls --> None