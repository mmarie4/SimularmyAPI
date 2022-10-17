using Commands.RegisterPlayer;
using Domain.Options;
using Domain.Repositories;
using Domain.Repositories.Abstractions;
using Domain.Repositories.Core;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Queries.GetPlayerByEmail;
using SimularmyAPI.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
{
    builder.WebHost.UseUrls("http://0.0.0.0:5001");
    builder.Configuration.AddJsonFile("SimularmyAPI/appsettings.json");
}

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ExceptionFilter>())
                .AddNewtonsoftJson(options =>
                 {
                     options.SerializerSettings.ContractResolver = new DefaultContractResolver
                     {
                         NamingStrategy = new SnakeCaseNamingStrategy()
                     };
                     options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                     options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                 });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SecurityOptions>(builder.Configuration.GetSection("SecurityOptions"));
builder.Services.AddMediatR(typeof(RegisterPlayerCommand).Assembly);
builder.Services.AddMediatR(typeof(GetPlayerByEmailQuery).Assembly);

// Db context and repositories
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();

// Configure JWT authentication.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["SecurityOptions:Issuer"],
          ValidAudience = builder.Configuration["SecurityOptions:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecurityOptions:Secret"])),
          RequireExpirationTime = false
      };

      options.Events = new JwtBearerEvents
      {
          OnAuthenticationFailed = context =>
          {
              if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
              {
                  context.Response.Headers.Add("Token-Expired", "true");
              }
              return Task.CompletedTask;
          }
      };
  });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.Run();
