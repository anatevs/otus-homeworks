using System;

public sealed class HeroServicePresenter : IDisposable
{
    public event Action<Team, int> OnHeroClicked;

    private readonly HeroListService _heroListService;

    public HeroServicePresenter(HeroListService heroListService)
    {
        _heroListService = heroListService;

        OnHeroClicked += _heroListService.ClickHero;
    }

    public void ClickHero(Team team, int id)
    {
        OnHeroClicked?.Invoke(team, id);
    }

    void IDisposable.Dispose()
    {
        OnHeroClicked -= _heroListService.ClickHero;
    }
}