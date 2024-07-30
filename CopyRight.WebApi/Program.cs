
using CopyRight.Bl.Interfaces;
using CopyRight.Bl;
using CopyRight.Dal;
using CopyRight.Dal.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CopyRight.Bl.Service;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// הגדרת אימות JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = TokenService.GetTokenValidationParameters();
});
builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("Admin", policy => policy.RequireClaim("Type", "Admin"));
    cfg.AddPolicy("Worker", policy => policy.RequireClaim("Type", "Worker"));
    cfg.AddPolicy("Customer", policy => policy.RequireClaim("Type", "Customer"));
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // הוספת הגדרת Authorization ל-Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CopyRightContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Host=dpg-cqkalq2ju9rs738jmrag-a.singapore-postgres.render.com;Port=5432;Database=copyrightdb;Username=copyrightdb_user;Password=RvXri1TomeRGZIq4MsJVzrmPrNGQvGj0;")));

//builder.Services.AddDbContext<CopyRightContext>(options => options.UseSqlServer("Server=.;Database=CopyRight;TrustServerCertificate=True;Trusted_Connection=True;"));


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<GoogleDriveService>();
builder.Services.AddScoped(typeof(IDocument), typeof(DocumentService));
builder.Services.AddScoped(typeof(CopyRight.Dal.Interfaces.IDocument), typeof(CopyRight.Dal.Service.DocumentService));
builder.Services.AddScoped(typeof(IUser), typeof(UserService));
builder.Services.AddScoped(typeof(CopyRight.Dal.Interfaces.IUser), typeof(CopyRight.Dal.Service.UserService));
builder.Services.AddScoped(typeof(ILead), typeof(LeadService));
builder.Services.AddScoped(typeof(ICustomer), typeof(CustomerService));
builder.Services.AddScoped(typeof(ITasks), typeof(TasksService));
builder.Services.AddScoped(typeof(CopyRight.Dal.Interfaces.ITasks), typeof(CopyRight.Dal.Service.TasksService));
builder.Services.AddScoped(typeof(IProject), typeof(ProjectService));
builder.Services.AddScoped(typeof(CopyRight.Dal.Interfaces.IProject), typeof(CopyRight.Dal.Service.ProjectService));
builder.Services.AddScoped(typeof(ICommunication), typeof(CommunicationService));
builder.Services.AddScoped(typeof(CopyRight.Dal.Interfaces.ICommunication), typeof(CopyRight.Dal.Service.CommunicationService));
builder.Services.AddScoped<DalManager>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.MapGet("/Copy-Right", () => "Copy Right!");

app.Run();
