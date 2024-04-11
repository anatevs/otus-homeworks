using System.Collections.Generic;
using UI;
using UnityEngine;
using System;

public class HeroListService : MonoBehaviour
{
    [SerializeField]
    private HeroListView _redHerosView;

    [SerializeField]
    private HeroListView _blueHerosView;

    private Dictionary<HeroView, HeroEntity> _redViewsModels;
    private Dictionary<HeroView, HeroEntity> _blueViewsModels;

    private Dictionary<Team, List<HeroEntity>> _heroModels = new();

    private void Awake()
    {
        InitModels(_redHerosView.GetViews(), Team.Red, _redViewsModels);
        InitModels(_blueHerosView.GetViews(), Team.Blue, _blueViewsModels);

        //_heroModels.Add(Team.Red, _redHeroModels);
        //_heroModels.Add(Team.Blue, _blueHeroModels);

        //_redHeroModels[0].Set(new IsActiveComponent(true));
    }

    private void InitModels(IReadOnlyList<HeroView> heroViews,
        Team team,
        Dictionary<HeroView, HeroEntity> viewsModels)
    {
        foreach (Team teamName in Enum.GetValues(typeof(Team)))
        {
            _heroModels.Add(teamName, new List<HeroEntity>());

            foreach (HeroView heroView in heroViews)
            {
                HeroEntity model = heroView.GetComponent<HeroEntity>();
                model.Add(new IsActiveComponent(false));
                model.Add(new TeamComponent(team));

                viewsModels.Add(heroView, model);
            }
        }

        
    }
}