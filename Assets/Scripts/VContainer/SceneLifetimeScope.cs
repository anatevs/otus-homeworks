using Sample;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private TestUpgrade _testUpgrade;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterWoodConverter(builder);
    }

    private void RegisterWoodConverter(IContainerBuilder builder)
    {
        builder.Register<WoodConverter>(Lifetime.Singleton);
        builder.RegisterComponent(_testUpgrade);
    }
}
