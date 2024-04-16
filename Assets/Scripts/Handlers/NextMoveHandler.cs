public class NextMoveHandler : BaseHandler<NextMoveEvent>
{
    private readonly HeroListService _heroListService;
    private readonly CurrentTeamData _teamData;

    public NextMoveHandler(EventBus eventBus, HeroListService heroListService, CurrentTeamData teamData) : base(eventBus)
    {
        _heroListService = heroListService;
        _teamData = teamData;
    }

    protected override void RaiseEvent(NextMoveEvent nextMoveEvent)
    {
        HeroEntity prevPlayer = nextMoveEvent.prevPlayer;
        prevPlayer.Set(new IsActiveComponent(false));

        _teamData.SwitchTeams();
        Team currentTeam = _teamData.Player;

        _heroListService.PrepareNextMove(currentTeam);
        HeroEntity currentHero = _heroListService.GetCurrentActive(currentTeam);

        currentHero.Set(new IsActiveComponent(true));
    }
}