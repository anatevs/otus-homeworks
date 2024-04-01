using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEventsSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private List<Stash> _stashes = new List<Stash>();

    public void OnAwake()
    {
        AddEventToClear<FireEvent>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var stash in _stashes)
        {
            stash.RemoveAll();
        }
    }

    private void AddEventToClear<T>() where T : struct, IComponent
    {
        _stashes.Add(this.World.GetStash<T>());
    }

    public void Dispose()
    {
    }
}
