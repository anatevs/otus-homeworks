using VContainer;
using VContainer.Unity;
using UnityEngine;
using Game.GamePlay.Conveyor;
using Upgrades;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ConveyorEntity _conveyorEntity;

    [SerializeField]
    private UpgradeSystem _upgradeSystem;

    protected override void Configure(IContainerBuilder builder)
    {
        ConfigureConveyorUpgrades(builder);
    }

    private void ConfigureConveyorUpgrades(IContainerBuilder builder)
    {
        builder.RegisterComponent(_conveyorEntity);

        builder.RegisterComponent(_upgradeSystem);
    }
}
