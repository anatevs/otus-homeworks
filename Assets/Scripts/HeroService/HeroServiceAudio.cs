using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using VContainer.Unity;
using Sounds;

public class HeroServiceAudio : IInitializable, IDisposable
{
    private readonly UIService _uiService;

    private readonly Dictionary<Team, List<HeroAudio>> _heroAudioLists = new();

    public HeroServiceAudio(UIService uiService)
    {
        _uiService = uiService;
    }

    void IInitializable.Initialize()
    {
        InitListAudios();

        //_viewLists[Team.Red].OnHeroClicked += OnClickedHeroRed;
        //_viewLists[Team.Blue].OnHeroClicked += OnClickedHeroBlue;
    }

    private void InitListAudios()
    {
        foreach (Team teamName in Enum.GetValues(typeof(Team)))
        {
            List<HeroAudio> heroAudios = new();

            HeroListView heroListView;

            if (teamName == Team.Red)
            {
                heroListView = _uiService.GetRedPlayer();
            }
            else
            {
                heroListView = _uiService.GetBluePlayer();
            }

            IReadOnlyList<HeroView> views = heroListView.GetViews();

            for (int i = 0; i < views.Count; i++)
            {
                HeroAudio heroAudio = views[i].GetComponent<HeroAudio>();
                if (heroAudio == null)
                {
                    throw new Exception($"no HeroAudio component attached to {views[i].name}");
                }
                heroAudios.Add(heroAudio);
            }

            _heroAudioLists.Add(teamName, heroAudios);
        }
    }

    public void PlaySound(InfoComponent info, SoundType soundType)
    {
        _heroAudioLists[info.team][info.id].PlaySound(soundType);
    }

    void IDisposable.Dispose()
    {
        //_viewLists[Team.Red].OnHeroClicked -= OnClickedHeroRed;
        //_viewLists[Team.Blue].OnHeroClicked -= OnClickedHeroBlue;
    }
}