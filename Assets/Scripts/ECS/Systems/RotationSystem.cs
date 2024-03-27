using Scellecs.Morpeh;
using UnityEngine;

public class RotationSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Rotation>()
            .With<MoveDirection>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            Vector3 moveDirection = entity.GetComponent<MoveDirection>().value;

            if (moveDirection != Vector3.zero)
            {
                ref Rotation rotation = ref entity.GetComponent<Rotation>();
                rotation.value = Quaternion.LookRotation(moveDirection);
            }
        }
    }

    public void Dispose()
    {
        
    }
}