using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class MovableProvider : UniversalProvider
{
    protected override void Initialize()
    {
        base.Initialize();
        Entity.GetComponent<Position>().value = transform.position;
        Entity.GetComponent<Rotation>().value = transform.rotation;
        Entity.GetComponent<MoveDirection>().value = transform.forward;
        Entity.GetComponent<TransformView>().value = transform;
    }
}