using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using VContainer;

public sealed class HeroServiceView : MonoBehaviour
{
    private UIService _uiService;

    private HeroServicePresenter _presenter;

    private readonly Dictionary<Team, HeroListView> _listViews = new();

    [Inject]
    public void Construct(HeroServicePresenter heroServicePresenter, UIService uiService)
    {
        _presenter = heroServicePresenter;
        _uiService = uiService;

        //SetListViews();

        //_presenter.OnSetActive += SetActive;
        //_presenter.OnDestroy += DestroyHero;
        //_presenter.OnAttack += Attack;
        //_presenter.OnChangeStats += ChangeStats;

        //_listViews[Team.Red].OnHeroClicked += OnClickedHeroRed;
        //_listViews[Team.Blue].OnHeroClicked += OnClickedHeroBlue;
    }

    private void Awake()
    {
        SetListViews();

        _presenter.OnSetActive += SetActiveTeamAndHero;
        _presenter.OnDestroy += DestroyHero;
        //_presenter.OnAttack += Attack;
        _presenter.OnChangeStats += ChangeStats;

        _listViews[Team.Red].OnHeroClicked += OnClickedHeroRed;
        _listViews[Team.Blue].OnHeroClicked += OnClickedHeroBlue;
    }

    private void SetListViews()
    {
        foreach (Team teamName in Enum.GetValues(typeof(Team)))
        {
            HeroListView heroListView;

            if (teamName == Team.Red)
            {
                heroListView = _uiService.GetRedPlayer();
            }
            else
            {
                heroListView = _uiService.GetBluePlayer();
            }
            _listViews.Add(teamName, heroListView);
        }
    }

    public void SetActiveTeamAndHero(InfoComponent info, bool isActive)
    {
        _listViews[info.team].SetActiveTeam(isActive);
        _listViews[info.team].SetActiveHero(info.id, isActive);
    }


    public void DestroyHero(InfoComponent info)
    {
        _listViews[info.team].OnViewDestroyed(info.id);
    }

    public UniTask AttackTask(InfoComponent hero, InfoComponent target)
    {
        HeroView heroView = _listViews[hero.team].GetView(hero.id);
        HeroView targetView = _listViews[target.team].GetView(target.id);

        return heroView.AnimateAttack(targetView);
    }

    private void ChangeStats(InfoComponent info, int hp, int damage)
    {
        _listViews[info.team].SetStats(info.id, hp, damage);
    }

    private void OnClickedHeroBlue(HeroView heroView)
    {
        Debug.Log("click on blue");
        OnClickReact(Team.Blue, heroView);
    }

    private void OnClickedHeroRed(HeroView heroView)
    {
        OnClickReact(Team.Red, heroView);
    }

    private void OnClickReact(Team team, HeroView heroView)
    {
        HeroListView heroListView = _listViews[team];
        int index = heroListView.GetIndex(heroView);

        _presenter.ClickHero(team, index);
    }

    void OnDisable()
    {
        _presenter.OnSetActive -= SetActiveTeamAndHero;
        _presenter.OnDestroy -= DestroyHero;
        _presenter.OnChangeStats -= ChangeStats;

        _listViews[Team.Red].OnHeroClicked -= OnClickedHeroRed;
        _listViews[Team.Blue].OnHeroClicked -= OnClickedHeroBlue;
    }
}