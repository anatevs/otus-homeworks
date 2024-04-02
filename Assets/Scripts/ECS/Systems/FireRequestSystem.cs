using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using VContainer;

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
            entity.AddComponent<Standing>();

            //start fire animation...
            Animator animator = entity.GetComponent<AnimatorView>().value;

            animator.SetTrigger(_fire);
            //Weapon weapon = entity.GetComponent<Weapon>();

            //entity.AddComponent<SpawnRequest>() = new SpawnRequest()
            //{
            //    type = weapon.projectileType,
            //    team = entity.GetComponent<Team>().value,
            //    transform = weapon.firePoint
            //};

            entity.RemoveComponent<FireRequest>();
        }
    }

    public void Dispose()
    {
    }
}