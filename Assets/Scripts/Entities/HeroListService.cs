using System.Collections.Generic;
using UI;
using System;


public class HeroListService : IDisposable
{
    public event Action<HeroEntity> OnViewClicked;

    private readonly UIService _uiService;

    private readonly Dictionary<HeroView, HeroEntity> _viewsEntities = new();

    private readonly Dictionary<Team, HeroEntityList> _heroEntities = new();

    public HeroListService(UIService uiService)
    {
        _uiService = uiService;

        InitEntities();
    }

    private void InitEntities()
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

            List<HeroEntity> heroList = new();

            foreach (HeroView heroView in heroListView.GetViews())
            {
                HeroEntity entity = heroView.GetComponent<HeroEntity>();
                entity.Add(new TeamComponent(teamName));

                heroList.Add(entity);
                _viewsEntities.Add(heroView, entity);
            }

            _heroEntities.Add(teamName, new HeroEntityList(heroList));

            heroListView.OnHeroClicked += OnClickedEvent;
        }
    }

    private void OnClickedEvent(HeroView heroView)
    {
        OnViewClicked.Invoke(_viewsEntities[heroView]);
    }

    void IDisposable.Dispose()
    {
        _uiService.GetRedPlayer().OnHeroClicked -= OnClickedEvent;
        _uiService.GetBluePlayer().OnHeroClicked -= OnClickedEvent;
    }
}