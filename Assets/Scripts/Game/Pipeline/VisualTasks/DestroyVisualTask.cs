public sealed class DestroyVisualTask : Task
{
    private readonly HeroServiceView _heroServiceView;
    private readonly HeroEntity _hero;

    public DestroyVisualTask(HeroServiceView heroServiceView, HeroEntity hero)
    {
        _heroServiceView = heroServiceView;
        _hero = hero;
    }

    protected override void OnRun()
    {
        _heroServiceView.DestroyHero(_hero.Get<TeamInfoComponent>());
        Finish();
    }
}