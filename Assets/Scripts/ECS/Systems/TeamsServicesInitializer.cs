using Scellecs.Morpeh;

public class TeamsServicesInitializer : IInitializer
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private readonly TeamService<TeamRed> _redTeamService;
    private readonly TeamService<TeamBlue> _blueTeamService;

    public TeamsServicesInitializer
        (
        TeamService<TeamRed> redTeamService,
        TeamService<TeamBlue> blueTeamService
        )
    {
        _redTeamService = redTeamService;
        _blueTeamService = blueTeamService;
    }

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<Team>()
            .Without<Inactive>()
            .Build();

        foreach (Entity entity in _filter)
        {
            if (entity.GetComponent<Team>().value == TeamType.Red)
            {
                _redTeamService.AddToTeam(entity);
            }
            else if (entity.GetComponent<Team>().value == TeamType.Blue)
            {
                _blueTeamService.AddToTeam(entity);
            }
        }
    }

    public void Dispose()
    {
    }
}
