using System;
using System.Collections.Generic;

public class Entity_Plain
{
    private List<object> _components = new List<object>();

    public void AddComponentToEntity<T>(T component)
    {
        _components.Add(component);
    }

    public bool TryGetComponentFromEntity<T>(out T component)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T componentT)
            {
                component = componentT;
                return true;
            }
        }

        component = default;
        return false;
    }

    public T GetComponentFromEntity<T>()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T componentT)
            {
                return componentT;
            }
        }

        throw new Exception($"Component of type {typeof(T).Name} was not found in entity");
    }

    public void RemoveAllComponents()
    {
        _components = new List<object>();
    }
}