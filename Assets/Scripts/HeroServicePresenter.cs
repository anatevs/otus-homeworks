using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public sealed class HeroServicePresenter : IDisposable
{
    public event Action<InfoComponent, bool> OnSetActive;

    public event Action<InfoComponent> OnDestroy;

    public event Action<InfoComponent, int, int> OnChangeStats;

    public event Action<Team, int> OnClickHero;


    private readonly HeroListService _heroListService;

    public HeroServicePresenter(HeroListService heroListService)
    {
        _heroListService = heroListService;

        _heroListService.OnDestroy += DestroyHero;
        _heroListService.OnSetActive += OnActiveSet;
        _heroListService.OnChangeHP += OnStatChanged;

        OnClickHero += _heroListService.OnHeroClicked;
    }

    private void DestroyHero(InfoComponent info)
    {
        OnDestroy?.Invoke(info);
    }

    private void OnActiveSet(InfoComponent info, bool isActive)
    {
        OnSetActive?.Invoke(info, isActive);
    }

    private void OnStatChanged(InfoComponent info, int hp, int damage)
    {
        OnChangeStats?.Invoke(info, hp, damage);
    }

    public void OnHeroClicked(Team team, int id)
    {
        OnClickHero?.Invoke(team, id);
    }

    void IDisposable.Dispose()
    {
        _heroListService.OnDestroy -= DestroyHero;
        _heroListService.OnSetActive -= OnActiveSet;
        _heroListService.OnChangeHP -= OnStatChanged;

        OnClickHero -= _heroListService.OnHeroClicked;
    }
}