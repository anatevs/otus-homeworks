using Scellecs.Morpeh;
using UnityEngine;

public sealed class MobProvider : MovableProvider
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    AnimationDispatcher _dispatcher;

    [SerializeField]
    ParticleSystem _damageVFX;

    protected override void Initialize()
    {
        base.Initialize();

        Entity.SetComponent(new StandingFlag());
        Entity.SetComponent(new CanFireTag());
        Entity.SetComponent(new MobFlag());
        Entity.SetComponent(new Target());

        Entity.SetComponent(new AnimatorView() { value = _animator });
        _dispatcher.OnEventReceived += ReceiveAnimEvent;
    }


    private void ReceiveAnimEvent(string eventName)
    {
        if (eventName == MobAnimationEvents.Shoot)
        {
            Entity.SetComponent(new SpawnProjectileRequest());
        }

        else if (eventName == MobAnimationEvents.Death)
        {
            Entity.AddComponent<UnspawnRequest>();
        }
    }
}