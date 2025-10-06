using System.Text.Json;

namespace TestWebAPI1.users;


public class UserManager
{
    private static UserManager? _instance;
    
    private readonly UserStore _userStore;

    private UserManager()
    {
        // parse users
        // todo handle exceptions
        _userStore = JsonSerializer.Deserialize<UserStore>(File.ReadAllText(Configuration.UserFileName()))!;
    }

    public static UserManager GetInstance()
    {
        _instance ??= new UserManager();

        return _instance;
    }

    public List<User> GetUsers()
    {
        return _userStore.Users.ToList();
    }

    public List<Admin> GetAdmins()
    {
        return _userStore.Admins.ToList();
    }

    public bool ExistsUser(string user)
    {
        return GetUsers().Contains(new User() {Name = user});
    }

    public bool AddUser(string user)
    {
        if (!ExistsUser(user))
        {
            var users = GetUsers();
            users.Add(new User() { Name = user });
            _userStore.Users = users.ToArray();
            UpdateStore();
            return true;
        }
        
        return false;
    }

    public bool RemoveUser(string user)
    {
        if (ExistsUser(user))
        {
            var users = GetUsers();
            users.RemoveAll(x => x.Name == user);
            UpdateStore();
            return true;
        }
        
        return false;
    }

    public bool ExistsAdmin(string admin)
    {
        return GetAdmins().Contains(new Admin() {Name = admin, Email = ""});
    }

    public bool AddAdmin(string admin,  string email)
    {
        if (!ExistsAdmin(admin))
        {
            var admins = GetAdmins();
            admins.Add(new Admin() { Name = admin,  Email = email });
            _userStore.Admins = admins.ToArray();
            UpdateStore();
            return true;
        }
        
        return false;
    }

    public bool RemoveAdmin(string admin)
    {
        if (ExistsAdmin(admin))
        {
            var admins = GetAdmins();
            admins.RemoveAll(x => x.Name == admin);
            UpdateStore();
            return true;
        }
        
        return false;
    }

    private void UpdateStore()
    {
        var json = JsonSerializer.Serialize(_userStore);
        File.WriteAllText(Configuration.UserFileName(), json);
    }
}