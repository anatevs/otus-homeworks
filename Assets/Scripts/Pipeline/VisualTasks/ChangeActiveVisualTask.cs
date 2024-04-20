using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeActiveVisualTask : Task
{
    private readonly HeroServiceView _heroServiceView;
    private readonly InfoComponent _prevHero;
    private readonly InfoComponent _currentHero;

    public ChangeActiveVisualTask(HeroServiceView heroServiceView, InfoComponent prevHero, InfoComponent currentHero)
    {
        _heroServiceView = heroServiceView;
        _prevHero = prevHero;
        _currentHero = currentHero;
    }

    protected override void OnRun()
    {
        _heroServiceView.SetActiveTeamAndHero(_prevHero, false);
        _heroServiceView.SetActiveTeamAndHero(_currentHero, true);

        Finish();
    }
}
