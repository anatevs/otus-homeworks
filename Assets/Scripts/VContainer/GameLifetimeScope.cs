using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ECSAdmin _ECSAdmin;

    [SerializeField]
    private PrefabsStorage _prefabsStorage;

    protected override void Configure(IContainerBuilder builder)
    {
        BuildTeamServices(builder);

        BuildPrefabStorage(builder);
    }

    private void BuildTeamServices(IContainerBuilder builder)
    {
        builder.Register<TeamService>(Lifetime.Singleton);

        builder.RegisterComponent<ECSAdmin>(_ECSAdmin);
    }

    private void BuildPrefabStorage(IContainerBuilder builder)
    {
        builder.RegisterComponent<PrefabsStorage>(_prefabsStorage);
    }
}