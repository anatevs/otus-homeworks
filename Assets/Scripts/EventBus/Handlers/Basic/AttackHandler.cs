using System;
using System.Collections.Generic;

public sealed class AttackHandler : BaseHandler<AttackEvent>
{
    private readonly HeroListService _heroListService;

    public AttackHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(AttackEvent evnt)
    {
        HeroEntity hero = evnt.hero;
        HeroEntity target = evnt.target;

        if (hero.TryGet(out WeaponComponent weapon))
        {
            IAttackEffect effect = weapon.effect;
            if (effect is not MediatorEffect)
            {
                effect.Hero = hero;
                effect.Target = target;
                EventBus.RaiseEvent(effect);
            }
            else
            {
                EventBus.RaiseEvent(new DefaultAttackEvent(hero, target));
            }
        }
        else
        {
            EventBus.RaiseEvent(new DefaultAttackEvent(hero, target));
        }

        ApplyMediator();
    }

    private void ApplyMediator()
    {
        foreach (Team team in Enum.GetValues(typeof(Team)))
        {
            IReadOnlyList<HeroEntity> entities = _heroListService.GetValidEntities(team);
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].TryGet(out WeaponComponent weapon))
                {
                    if (weapon.effect is MediatorEffect mediator)
                    {
                        mediator.Hero = entities[i];
                        mediator.TeammateEntities = entities;
                        EventBus.RaiseEvent(mediator);

                        return;
                    }
                }
            }
        }
    }
}