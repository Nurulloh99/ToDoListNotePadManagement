namespace ToDoListManagement.Repository.Settings;

public class SqlDbConnectionString
{
	private string connectionString;

	public string ConnectionString
	{
		get { return connectionString; }
		set { connectionString = value; }
	}

    public SqlDbConnectionString(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
