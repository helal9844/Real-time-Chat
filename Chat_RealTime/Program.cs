
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Chat_BL;
using Chat_DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Chat_BL.MiddleWare;

namespace Chat_RealTime;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        #region Services

        #region DefaultService
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        /*builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();*/
        #endregion

        #region DataBase
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));
        #endregion

        #region Hubs
        builder.Services.AddSignalR();
        #endregion

        #region AutoMapper
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        #endregion

        #region Repos
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Helpers
        builder.Services.AddScoped<ITokenService, TokenService>();
        #endregion

        #region Cores
        builder.Services.AddCors(p => p.AddPolicy("coresapp", builder => 
        {
            builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:4200");
        }));
        #endregion

        #region Authentications

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            { 
                var KeyFromConfig = builder.Configuration.GetValue<string>("TokenKey");
                var KeyInBytes = Encoding.ASCII.GetBytes(KeyFromConfig);
                var secretkey = new SymmetricSecurityKey(KeyInBytes);
                var signingcredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256Signature);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = secretkey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        #endregion

        #endregion
        var app = builder.Build();
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
            await Seed.SeedUsers(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An Error occurred during migration");
        }
        #region MiddleWares
        // Configure the HTTP request pipeline.
            
        app.UseMiddleware<ExceptionMiddleWare>();
        
        //app.UseHttpsRedirection();
        app.UseCors("coresapp");
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        await app.RunAsync();
        #endregion
    }
}