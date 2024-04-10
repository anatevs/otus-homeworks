using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class HerosService : MonoBehaviour
{
    [SerializeField]
    private HeroListView _redHerosView;

    [SerializeField]
    private HeroListView _blueHerosView;

    private List<HeroModel> _redHeroModels;
    private List<HeroModel> _blueHeroModels;

    private void Awake()
    {
        InitModels(_redHeroModels, _redHerosView.GetViews());
        InitModels(_blueHeroModels, _blueHerosView.GetViews());

        //_redHeroModels[0].Entity
    }

    private void InitModels(List<HeroModel> heroModels, IReadOnlyList<HeroView> heroViews)
    {
        foreach(HeroView heroView in heroViews)
        {
            heroModels.Add(heroView.GetComponent<HeroModel>());
        }
    }
}