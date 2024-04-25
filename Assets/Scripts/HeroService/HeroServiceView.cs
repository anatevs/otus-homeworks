using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UI;
using VContainer.Unity;

public sealed class HeroServiceView : IInitializable, IDisposable
{
    private readonly UIService _uiService;

    private readonly HeroServicePresenter _presenter;

    private readonly Dictionary<Team, HeroListView> _viewLists = new();

    public HeroServiceView(HeroServicePresenter heroServicePresenter, UIService uiService)
    {
        _presenter = heroServicePresenter;
        _uiService = uiService;
    }

    void IInitializable.Initialize()
    {
        InitListViews();

        _viewLists[Team.Red].OnHeroClicked += OnClickedHeroRed;
        _viewLists[Team.Blue].OnHeroClicked += OnClickedHeroBlue;
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
            _viewLists.Add(teamName, heroListView);


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
        _viewLists[info.team].SetActiveTeam(isActive);
        _viewLists[info.team].SetActiveHero(info.id, isActive);
    }

    public void DestroyHero(InfoComponent info)
    {
        _viewLists[info.team].OnViewDestroyed(info.id);
    }

    public UniTask AttackTaskAsync(InfoComponent hero, InfoComponent target)
    {
        HeroView heroView = _viewLists[hero.team].GetView(hero.id);
        HeroView targetView = _viewLists[target.team].GetView(target.id);

        return heroView.AnimateAttack(targetView);
    }

    public void ChangeStats(InfoComponent info, int hp, int damage)
    {
        _viewLists[info.team].SetStats(info.id, hp, damage);
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
        HeroListView heroListView = _viewLists[team];
        int index = heroListView.GetIndex(heroView);

        _presenter.ClickHero(team, index);
    }

    void IDisposable.Dispose()
    {
        _viewLists[Team.Red].OnHeroClicked -= OnClickedHeroRed;
        _viewLists[Team.Blue].OnHeroClicked -= OnClickedHeroBlue;
    }
}