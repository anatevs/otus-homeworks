public sealed class DealDamageVisualTask : Task
{
    private readonly HeroServiceView _heroServiceView;
    private readonly TeamInfoComponent _infoComponent;
    private readonly int _hp;
    private readonly int _damage;

    public DealDamageVisualTask(HeroServiceView heroServiceView, TeamInfoComponent infoComponent, int hp, int damage)
    {
        _heroServiceView = heroServiceView;
        _infoComponent = infoComponent;
        _hp = hp;
        _damage = damage;
    }

    protected override void OnRun()
    {
        _heroServiceView.ChangeStats(_infoComponent, _hp, _damage);

        Finish();
    }
}