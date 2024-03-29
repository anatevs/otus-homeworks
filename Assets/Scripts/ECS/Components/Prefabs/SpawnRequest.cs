using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct SpawnRequest : IComponent
{
    public ObjectsTypeNames typeName;
    public TeamType teamType;
    public Vector3 position;
    public Quaternion rotation;
}