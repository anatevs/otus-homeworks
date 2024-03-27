using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TransformViewProvider : MonoProvider<TransformView>
{
    protected override void Initialize()
    {
        base.Initialize();
        Entity.GetComponent<TransformView>().value = transform;
    }
}