using UnityEngine;

public class LordVampEffectHandler : BaseHandler<LordVampEffect>
{
    private readonly bool[] _choises = new bool[2] { true, false };

    public LordVampEffectHandler(EventBus eventBus) : base(eventBus)
    {

    }

    protected override void RaiseEvent(LordVampEffect evnt)
    {
        EventBus.RaiseEvent(new DefaultDamageEvent(evnt.Hero, evnt.Target));

        int random = Random.Range(0, _choises.Length);

        if (_choises[random])
        {
            HPComponent hp = evnt.Hero.Get<HPComponent>();
            hp.Value += evnt.Hero.Get<DamageComponent>().value;

            evnt.Hero.Set(hp);
        }
    }
}