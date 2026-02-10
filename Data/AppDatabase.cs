using System.IO;
using Microsoft.Maui.Storage;
using SQLite;

public class AppDatabase
{
    private static SQLiteAsyncConnection? _database;

    public SQLiteAsyncConnection? Connection { get; internal set; }

    public AppDatabase()
    {
        // ensure the instance property is initialized
        Connection = _database ??= CreateConnection();
    }

    private static SQLiteAsyncConnection CreateConnection()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "pois.db");
        return new SQLiteAsyncConnection(dbPath);
    }
}
