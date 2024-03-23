using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private ECSAdmin _ECSAdmin;

    protected override void Configure(IContainerBuilder builder)
    {
        BuildTeamServices(builder);
    }

    private void BuildTeamServices(IContainerBuilder builder)
    {
        builder.Register<TeamService<TeamBlue>>(Lifetime.Singleton).WithParameter<TeamBlue>(new TeamBlue());
        builder.Register<TeamService<TeamRed>>(Lifetime.Singleton).WithParameter<TeamRed>(new TeamRed());

        builder.Register<TowardsTargetSystem<TeamRed>>(Lifetime.Singleton);

        builder.RegisterComponent<ECSAdmin>(_ECSAdmin);
    }
}
