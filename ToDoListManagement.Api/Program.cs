using FluentValidation;
using ToDoListManagement.Api.Configurations;
using ToDoListManagement.Bll.DTOs;
using ToDoListManagement.Bll.Mappers;
using ToDoListManagement.Bll.Services;
using ToDoListManagement.Repository.Services;
using ToDoListManagement.Repository.Settings;

namespace ToDoListManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.ConfigureSerilog();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.ConfigureDataBase();
            builder.Services.AddScoped<IToDoListRepositoryAdoNet, ToDoListRepositoryAdoNet>(); // -------------
            builder.Services.AddScoped<IToDoListService, ToDoListService>(); // -------------
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddValidatorsFromAssemblyContaining<ToDoListCreateDto>();
            builder.Services.AddValidatorsFromAssemblyContaining<ToDoListGetDto>();
            builder.Services.AddSingleton(new TelegramHandler("7878522330:AAFjGSnpuJNbIpa1RwxBw2stsQwnSdmirf0", "1046201506"));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
