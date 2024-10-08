using Scripts.Chest;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Scripts.SaveLoadNamespace;
using Scripts.MoneyNamespace;

public sealed class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ChestTimer[] _chestTimers;

    [SerializeField]
    private AppQuitManager _appQuitManager;

    [SerializeField]
    private MoneyView[] _moneyStorageViews;

    protected override void Configure(IContainerBuilder builder)
    {
        InjectChests();

        RegisterManagers(builder);
    }

    private void InjectChests()
    {
        foreach (var chest in _chestTimers)
        {
            autoInjectGameObjects.Add(chest.gameObject);
        }
    }

    private void RegisterManagers(IContainerBuilder builder)
    {
        builder.RegisterComponent(_appQuitManager);
    }
}