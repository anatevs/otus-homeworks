using Scellecs.Morpeh;
using UnityEngine;

public sealed class AnimationTakeDamageSystem : ISystem
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
            .With<TakeDamageEvent>()
            .With<AnimatorView>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            Animator animator = entity.GetComponent<AnimatorView>().value;
            animator.SetTrigger(Animator.StringToHash(MobAnimationTriggers.TakeDamage));
        }
    }

    public void Dispose()
    {
    }
}