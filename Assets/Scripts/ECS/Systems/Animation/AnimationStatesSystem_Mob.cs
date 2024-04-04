using Scellecs.Morpeh;
using UnityEngine;

public sealed class AnimationStatesSystem_Mob : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private static readonly int _mainState = Animator.StringToHash("MainState");

    private Filter _filter;
    private Stash<StandingFlag> _standing;
    private Stash<Inactive> _inactive;
    private Stash<AttackingTag> _attacking;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<MobFlag>()
            .With<AnimatorView>()
            .Build();

        _standing = this.World.GetStash<StandingFlag>();
        _inactive = this.World.GetStash<Inactive>();
        _attacking = this.World.GetStash<AttackingTag>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            Animator animator = entity.GetComponent<AnimatorView>().value;
            if (_inactive.Has(entity))
            {
                animator.SetInteger(_mainState, (int)MobAnimationStates.Idle);
            }
            else
            {
                if (_standing.Has(entity))
                {
                    animator.SetInteger(_mainState, (int)MobAnimationStates.Idle);
                }
                else
                {
                    animator.SetInteger(_mainState, (int)MobAnimationStates.Move);
                }

                if (_attacking.Has(entity))
                {
                    animator.SetInteger(_mainState, (int)MobAnimationStates.Attack);
                }
            }
        }
    }

    public void Dispose()
    {
    }
}