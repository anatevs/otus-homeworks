using Scellecs.Morpeh;
using UnityEngine;

public class ChangeHealthSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _changeFilter;

    public void OnAwake()
    {
        _changeFilter = this.World.Filter
            .With<HealthChangeRequest>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _changeFilter)
        {
            entity.GetComponent<Health>().value -=
                entity.GetComponent<HealthChangeRequest>().value;

            //Debug.Log($"health of {entity} now is {entity.GetComponent<Health>().value}");

            entity.RemoveComponent<HealthChangeRequest>();
        }
    }

    public void Dispose()
    {
    }
}