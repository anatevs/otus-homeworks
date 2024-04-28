using UnityEngine;

public sealed class LordVampEffectHandler : BaseHandler<LordVampEffect>
{
    public LordVampEffectHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(LordVampEffect evnt)
    {
        EventBus.RaiseEvent(new DefaultAttackEvent(evnt.Hero, evnt.Target));

        int random = Random.Range(0, 2);

        if (random == 1)
        {
            HPComponent hp = evnt.Hero.Get<HPComponent>();
            hp.CurrentHP += evnt.Hero.Get<DamageComponent>().value;

            evnt.Hero.Set(hp);

            Debug.Log("Lord vamp effect: up self hp");
        }
    }
}