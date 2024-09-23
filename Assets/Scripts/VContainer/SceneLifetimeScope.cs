using Scripts.Chest;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ChestTimer _chestTimer;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterChests(builder);
    }

    private void RegisterChests(IContainerBuilder builder)
    {
        builder.RegisterComponent(_chestTimer);
    }
}
