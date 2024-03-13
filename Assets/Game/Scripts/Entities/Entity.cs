using System.Collections.Generic;
using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private List<object> _components = new List<object>();

    public int GetEntitySize()
    {
        return _components.Count;
    }

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

        throw new Exception ($"Component of type {typeof(T).Name} was not found in entity");
    }

    public void RemoveAllComponents()
    {
        _components.Clear();
    }
}