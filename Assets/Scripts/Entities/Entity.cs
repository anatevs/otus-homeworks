using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private readonly Dictionary<ComponentName, IComponent> _components = new();

    public void Add(IComponent component)
    {
        if (_components.ContainsKey(component.Name))
        {
            throw new Exception($"you are trying to add " +
                $"a component of type {component.Name} " +
                $"that already exist in the entity {this}");
        }
        else
        {
            _components.Add(component.Name, component);
        }
    }

    public void Set(IComponent component)
    {
        if (_components.ContainsKey(component.Name))
        {
            _components[component.Name] = component;
        }
        else
        {
            _components.Add(component.Name, component);
        }
    }

    public IComponent Get(ComponentName componentName)
    {
        return _components[componentName];
    }

    public bool TryGet(ComponentName name, out IComponent component)
    {
        if (_components.ContainsKey(name))
        {
            component = _components[name];
            return true;
        }
        else
        {
            component = null;
            return false;
        }
    }

    public void Remove(IComponent component)
    {
        _components.Remove(component.Name);
    }
}