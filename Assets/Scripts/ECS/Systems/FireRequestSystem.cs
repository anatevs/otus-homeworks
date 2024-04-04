using Scellecs.Morpeh;
using UnityEngine;

public class FireRequestSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _shootFilter;
    private Stash<ObjectType> _typeStash;

    private static readonly int _fire =
        Animator.StringToHash(MobAnimationTriggers.Attack);

    public void OnAwake()
    {
        _shootFilter = this.World.Filter
            .With<FireRequest>()
            .With<Team>()
            .With<AnimatorView>()
            .Without<Inactive>()
            .Build();

        _typeStash = this.World.GetStash<ObjectType>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _shootFilter)
        {
            Animator animator = entity.GetComponent<AnimatorView>().value;
            ObjectsTypeNames typeName = _typeStash.Get(entity).value;

            if (typeName == ObjectsTypeNames.Archer)
            {
                if (entity.Has<CanFireTag>())
                {
                    animator.SetTrigger(_fire);
                    entity.RemoveComponent<CanFireTag>();
                }
            }
            else if (typeName == ObjectsTypeNames.Swordman)
            {
                entity.AddComponent<AttackingTag>();
            }

            entity.RemoveComponent<FireRequest>();
        }
    }

    public void Dispose()
    {
    }
}