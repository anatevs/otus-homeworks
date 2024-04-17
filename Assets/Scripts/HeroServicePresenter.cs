using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroServicePresenter : IDisposable
{
    public event Action<InfoComponent, bool> OnSetActive;

    public event Action<InfoComponent> OnDestroy;

    public event Action<InfoComponent, int, int> OnChangeStats;

    //public HeroEntityList HeroEntityList => _heroList;

    private readonly HeroListService _heroListService;

    public HeroServicePresenter(HeroListService heroListService)
    {
        _heroListService = heroListService;

        _heroListService.OnDestroy += DestroyHero;
        _heroListService.OnSetActive += OnActiveSet;
        _heroListService.OnChangeHP += OnStatChanged;
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

    void IDisposable.Dispose()
    {
        _heroListService.OnDestroy -= DestroyHero;
        _heroListService.OnSetActive -= OnActiveSet;
        _heroListService.OnChangeHP -= OnStatChanged;
    }
}