using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using VContainer;

public sealed class HeroServiceView : MonoBehaviour, IDisposable
{
    private UIService _uiService;

    private HeroServicePresenter _presenter;

    private readonly Dictionary<Team, HeroListView> _listViews = new();

    [Inject]
    public void Construct(HeroServicePresenter heroServicePresenter, UIService uiService)
    {
        _presenter = heroServicePresenter;
        _uiService = uiService;

        SetListViews();

        _presenter.OnSetActive += OnSetActive;
        _presenter.OnDestroy += OnHeroDestoyed;
        _presenter.OnChangeStats += OnStatsChanged;

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

    private void OnSetActive(InfoComponent info, bool isActive)
    {
        _listViews[info.team].SetActive(isActive);
    }

    private void OnHeroDestoyed(InfoComponent info)
    {
        _listViews[info.team].OnViewDestroyed(info.id);
    }

    private void OnStatsChanged(InfoComponent info, int hp, int damage)
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
        Debug.Log("click on red");
        OnClickReact(Team.Red, heroView);
    }

    private void OnClickReact(Team team, HeroView heroView)
    {
        HeroListView heroListView = _listViews[team];
        int index = heroListView.GetIndex(heroView);

        _presenter.OnHeroClicked(team, index);
    }

    void IDisposable.Dispose()
    {
        _presenter.OnSetActive -= OnSetActive;
        _presenter.OnDestroy -= OnHeroDestoyed;
        _presenter.OnChangeStats -= OnStatsChanged;

        _listViews[Team.Red].OnHeroClicked -= OnClickedHeroRed;
        _listViews[Team.Blue].OnHeroClicked -= OnClickedHeroBlue;
    }
}