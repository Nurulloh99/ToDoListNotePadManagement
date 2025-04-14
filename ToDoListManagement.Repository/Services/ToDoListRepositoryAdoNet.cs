using System.Data;
using Microsoft.Data.SqlClient;
using ToDoListManagement.Dal.Entity;
using ToDoListManagement.Repository.Settings;

namespace ToDoListManagement.Repository.Services;

public class ToDoListRepositoryAdoNet : IToDoListRepositoryAdoNet
{
    private readonly string ConnectionString;
    public ToDoListRepositoryAdoNet(SqlDbConnectionString sqlDBConnectionString)
    {
        ConnectionString = sqlDBConnectionString.ConnectionString;
    }


    public async Task DeleteToDoItemAsync(long toDoItemId)
    {
        using (var conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("DeleteToDOList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", toDoItemId);
                cmd.ExecuteNonQuery();
            }
        }
    }




    public async Task<long> InsertToDoItem(ToDoItem toDoItem)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("AddToDoList", conn))
            {
                cmd.CommandType= CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Title", toDoItem.Title);
                cmd.Parameters.AddWithValue("@Discription", toDoItem.Description);
                cmd.Parameters.AddWithValue("@IsCompleted", toDoItem.IsCompleted);
                cmd.Parameters.AddWithValue("@CreatedAt", toDoItem.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoItem.DueDate);

                var res = (long)await cmd.ExecuteScalarAsync(); // Returns a new ID
                await conn.CloseAsync();

                return res;
            }
        }
    }


    public async Task<ICollection<ToDoItem>> SelectAllToDoItemsAsync(int skip, int take)
    {
        var ToDoItemsLists = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SelectAllToDoListPagenation", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Offset", skip);
                command.Parameters.AddWithValue("@PageSize", take);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ToDoItemsLists.Add(new ToDoItem
                        {
                            ToDoItemId = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return ToDoItemsLists;
    }

    public async Task<ICollection<ToDoItem>> SelectByDueDateAsync(DateTime selectedDate)
    {
        var ToDoItemsLists = new List<ToDoItem>();

        using (var conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetToDoListByDueDate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DueDate", selectedDate);
                using (var reader = cmd.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        ToDoItemsLists.Add(new ToDoItem
                        {
                            ToDoItemId = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return ToDoItemsLists;
    }




    public async Task<ICollection<ToDoItem>> SelectCompletedAsync(int skip, int take)
    {
        var ToDoItemsLists = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SelectAllCompletedToDoListPagenation", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Offset", skip);
                command.Parameters.AddWithValue("@PageSize", take);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ToDoItemsLists.Add(new ToDoItem
                        {
                            ToDoItemId = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return ToDoItemsLists;
    }




    public async Task<ICollection<ToDoItem>> SelectIncompleteAsync(int skip, int take)
    {
        var ToDoItemsLists = new List<ToDoItem>();

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SelectAllInCompletedToDoListPagenation", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Offset", skip);
                command.Parameters.AddWithValue("@PageSize", take);

                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ToDoItemsLists.Add(new ToDoItem
                        {
                            ToDoItemId = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return ToDoItemsLists;
    }

    public async Task<ToDoItem> SelectToDoItemByIdAsync(long toDoItemId)
    {
        using (var conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetToTOListByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", toDoItemId);
                using (var reader = cmd.ExecuteReader())
                {
                    return new ToDoItem
                    {
                        ToDoItemId = reader.GetInt64(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        IsCompleted = reader.GetBoolean(3),
                        CreatedAt = reader.GetDateTime(4),
                        DueDate = reader.GetDateTime(5),
                    };
                }
            }
        }
    }

    public async Task UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        using (var conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("UpdateToDoList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", toDoItem.Title);
                cmd.Parameters.AddWithValue("@Discription", toDoItem.Description);
                cmd.Parameters.AddWithValue("@IsCompleted", toDoItem.IsCompleted);
                cmd.Parameters.AddWithValue("@CreatedAt", toDoItem.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoItem.DueDate);

                cmd.ExecuteNonQuery();
            }
        }
    }
}


