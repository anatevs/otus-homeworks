using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public sealed class BaseProvider : UniversalProvider
{
    [SerializeField]
    private BaseHPView _hpView;

    [SerializeField]
    private ParticleSystem _destroyVFX;

    [SerializeField]
    VFXDispatcher _vfxDispatcher;

    protected override void Initialize()
    {
        base.Initialize();

        Entity.AddComponent<BaseFlag>();
        Entity.AddComponent<Position>() = new Position() { value = transform.position };

        Entity.AddComponent<DestroyVFX>() = new DestroyVFX() { value = _destroyVFX};

        _vfxDispatcher.Init(_destroyVFX);
        _vfxDispatcher.OnReceiveEvent += ReceiveVFXEvent;
    }

    private void Update()
    {
        _hpView.SetText(Entity.GetComponent<Team>().value,
            Entity.GetComponent<Health>().value);
    }

    private void ReceiveVFXEvent(VFXEventNames eventName)
    {
        if (eventName == VFXEventNames.FinishGame)
        {
            Entity.SetComponent(new FinishGameRequest());
        }
    }

    protected override void Deinitialize()
    {
        base.Deinitialize();
        _vfxDispatcher.OnReceiveEvent -= ReceiveVFXEvent;
    }
}