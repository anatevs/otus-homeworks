using Sample;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public sealed class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private InventoryContext _inventoryContext;

    [SerializeField]
    private EquipmentInventoryHelper _equipmentHelper;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterCharacter(builder);
        RegisterStats(builder);
        RegisterInventory(builder);
    }

    private void RegisterCharacter(IContainerBuilder builder)
    {
        builder.Register<Character>(Lifetime.Singleton);
    }

    private void RegisterStats(IContainerBuilder builder)
    {
        builder.Register<CharacterStatsNames>(Lifetime.Singleton);
    }

    private void RegisterInventory(IContainerBuilder builder)
    {
        builder.Register<Inventory>(Lifetime.Singleton).WithParameter(new Item[] { });
        builder.Register<Equipment.Equipment>(Lifetime.Singleton);
        builder.RegisterComponent(_inventoryContext);

        builder.RegisterComponent(_equipmentHelper);
    }
}