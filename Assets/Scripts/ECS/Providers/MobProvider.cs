using Scellecs.Morpeh;
using UnityEngine;

public class MobProvider : MovableProvider
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    AnimationDispatcher _dispatcher;

    protected override void Initialize()
    {
        base.Initialize();
        Entity.AddComponent<AnimatorView>().value = _animator;
        _dispatcher.OnEventReceived += ReceiveAnimEvent;
    }


    private void ReceiveAnimEvent(string eventName)
    {
        if (eventName == "Shoot")
        {
            Entity.AddComponent<SpawnProjectileRequest>();
        }

        else if (eventName == "Death")
        {
            Entity.AddComponent<UnspawnRequest>();
        }
    }
}