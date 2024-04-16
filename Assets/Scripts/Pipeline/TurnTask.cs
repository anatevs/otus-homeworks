using UnityEngine;

public class TurnTask : Task
{
    private readonly HeroListService _heroListService;
    private readonly CurrentTeamData _teamData;
    private readonly EventBus _eventBus;

    private readonly int _backDamage = 1;

    public TurnTask(EventBus eventBus, HeroListService heroListService, CurrentTeamData teamData)
    {
        _eventBus = eventBus;
        _heroListService = heroListService;
        _teamData = teamData;
    }

    protected override void OnRun()
    {
        _heroListService.OnViewClicked += OnClickedHero;

        Debug.Log("Run turn task");
    }

    protected override void OnFinished()
    {
        _heroListService.OnViewClicked -= OnClickedHero;
        Debug.Log("turn task is finished");
    }

    private void OnClickedHero(HeroEntity clickedEntity)
    {
        Team team = clickedEntity.Get<TeamComponent>().value;
        Debug.Log(team);
        if (team != _teamData.Enemy)
        {
            return;
        }
        else
        {
            HeroEntity playerHero = _heroListService.GetCurrentActive(_teamData.Player);
            _eventBus.RaiseEvent(new AttackEvent(clickedEntity, playerHero));
            _eventBus.RaiseEvent(new DealDamageEvent(playerHero, _backDamage));

            _eventBus.RaiseEvent(new NextMoveEvent(playerHero));
        }

        Finish();
    }
}