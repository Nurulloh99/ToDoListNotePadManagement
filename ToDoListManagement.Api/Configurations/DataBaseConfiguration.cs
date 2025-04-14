using ToDoListManagement.Repository.Settings;

namespace ToDoListManagement.Api.Configurations;

public static class DataBaseConfiguration
{
    public static void ConfigureDataBase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DataBaseConnection");

        SqlDbConnectionString sqlDBConnectionString = new SqlDbConnectionString(connectionString);


        builder.Services.AddSingleton<SqlDbConnectionString>(sqlDBConnectionString);
    }
}
