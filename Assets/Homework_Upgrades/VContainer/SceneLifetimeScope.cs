using VContainer;
using VContainer.Unity;
using UnityEngine;
using Game.GamePlay.Conveyor;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ConveyorEntity _conveyorEntity;

    protected override void Configure(IContainerBuilder builder)
    {
    }

    private void ConfigureConveyorUpgrades(IContainerBuilder builder)
    {
        builder.RegisterComponent(_conveyorEntity);


    }
}
