using System.Text.Json.Serialization;

namespace TestWebAPI1.users;

public class User
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    protected bool Equals(User other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((User)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}

public class Admin : User
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }
}

public class UserStore
{
    [JsonPropertyName("admins")]
    public required Admin[]  Admins { get; set; }

    [JsonPropertyName("users")]
    public required User[]  Users { get; set; }
}