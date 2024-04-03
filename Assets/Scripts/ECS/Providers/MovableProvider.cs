using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class MovableProvider : UniversalProvider
{
    protected override void Initialize()
    {
        base.Initialize();

        Entity.SetComponent<Position>(new Position() { value = transform.position });
        Entity.SetComponent<Rotation>(new Rotation() { value = transform.rotation });
        Entity.SetComponent<MoveDirection>(new MoveDirection() { value = transform.forward });
        Entity.SetComponent<TransformView>(new TransformView() { value = transform });
    }
}