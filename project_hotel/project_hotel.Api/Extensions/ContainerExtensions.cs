using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using project_hotel.Api.Core;
using project_hotel.Application.UseCases.Commands;
using project_hotel.Application.UseCases.Queries;
using project_hotel.DataAccess;
using project_hotel.Domain;
using project_hotel.Implementation.UseCases.Commands;
using project_hotel.Implementation.UseCases.Queries;
using project_hotel.Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace project_hotel.Api.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<HotelContext>();
                var tokenStorage = x.GetService<ITokenStorage>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings, tokenStorage);
            });


            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        //Token dohvatamo iz Authorization header-a

                        var header = context.Request.Headers["Authorization"];

                        var token = header.ToString().Split("Bearer ")[1];

                        var handler = new JwtSecurityTokenHandler();

                        var tokenObj = handler.ReadJwtToken(token);

                        string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;


                        //ITokenStorage

                        ITokenStorage storage = context.HttpContext.RequestServices.GetService<ITokenStorage>();

                        bool isValid = storage.TokenExists(jti);

                        if (!isValid)
                        {
                            context.Fail("Token is not valid.");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }
        public static void AddUseCases(this IServiceCollection services)
        {
            //commands
            services.AddTransient<ICreateEquipmentCommand, EfCreateEquipmentCommand>();
            services.AddTransient<ICreateReservationCommand, EfCreateReservationCommand>();
            services.AddTransient<ICreateApartmentCommand, EfCreateApartmentCommand>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<ICreatePriceCommand, EfCreatePriceCommand>();

            services.AddTransient<IUpdateApartmentCommand, EfUpdateApartmentCommand>();
            services.AddTransient<IDeactivateApartmentCommand, EfDeactivateApartmentCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();

            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IDeleteReservationCommand, EfDeleteReservationCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();

            //queries
            services.AddTransient<IGetAuditLogsQuery, EfGetAuditLogsQuery>();
            services.AddTransient<IGetApartmentsQuery, EfGetApartmentsQuery>();
            services.AddTransient<IFindApartmentQuery, EfFindApartmentQuery>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetReservationsQuery, EfGetReservationsQuery>();

            //validators
            services.AddTransient<CreateEquipmentValidator>();
            services.AddTransient<CreateReservationValidator>();
            services.AddTransient<CreateApartmentValidator>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateApartmentValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<CreatePriceValidator>();
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accesor = x.GetService<IHttpContextAccessor>();
                var header = accesor.HttpContext.Request.Headers["Authorization"];

                var claims = accesor.HttpContext.User;

                if(claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonimousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Username").Value,
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value),
                    Username = claims.FindFirst("Username").Value
                };

                return actor;
            });
        }

        public static void AddHotelDbContext(this IServiceCollection services)
        {
            services.AddTransient(x =>
            {
                var optionBuilder = new DbContextOptionsBuilder();

                var conString = x.GetService<AppSettings>().ConnString;

                optionBuilder.UseSqlServer(conString);

                var options = optionBuilder.Options;

                return new HotelContext(options);
            });
        }
    }
}
