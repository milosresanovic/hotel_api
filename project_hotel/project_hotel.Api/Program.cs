using Microsoft.OpenApi.Models;
using project_hotel.Api.Core;
using project_hotel.Api.Extensions;
using project_hotel.Application.Emails;
using project_hotel.Application.Logging.Exceptions;
using project_hotel.Application.Logging.UseCases;
using project_hotel.DataAccess;
using project_hotel.Domain;
using project_hotel.Implementation;
using project_hotel.Implementation.Emails;
using project_hotel.Implementation.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region My
var settings = new AppSettings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddApplicationUser();
builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
builder.Services.AddJwt(settings);


builder.Services.AddHotelDbContext();
builder.Services.AddUseCases();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();
builder.Services.AddTransient<IUseCaseLogger, DataBaseUseCaseLogger>();



builder.Services.AddTransient<IEmailSender>(x => new SmtpEmailSender(settings.EmailOptions.FromEmail,
                                                                     settings.EmailOptions.Password,
                                                                     settings.EmailOptions.Port,
                                                                     settings.EmailOptions.Host));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Car Rental API",
        Version = "v1",
        Description = "An API to perform Renting Cars operations",
        Contact = new OpenApiContact
        {
            Name = "Milos Resanovic",
            Email = "milos.resanovic.7.20@ict.edu.rs",
            //Url = new Uri("https://twitter.com/jwalkner%22),
        },
        License = new OpenApiLicense
        {
            Name = "Employee API LICX",
            Url = new Uri("https://example.com/license%22"),
            }
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token with Bearer formatk like Bearer[space] token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{ }
        }
    });
});

#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
