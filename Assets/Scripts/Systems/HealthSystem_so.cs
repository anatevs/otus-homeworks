using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem_so))]
public sealed class HealthSystem_so : UpdateSystem
{
    private Filter _filter;
    public override void OnAwake()
    {
        _filter = this.World.Filter.With<Health>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach(var entity in _filter)
        {
            ref var healthComponent = ref entity.GetComponent<Health>();
            Debug.Log(healthComponent.value);
        }
    }
}