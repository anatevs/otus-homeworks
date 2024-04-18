using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public sealed class HeroServicePresenter : IDisposable
{
    public event Action<InfoComponent, bool> OnSetActive;

    public event Action<InfoComponent> OnDestroy;

    public event Action<InfoComponent, InfoComponent> OnAttack;

    public event Action<InfoComponent, int, int> OnChangeStats;

    public event Action<Team, int> OnHeroClicked;


    private readonly HeroListService _heroListService;

    public HeroServicePresenter(HeroListService heroListService)
    {
        _heroListService = heroListService;

        _heroListService.OnDestroy += DestroyHero;
        _heroListService.OnSetActive += SetActive;
        _heroListService.OnAttack += Attack;
        _heroListService.OnChangeHP += ChangeStat;

        OnHeroClicked += _heroListService.ClickHero;
    }

    private void DestroyHero(InfoComponent info)
    {
        OnDestroy?.Invoke(info);
    }

    private void SetActive(InfoComponent info, bool isActive)
    {
        OnSetActive?.Invoke(info, isActive);
    }

    private void Attack(InfoComponent hero, InfoComponent target)
    {
        OnAttack?.Invoke(hero, target);
    }

    private void ChangeStat(InfoComponent info, int hp, int damage)
    {
        OnChangeStats?.Invoke(info, hp, damage);
    }

    public void ClickHero(Team team, int id)
    {
        OnHeroClicked?.Invoke(team, id);
    }

    void IDisposable.Dispose()
    {
        _heroListService.OnDestroy -= DestroyHero;
        _heroListService.OnSetActive -= SetActive;
        _heroListService.OnAttack -= Attack;
        _heroListService.OnChangeHP -= ChangeStat;

        OnHeroClicked -= _heroListService.ClickHero;
    }
}