using System;
using UnityEngine;

[Serializable]
public struct StatStruct
{
    [field: SerializeField]
    public CharacterStat Name { get; private set; }

    [field: SerializeField]
    public int Value { get; private set; }
}