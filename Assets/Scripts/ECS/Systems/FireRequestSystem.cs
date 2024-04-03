using Scellecs.Morpeh;
using UnityEngine;

public class FireRequestSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    private static readonly int _fire =
        Animator.StringToHash(MobAnimationTriggers.Attack);

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<FireRequest>()
            .With<Weapon>()
            .With<Team>()
            .With<AnimatorView>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            if (entity.Has<CanFireTag>())
            {
                Animator animator = entity.GetComponent<AnimatorView>().value;
                animator.SetTrigger(_fire);

                entity.RemoveComponent<CanFireTag>();
            }
            entity.RemoveComponent<FireRequest>();
        }
    }

    public void Dispose()
    {
    }
}