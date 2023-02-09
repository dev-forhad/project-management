using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using project_management_api.Data;
using project_management_api.Interface;
using project_management_api.Model;
using project_management_api.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Database Config
var server = builder.Configuration["DatabaseServer"] ?? "172.21.96.1";
var port = builder.Configuration["DatabasePort"] ?? "1433";
var user = builder.Configuration["DatabaseUser"] ?? "sa";
var password = builder.Configuration["DatabasePassword"] ?? "Forh@d123";
var database = builder.Configuration["DatabaseName"] ?? "UserManagement";

var connectionString = $"Server={server},{port}; Initial Catalog={database}; Integrated Security=False; TrustServerCertificate=True; Encrypt=false; User ID={user}; Password={password}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));


// For Identity  
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "http://localhost:4200",
        ValidIssuer = "http://localhost:61955",
        
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"))
    };
});


builder.Services.AddSwaggerGen(c =>
{
    

    // Add authentication to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Description = "JWT Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement();
    securityRequirement.Add(securityScheme, new[] { "Bearer" });
    c.AddSecurityRequirement(securityRequirement);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Resolve DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskInformationRepository, TaskInformationRepository>();


var app = builder.Build();

// add services to DI container


// Configure the HTTP request pipeline.



app.UseSwagger(c =>
{
    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
});


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();

// This middleware serves the Swagger documentation UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Login API V1");
    c.RoutePrefix = "api-docs";
    c.DocumentTitle = "Authentication And Authorization";
});


app.Run();
