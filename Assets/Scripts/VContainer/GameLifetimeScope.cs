using Scellecs.Morpeh;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ECSAdmin _ECSAdmin;

    protected override void Configure(IContainerBuilder builder)
    {
        BuildEventsWorld(builder);
        BuildTeamServices(builder);
    }

    private void BuildTeamServices(IContainerBuilder builder)
    {
        builder.Register<TeamService<TeamBlue>>(Lifetime.Singleton).WithParameter<TeamBlue>(new TeamBlue());
        builder.Register<TeamService<TeamRed>>(Lifetime.Singleton).WithParameter<TeamRed>(new TeamRed());

        builder.RegisterComponent<ECSAdmin>(_ECSAdmin);
    }

    private void BuildEventsWorld(IContainerBuilder builder)
    {
        World _eventsWorld = World.Create(ECSWorlds.Events);
        builder.RegisterComponent<World>(_eventsWorld);
    }
}