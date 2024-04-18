using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTurnTask : Task
{
    HeroListService _heroListService;

    public VisualTurnTask(HeroListService heroListService)
    {
        _heroListService = heroListService;
    }

    protected override void OnRun()
    {
        //_heroListService.Attack(attackEvent.hero, attackEvent.target);

        Finish();
    }

    //private void Attack()
}
