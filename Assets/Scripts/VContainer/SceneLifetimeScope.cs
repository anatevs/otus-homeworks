using Scripts.Chest;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ChestTimer[] _chestTimers;

    protected override void Configure(IContainerBuilder builder)
    {
        InjectChests();
    }

    private void InjectChests()
    {
        foreach (var chest in _chestTimers)
        {
            autoInjectGameObjects.Add(chest.gameObject);
        }
    }
}