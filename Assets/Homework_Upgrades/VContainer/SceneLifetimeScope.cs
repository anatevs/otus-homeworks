using VContainer;
using VContainer.Unity;
using UnityEngine;
using Upgrades;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ConveyorModel _conveyorModel;

    [SerializeField]
    private UpgradeSystem _upgradeSystem;

    protected override void Configure(IContainerBuilder builder)
    {
        ConfigureConveyorUpgrades(builder);
    }

    private void ConfigureConveyorUpgrades(IContainerBuilder builder)
    {
        builder.RegisterComponent(_conveyorModel);

        builder.RegisterComponent(_upgradeSystem);
    }
}