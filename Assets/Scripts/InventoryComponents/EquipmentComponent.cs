using Equipment;
using System;
using UnityEngine;

[Serializable]
public class EquipmentComponent
{
    [field: SerializeField]
    public EquipmentType Type { get; private set; }

    [field: SerializeField]
    public CharacterStat CharacterStat { get; private set; }

    [field: SerializeField]
    public int Value { get; private set; }
}