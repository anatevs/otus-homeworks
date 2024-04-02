using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class MoveDirectionProvider : MonoProvider<MoveDirection>
{
    protected override void Initialize()
    {
        base.Initialize();
        Entity.GetComponent<MoveDirection>().value = transform.forward;
    }
}