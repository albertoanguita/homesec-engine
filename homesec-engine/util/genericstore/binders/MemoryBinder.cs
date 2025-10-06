using System.Collections.Concurrent;

namespace TestWebAPI1.util.genericstore.binders;

class MemoryStoreEntity
{
    public required GenericStoreType Type { get; set; }
    public int? IntValue { get; set; }
    public long? LongValue { get; set; }
    public float? FloatValue { get; set; }
    public double? DoubleValue { get; set; }
    public bool? BoolValue { get; set; }
    public string? StringValue { get; set; }
}

public class MemoryBinder : GenericStoreBinder
{
    private ConcurrentDictionary<string, ConcurrentDictionary<string, MemoryStoreEntity>> _store = new();
    
    private MemoryStoreEntity? GetVariable(string group, string name)
    {
        if (_store.TryGetValue(group, out var groupStore) && 
            groupStore.TryGetValue(name, out var variable))
        {
            return variable;
        }

        return null;
    }

    private MemoryStoreEntity? GetVariable(string group, string name, GenericStoreType type)
    {
        var variable = GetVariable(group, name);
        return variable != null && variable.Type == type ? variable : null;
    }

    private MemoryStoreEntity GetOrCreateVariable(string group, string name, GenericStoreType type)
    {
        if (!_store.TryGetValue(group, out var groupStore))
        {
            groupStore = new ConcurrentDictionary<string, MemoryStoreEntity>();
            _store.TryAdd(group, groupStore);
        }

        if (groupStore.TryGetValue(name, out var variable))
        {
            if (variable.Type != type)
            {
                ClearGroupIfEmpty(group);
                throw new ArgumentException($"Variable [{group}].{name} already exists with type {variable.Type}, " +
                                            $"but trying to assign with type {type}");
            }
        }
        else
        {
            variable = new MemoryStoreEntity
            {
                Type = type
            };
            groupStore.TryAdd(name, variable);
        }
        
        return variable;
    }

    private void ClearGroupIfEmpty(string group)
    {
        if (_store.TryGetValue(group, out var groupStore) && groupStore.IsEmpty)
            _store.TryRemove(group, out _);
    }

    public int? GetIntValue(string group, string name)
    {
        return GetVariable(group, name, GenericStoreType.Int)?.IntValue;;
    }

    public long? GetLongValue(string group, string name)
    {
        return GetVariable(group, name, GenericStoreType.Long)?.LongValue;;
    }

    public float? GetFloatValue(string group, string name)
    {
        return GetVariable(group, name, GenericStoreType.Float)?.FloatValue;;
    }

    public double? GetDoubleValue(string group, string name)
    {
        return GetVariable(group, name, GenericStoreType.Double)?.DoubleValue;;
    }

    public bool? GetBoolValue(string group, string name)
    {
        return GetVariable(group, name, GenericStoreType.Bool)?.BoolValue;;
    }

    public string? GetStringValue(string group, string name)
    {
        return GetVariable(group, name, GenericStoreType.String)?.StringValue;;
    }

    public void SetValue(string group, string name, int value)
    {
        GetOrCreateVariable(group, name,  GenericStoreType.Int).IntValue = value;
    }

    public void SetValue(string group, string name, long value)
    {
        GetOrCreateVariable(group, name,  GenericStoreType.Int).LongValue = value;
    }

    public void SetValue(string group, string name, float value)
    {
        GetOrCreateVariable(group, name,  GenericStoreType.Int).FloatValue = value;
    }

    public void SetValue(string group, string name, double value)
    {
        GetOrCreateVariable(group, name,  GenericStoreType.Int).DoubleValue = value;
    }

    public void SetValue(string group, string name, bool value)
    {
        GetOrCreateVariable(group, name,  GenericStoreType.Int).BoolValue = value;
    }

    public void SetValue(string group, string name, GenericStoreType type, string value)
    {
        GetOrCreateVariable(group, name,  GenericStoreType.Int).StringValue = value;
    }

    public List<string> Groups()
    {
        return _store.Keys.ToList();
    }

    public List<(string, GenericStoreType)> GetGroup(string group)
    {
        return _store.TryGetValue(group, out var groupStore) ? 
            groupStore.Keys.Select(name => (name, groupStore[name].Type)).ToList() : [];
    }

    public void DeleteVariable(string group, string name)
    {
        if (_store.TryGetValue(group, out var groupStore))
            groupStore.TryRemove(name, out _);
        
        ClearGroupIfEmpty(group);
    }

    public void DeleteGroup(string group)
    {
        _store.TryRemove(group, out _);
    }
}