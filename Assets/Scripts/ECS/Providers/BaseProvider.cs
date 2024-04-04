using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class BaseProvider : UniversalProvider
{
    protected override void Initialize()
    {
        base.Initialize();

        Entity.AddComponent<BaseFlag>();
        Entity.AddComponent<Position>() = new Position() { value = transform.position };
    }
}