using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RESTAPI.Data;
using RESTAPI.Mappings;
using RESTAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---- DBContext Injected Start 
// ye DBContext class ko inject karega yani Database k saat Conection banaye ga or baad mai hm operation Perform Karenge query 
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))
);
//AddDbContext<NZWalksDbContext>: Here, you're registering a database context class named NZWalksDbContext with the dependency injection container. This allows you to use dependency injection to obtain an instance of NZWalksDbContext in your application
// ---- DBContext Injected Close

// Here we register or inject the Add AuthDBContext Service
builder.Services.AddDbContext<NZWalksAuthDBContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")
    )); 


//    this line add the Iregion Interface and thier implemention in Service <IRegionRepository, SQLRegionRepository> 
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

//   Add the IAddWalk repository dependency
builder.Services.AddScoped<IAddWalkRepository,SQLWalkRepository>();

// Add the IDifficulty repository 

builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();

// Injected Service for IJWTTokenRepository
builder.Services.AddScoped<IJWTTokenRepository, TokenRepository>();

/*
          this is the shorthand of this builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
      IRegionRepository a1 = new IRegionRepository();
  IRegionRepository a1 = new SQLRegionRepository( provide necessary constructor arguments );
 
 */

// First of all we need to add or inject automapper befor use otherwise we will not be able to access automapper
//Mapping Profile name ( )

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//----------------> ADD Authentication Service start

// 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")  // Replace with your React app's URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Now we Inject This line adds the Identity framework to your ASP.NET Core application.
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthDBContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;  
    options.Password.RequireNonAlphanumeric = false;    
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequiredLength = 6;

});

// this service is  used for authenticating users based on JWTs.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
  ValidateIssuer = true,
  ValidateAudience = true,
  ValidateIssuerSigningKey = true,
  ValidateLifetime  = true,
  ValidAudience = builder.Configuration["jwt:Audience"],
  ValidIssuer = builder.Configuration["jwt:Issuer"],
  IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
 
});
//----------------> ADD Authentication Service closed
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();