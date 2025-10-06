namespace TestWebAPI1.util.genericstore;

public interface GenericStoreBinder
{
    public int? GetIntValue(string group, string name);
    
    public long? GetLongValue(string group, string name);
    
    public float? GetFloatValue(string group, string name);
    
    public double? GetDoubleValue(string group, string name);
    
    public bool? GetBoolValue(string group, string name);
    
    public string? GetStringValue(string group, string name);

    public void SetValue(string group, string name, int value);
    
    public void SetValue(string group, string name, long value);
    
    public void SetValue(string group, string name, float value);
    
    public void SetValue(string group, string name, double value);
    
    public void SetValue(string group, string name, bool value);
    
    public void SetValue(string group, string name, GenericStoreType type, string value);

    public List<string> Groups();

    public List<(string, GenericStoreType)> GetGroup(string group);
    
    public void DeleteVariable(string group, string name);
    
    public void DeleteGroup(string group);
}