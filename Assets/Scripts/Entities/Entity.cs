using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private readonly Dictionary<Type, IComponent> _components = new();

    public void Add<T>(T component) where T : IComponent
    {
        Type type = component.GetType();

        if (_components.ContainsKey(type))
        {
            throw new Exception($"you are trying to add " +
                $"a component of type {type} " +
                $"that already exist in the entity {this}");
        }
        else
        {
            _components.Add(type, component);
        }

    }

    public void Set<T>(T component) where T : IComponent
    {
        Type type = component.GetType();

        if (_components.ContainsKey(type))
        {
            _components[type] = component;
        }
        else
        {
            _components.Add(type, component);
        }
    }

    public T Get<T>() where T : IComponent
    {
        return (T)_components[typeof(T)];
    }

    public bool TryGet<T>(out T component) where T : IComponent
    {
        Type type = typeof(T);

        if (_components.ContainsKey(type))
        {
            component = (T)_components[type];
            return true;
        }
        else
        {
            component = default;
            return false;
        }
    }

    public void Remove<T>(T component) where T : IComponent
    {
        _components.Remove(component.GetType());
    }

    public void Remove<T>() where T : IComponent
    {
        if (_components.TryGetValue(typeof(T), out IComponent component))
        {
            _components.Remove(component.GetType());
        }
    }
}