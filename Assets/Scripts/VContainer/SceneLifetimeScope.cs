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

    private CharacterStatsNames _statNames;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterStats(builder);
        RegisterCharacter(builder);
        RegisterInventory(builder);
    }

    private void RegisterStats(IContainerBuilder builder)
    {
        _statNames = new CharacterStatsNames();
        builder.RegisterComponent(_statNames);
    }

    private void RegisterCharacter(IContainerBuilder builder)
    {
        KeyValuePair<string, int>[] initStats = 
            _initCharacterStats.GetInitStats(_statNames);

        builder.Register<Character>(Lifetime.Singleton)
            .WithParameter(initStats);
    }

    private void RegisterInventory(IContainerBuilder builder)
    {
        builder.Register<Inventory>(Lifetime.Singleton).WithParameter(new Item[] { });
        builder.Register<Equipment.Equipment>(Lifetime.Singleton);
        builder.RegisterComponent(_inventoryContext);

        builder.RegisterComponent(_equipmentHelper);
    }
}