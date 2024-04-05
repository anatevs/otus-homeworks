using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public sealed class BaseProvider : UniversalProvider
{
    [SerializeField]
    BaseHPView hpView;
    protected override void Initialize()
    {
        base.Initialize();

        Entity.AddComponent<BaseFlag>();
        Entity.AddComponent<Position>() = new Position() { value = transform.position };
    }

    private void Update()
    {
        hpView.SetText(Entity.GetComponent<Team>().value,
            Entity.GetComponent<Health>().value);
    }
}