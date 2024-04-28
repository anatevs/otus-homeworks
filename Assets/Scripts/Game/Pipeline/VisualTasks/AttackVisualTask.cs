public sealed class AttackVisualTask : Task
{
    private readonly HeroServiceView _heroServiceView;
    private readonly HeroEntity _hero;
    private readonly HeroEntity _target;


    public AttackVisualTask(HeroServiceView heroServiceView, HeroEntity hero, HeroEntity target)
    {
        _heroServiceView = heroServiceView;
        _hero = hero;
        _target = target;
    }

    protected override async void OnRun()
    {
        await _heroServiceView.AttackTaskAsync(_hero.Get<TeamInfoComponent>(), _target.Get<TeamInfoComponent>());

        Finish();
    }
}