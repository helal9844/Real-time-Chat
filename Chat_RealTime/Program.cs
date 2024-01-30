
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Chat_BL;
using Chat_DAL;
using Chat_DAL.UnitOfWork;

namespace Chat_RealTime;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        #region Services

        #region DefaultService
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
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

        #region Cores
        builder.Services.AddCors(p => p.AddPolicy("coresapp", builder => 
        {
            builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:4200");
        }));
        #endregion

        #endregion
        var app = builder.Build();
        #region MiddleWares
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.UseCors("coresapp");
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
        #endregion
    }
}