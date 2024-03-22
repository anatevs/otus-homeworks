using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        BuildTeamServices(builder);
    }

    private void BuildTeamServices(IContainerBuilder builder)
    {
        builder.Register<TeamService<TeamBlue>>(Lifetime.Singleton);
        builder.Register<TeamService<TeamRed>>(Lifetime.Singleton);
    }
}
