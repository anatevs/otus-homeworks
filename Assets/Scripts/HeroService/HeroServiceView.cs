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

    private readonly Dictionary<Team, HeroListView> _viewsLists = new();

    [Inject]
    public void Construct(HeroServicePresenter heroServicePresenter, UIService uiService)
    {
        _presenter = heroServicePresenter;
        _uiService = uiService;
    }

    private void Awake()
    {
        InitListViews();

        _presenter.OnSetActive += SetActiveTeamAndHero;
        _presenter.OnDestroy += DestroyHero;
        //_presenter.OnAttack += Attack;
        _presenter.OnChangeStats += ChangeStats;

        _viewsLists[Team.Red].OnHeroClicked += OnClickedHeroRed;
        _viewsLists[Team.Blue].OnHeroClicked += OnClickedHeroBlue;
    }

    private void InitListViews()
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
            _viewsLists.Add(teamName, heroListView);


            IReadOnlyList<HeroView> listViews = heroListView.GetViews();
            for (int i = 0; i < listViews.Count; i++)
            {
                HeroView view = listViews[i];
                HeroModel model = view.GetComponent<HeroModel>();

                heroListView.SetStats(i, model.HP, model.Damage);
            }
        }
    }

    public void SetActiveTeamAndHero(InfoComponent info, bool isActive)
    {
        _viewsLists[info.team].SetActiveTeam(isActive);
        _viewsLists[info.team].SetActiveHero(info.id, isActive);
    }


    public void DestroyHero(InfoComponent info)
    {
        _viewsLists[info.team].OnViewDestroyed(info.id);
    }

    public UniTask AttackTask(InfoComponent hero, InfoComponent target)
    {
        HeroView heroView = _viewsLists[hero.team].GetView(hero.id);
        HeroView targetView = _viewsLists[target.team].GetView(target.id);

        return heroView.AnimateAttack(targetView);
    }

    public void ChangeStats(InfoComponent info, int hp, int damage)
    {
        _viewsLists[info.team].SetStats(info.id, hp, damage);
    }

    private void OnClickedHeroBlue(HeroView heroView)
    {
        OnClickReact(Team.Blue, heroView);
    }

    private void OnClickedHeroRed(HeroView heroView)
    {
        OnClickReact(Team.Red, heroView);
    }

    private void OnClickReact(Team team, HeroView heroView)
    {
        HeroListView heroListView = _viewsLists[team];
        int index = heroListView.GetIndex(heroView);

        _presenter.ClickHero(team, index);
    }

    void OnDisable()
    {
        _presenter.OnSetActive -= SetActiveTeamAndHero;
        _presenter.OnDestroy -= DestroyHero;
        _presenter.OnChangeStats -= ChangeStats;

        _viewsLists[Team.Red].OnHeroClicked -= OnClickedHeroRed;
        _viewsLists[Team.Blue].OnHeroClicked -= OnClickedHeroBlue;
    }
}