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
        builder.Register<TeamService<TeamBlue>>(Lifetime.Singleton).WithParameter<TeamBlue>(new TeamBlue());
        builder.Register<TeamService<TeamRed>>(Lifetime.Singleton).WithParameter<TeamRed>(new TeamRed());

        builder.RegisterComponent<ECSAdmin>(_ECSAdmin);
    }

    private void BuildPrefabStorage(IContainerBuilder builder)
    {
        builder.RegisterComponent<PrefabsStorage>(_prefabsStorage);
    }

    private void BuildCollisionManagers(IContainerBuilder builder)
    {
        //builder.Register(ProjectileCollisionManager<MovableProvider>())
    }










}