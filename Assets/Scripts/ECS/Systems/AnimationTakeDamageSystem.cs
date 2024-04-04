using Scellecs.Morpeh;
using UnityEngine;

public class AnimationTakeDamageSystem : ISystem
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
            .With<TakeDamageEvent>()
            .With<AnimatorView>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _changeFilter)
        {
            Animator animator = entity.GetComponent<AnimatorView>().value;
            animator.SetTrigger(Animator.StringToHash(MobAnimationTriggers.TakeDamage));
        }
    }

    public void Dispose()
    {
    }
}