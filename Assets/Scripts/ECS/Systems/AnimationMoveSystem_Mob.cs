using Scellecs.Morpeh;
using UnityEngine;

public class AnimationMoveSystem_Mob : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private static readonly int _mainState = Animator.StringToHash("MainState");

    private Filter _filter;
    private Stash<Standing> _standing;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<MobFlag>()
            .With<AnimatorView>()
            .Build();

        _standing = this.World.GetStash<Standing>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            Animator animator = entity.GetComponent<AnimatorView>().value;
            if (_standing.Has(entity))
            {
                Debug.Log($" {entity} standing");
                animator.SetInteger(_mainState, (int)MobAnimationStates.Idle);
            }
            else
            {
                Debug.Log($"{entity} moving");
                animator.SetInteger(_mainState, (int)MobAnimationStates.Move);
            }
        }
    }

    public void Dispose()
    {
    }
}
