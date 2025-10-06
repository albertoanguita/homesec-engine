using Dapper;
using MySqlConnector;
using TestWebAPI1.util.genericstore.binders;

namespace TestWebAPI1.util.filestore;


/*
 *


CREATE TABLE test.FileStore (
	Id BIGINT UNSIGNED auto_increment NOT NULL,
	`Path` varchar(1024) NOT NULL,
	`Size` BIGINT UNSIGNED NOT NULL,
	`Data` BLOB NOT NULL,
	CONSTRAINT FileStore_PK PRIMARY KEY (Id),
	CONSTRAINT FileStore_Path_UNIQUE UNIQUE KEY (`Path`)
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4;
CREATE INDEX FileStore_Path_IDX USING BTREE ON test.FileStore (`Path`);




 */
public class DbFileStore
{
    public class FileStoreEntity
    {
        public ulong Id { get; set; }
        public required string Path { get; set; }
        public ulong Size { get; set; }
        public required byte[] Data { get; set; }
    }
    
    private const string _id = nameof(FileStoreEntity.Id);
    private const string _path = nameof(FileStoreEntity.Path);
    private const string _size = nameof(FileStoreEntity.Size);
    private const string _data = nameof(FileStoreEntity.Data);
    
    // 1st version: no directories or groups
    private class DirInfo
    {
        public string Name { get; set; }
        public DirInfo Parent { get; set; }
        public string AbsolutePath { get; set; }
        public long CreatedAt { get; set; }
    }
    
    private class FileInfo : DirInfo
    {
        public long Size { get; set; }
        public byte[] Data { get; set; }
        public long ModifiedAt { get; set; }
    }
    
    private const char PathSeparator = '/';
    
    private string _connectionString;
    
    private readonly string _table;
    
    private readonly DirInfo _root;

    public DbFileStore(string connectionString, string table)
    {
        _connectionString = connectionString;
        _table = table;
        // todo ensure root dir is created
        _root = new DirInfo { Name = _table };
    }

    public void Create(string drive, string path, byte[] data, bool overwrite = false)
    {
        var (dirs, fileName) = ParsePath(path);
        
        var dir = EnsureDirsCreated(drive, dirs);
    }

    private DirInfo EnsureDirsCreated(string drive, List<string> dirs)
    {
        return null;
        if (dirs.Count == 0)
            return _root;
        
        var parent = _root;
        var currentDir = dirs.First();
        var absolutePath = PathSeparator + currentDir;
        
        var dirInfo = GetDir(drive, absolutePath);
        
        if (dirInfo == null)
            dirInfo = CreateDir(drive, parent, currentDir, absolutePath);

        foreach (var dir in dirs.Skip(1))
        {
            parent = dirInfo;
            currentDir = dir;
            absolutePath = absolutePath + PathSeparator + currentDir;
        }

        // (DirInfo, string parent, string currentDir, string absolutePath) GetNextDir(string path)
        // {
        //     
        // }
        
    }

    private DirInfo CreateDir(string environment, DirInfo parent, string currentDir, string absolutePath)
    {
        // todo add to db
        return new DirInfo
        {
            Parent = parent,
            AbsolutePath = absolutePath,
            CreatedAt = DateTime.UtcNow.Millisecond,
            Name = currentDir
        };
    }

    private DirInfo? GetDir(string environment, string absolutePath)
    {
        return null;
    }

    private static (List<string> dirs, string fileName) ParsePath(string path)
    {
        if (path.EndsWith(PathSeparator))
            throw new ArgumentException($"File path cannot end with {PathSeparator}, received {path}");
        
        var items = path.Split(PathSeparator);
        
        if (items.Length == 0)
            throw new ArgumentException($"Invalid file path received: {path}");
        
        var fileName = items[^1];

        var dirs = items.ToList();
        dirs.RemoveAt(dirs.Count - 1);
        
        return (dirs, fileName);
    }

    private FileStoreEntity? Load(string path)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var sql = $"SELECT * FROM {_table} WHERE {_path} = @path";
        var file = con.QueryFirstOrDefault<FileStoreEntity>(sql, new { path });
        return file;
    }

    private void Save(string path, byte[] data)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        con.Execute($"INSERT INTO {_table}({_path}, {_size}, {_data}) values (@path, @size, @data)", 
                new { path, data.Length, data });
    }

    private ulong? Size(string path)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var sql = $"SELECT Size FROM {_table} WHERE {_path} = @path";
        var size = con.QueryFirstOrDefault<ulong?>(sql, new { path });
        return size;
    }

    private bool Exists(string path)
    {
        return Size(path).HasValue;
    }

    private void Delete(string path)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var sql = $"DELETE FROM {_table} WHERE {_path} = @path";
        con.Execute(sql,new { path });
    }
}