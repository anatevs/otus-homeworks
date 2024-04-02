using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class MovableProvider : UniversalProvider
{
    protected override void Initialize()
    {
        base.Initialize();
        Entity.AddComponent<Position>().value = transform.position;
        Entity.AddComponent<Rotation>().value = transform.rotation;
        Entity.AddComponent<MoveDirection>().value = transform.forward;
        Entity.AddComponent<TransformView>().value = transform;
    }
}