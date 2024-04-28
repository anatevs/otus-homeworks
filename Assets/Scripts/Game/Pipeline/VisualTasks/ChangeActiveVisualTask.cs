public sealed class ChangeActiveVisualTask : Task
{
    private readonly HeroServiceView _heroServiceView;
    private readonly TeamInfoComponent _prevHero;
    private readonly TeamInfoComponent _currentHero;

    public ChangeActiveVisualTask(HeroServiceView heroServiceView, TeamInfoComponent prevHero, TeamInfoComponent currentHero)
    {
        _heroServiceView = heroServiceView;
        _prevHero = prevHero;
        _currentHero = currentHero;
    }

    protected override void OnRun()
    {
        _heroServiceView.SetActiveTeamAndHero(_prevHero, false);
        _heroServiceView.SetActiveTeamAndHero(_currentHero, true);

        Finish();
    }
}
