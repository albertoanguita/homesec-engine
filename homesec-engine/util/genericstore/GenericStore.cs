namespace TestWebAPI1.util.genericstore;


public enum GenericStoreType : int
{
    Int = 1,
    Long = 2,
    Float = 3,
    Double = 4,
    Bool = 5,
    String = 6,
    IntArray = 7,
    LongArray = 8,
    FloatArray = 9,
    DoubleArray = 10,
    BoolArray = 11,
    StringArray = 12
}

public class GenericStore
{
    // todo add autobackup methods, print state method...
    private const string Separator = "/@-@/";
    
    private GenericStoreBinder _binder;

    private GenericStore? _backupStore;

    public GenericStore(GenericStoreBinder binder, GenericStore? backupStore = null, string listSeparator = Separator)
    {
        _binder = binder;
        _backupStore = backupStore;
    }
    
    public int GetValue(string group, string name, int defaultValue)
    {
        var value = _binder.GetIntValue(group, name);
        if (value == null)
        {
            value = _backupStore?.GetValue(group, name, defaultValue);
            if (value != null)
                _binder.SetValue(group, name, value.Value);
        }

        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, int value)
    {
        _binder.SetValue(group, name, value);
        _backupStore?.SetValue(group, name, value);
    }

    public long GetValue(string group, string name, long defaultValue)
    {
        var value = _binder.GetLongValue(group, name);
        if (value == null)
        {
            value = _backupStore?.GetValue(group, name, defaultValue);
            if (value != null)
                _binder.SetValue(group, name, value.Value);
        }

        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, long value)
    {
        _binder.SetValue(group, name, value);
        _backupStore?.SetValue(group, name, value);
    }

    public float GetValue(string group, string name, float defaultValue)
    {
        var value = _binder.GetFloatValue(group, name);
        if (value == null)
        {
            value = _backupStore?.GetValue(group, name, defaultValue);
            if (value != null)
                _binder.SetValue(group, name, value.Value);
        }

        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, float value)
    {
        _binder.SetValue(group, name, value);
        _backupStore?.SetValue(group, name, value);
    }

    public double GetValue(string group, string name, double defaultValue)
    {
        var value = _binder.GetDoubleValue(group, name);
        if (value == null)
        {
            value = _backupStore?.GetValue(group, name, defaultValue);
            if (value != null)
                _binder.SetValue(group, name, value.Value);
        }

        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, double value)
    {
        _binder.SetValue(group, name, value);
        _backupStore?.SetValue(group, name, value);
    }

    public bool GetValue(string group, string name, bool defaultValue)
    {
        var value = _binder.GetBoolValue(group, name);
        if (value == null)
        {
            value = _backupStore?.GetValue(group, name, defaultValue);
            if (value != null)
                _binder.SetValue(group, name, value.Value);
        }

        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, bool value)
    {
        _binder.SetValue(group, name, value);
        _backupStore?.SetValue(group, name, value);
    }

    public string GetValue(string group, string name, string defaultValue)
    {
        var value = _binder.GetStringValue(group, name);
        if (value == null)
        {
            value = _backupStore?.GetValue(group, name, defaultValue);
            if (value != null)
                _binder.SetValue(group, name, GenericStoreType.String, value);
        }

        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, string value)
    {
        _binder.SetValue(group, name, GenericStoreType.String, value);
        _backupStore?.SetValue(group, name, value);
    }

    public int[] GetValue(string group, string name, int[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
        {
            var backupValues = _backupStore?.GetValue(group, name, defaultValue);
            return backupValues ?? defaultValue;
        }

        var values = ParseList(listStr);
        return values.Select(int.Parse).ToArray();
    }

    public void SetValue(string group, string name, int[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
        _binder.SetValue(group, name, GenericStoreType.IntArray, strValue);
        _backupStore?.SetValue(group, name, value);
    }

    public long[] GetValue(string group, string name, long[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
        {
            var backupValues = _backupStore?.GetValue(group, name, defaultValue);
            return backupValues ?? defaultValue;
        }
        
        var values = ParseList(listStr);
        return values.Select(long.Parse).ToArray();
    }

    public void SetValue(string group, string name, long[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
        _binder.SetValue(group, name, GenericStoreType.LongArray, strValue);
        _backupStore?.SetValue(group, name, value);
    }

    public float[] GetValue(string group, string name, float[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
        {
            var backupValues = _backupStore?.GetValue(group, name, defaultValue);
            return backupValues ?? defaultValue;
        }
        
        var values = ParseList(listStr);
        return values.Select(float.Parse).ToArray();
    }

    public void SetValue(string group, string name, float[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
        _binder.SetValue(group, name, GenericStoreType.FloatArray, strValue);
        _backupStore?.SetValue(group, name, value);
    }

    public double[] GetValue(string group, string name, double[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
        {
            var backupValues = _backupStore?.GetValue(group, name, defaultValue);
            return backupValues ?? defaultValue;
        }
        
        var values = ParseList(listStr);
        return values.Select(double.Parse).ToArray();
    }

    public void SetValue(string group, string name, double[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
        _binder.SetValue(group, name, GenericStoreType.DoubleArray, strValue);
        _backupStore?.SetValue(group, name, value);
    }

    public bool[] GetValue(string group, string name, bool[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
        {
            var backupValues = _backupStore?.GetValue(group, name, defaultValue);
            return backupValues ?? defaultValue;
        }
        
        var values = ParseList(listStr);
        return values.Select(bool.Parse).ToArray();
    }

    public void SetValue(string group, string name, bool[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
        _binder.SetValue(group, name, GenericStoreType.BoolArray, strValue);
        _backupStore?.SetValue(group, name, value);
    }

    public string[] GetValue(string group, string name, string[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
        {
            var backupValues = _backupStore?.GetValue(group, name, defaultValue);
            return backupValues ?? defaultValue;
        }
        
        var values = ParseList(listStr);
        return values.ToArray();
    }

    public void SetValue(string group, string name, string[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
        _binder.SetValue(group, name, GenericStoreType.StringArray, strValue);
        _backupStore?.SetValue(group, name, value);
    }

    private static string[] ParseList(string list)
    {
        return list.Split(Separator);
    }

    private static string GenerateStringList(List<string> list)
    {
        return string.Join(Separator, list);
    }

    public List<string> Groups()
    {
        return _binder.Groups();
    }

    public List<(string, GenericStoreType)> GetGroup(string group)
    {
        return _binder.GetGroup(group);
    }
    
    public void ClearGroup(string group)
    {
        _binder.DeleteGroup(group);
    }
}