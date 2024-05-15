using Equipment;
using System;
using UnityEngine;

[Serializable]
public sealed class EquipmentComponent
{
    [field: SerializeField]
    public EquipmentType Type { get; private set; }

    [field: SerializeField]
    public CharacterStat CharacterStat { get; private set; }

    [field: SerializeField]
    public int Value { get; private set; }

    public EquipmentComponent(EquipmentType type, CharacterStat characterStat, int value)
    {
        Type = type;
        CharacterStat = characterStat;
        Value = value;
    }
}