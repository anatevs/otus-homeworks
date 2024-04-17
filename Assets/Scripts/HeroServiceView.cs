using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class HeroServiceView : MonoBehaviour, IDisposable
{
    private UIService _uiService;

    private HeroServicePresenter _presenter;

    private Dictionary<Team, HeroListView> _listViews = new Dictionary<Team, HeroListView>();

    public void Construct(HeroServicePresenter heroServicePresenter, UIService uiService)
    {
        _presenter = heroServicePresenter;
        _uiService = uiService;

        SetListViews();

        _presenter.OnSetActive += OnSetActive;
        _presenter.OnDestroy += OnHeroDestoyed;
        _presenter.OnChangeStats += OnStatsChanged;
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

    void IDisposable.Dispose()
    {
        _presenter.OnSetActive -= OnSetActive;
        _presenter.OnDestroy -= OnHeroDestoyed;
        _presenter.OnChangeStats -= OnStatsChanged;
    }
}