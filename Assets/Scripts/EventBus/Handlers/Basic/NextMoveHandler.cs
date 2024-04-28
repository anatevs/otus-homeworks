public sealed class NextMoveHandler : BaseHandler<NextMoveEvent>
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
        HeroEntity prevHero = nextMoveEvent.prevHero;

        _teamData.SwitchTeams();

        Team currentTeam = _teamData.Player;

        _heroListService.PrepareNextMove(currentTeam);
        HeroEntity currentHero = _heroListService.GetCurrentActive(currentTeam);

        if (currentHero.TryGet<FreezeComponent>(out _))
        {
            currentHero.Remove<FreezeComponent>();

            _heroListService.PrepareNextMove(currentTeam);
            currentHero = _heroListService.GetCurrentActive(currentTeam);
        }

        EventBus.RaiseEvent(new ChangeActiveEvent(prevHero, currentHero));
    }
}