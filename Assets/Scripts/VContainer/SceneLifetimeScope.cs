using Sample;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using System.Collections.Generic;

public sealed class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private InventoryContext _inventoryContext;

    [SerializeField]
    private EquipmentInventoryHelper _equipmentHelper;

    [SerializeField]
    private InitCharacterStats _initCharacterStats;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterCharacter(builder);
        RegisterInventory(builder);
    }

    private void RegisterCharacter(IContainerBuilder builder)
    {
        KeyValuePair<string, float>[] initStats = 
            _initCharacterStats.GetInitStats();

        builder.Register<Character>(Lifetime.Singleton)
            .WithParameter(initStats);
    }

    private void RegisterInventory(IContainerBuilder builder)
    {
        builder.Register<Inventory>(Lifetime.Singleton).WithParameter(new Item[] { });
        builder.Register<Equipment>(Lifetime.Singleton);
        builder.RegisterComponent(_inventoryContext);

        builder.RegisterComponent(_equipmentHelper)
            .AsImplementedInterfaces()
            .AsSelf();
    }
}