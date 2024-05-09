using Equipment;
using Sample;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventoryHelper : MonoBehaviour, IDisposable
{
    [ShowInInspector]
    private readonly List<string> _equipmentsID = new List<string>();

    private Inventory _inventory;

    private Equipment.Equipment _equipment;

    private Character _character;

    private CharacterStatsNames _statNames;

    public void Construct(Inventory inventory, Equipment.Equipment equipment, Character character, CharacterStatsNames statsNames)
    {
        _inventory = inventory;
        _equipment = equipment;
        _character = character;
        _statNames = statsNames;
    }

    private void Awake()
    {
        _inventory.OnItemAdded += OnInventoryAdded;
        _inventory.OnItemRemoved += OnInventoryRemoved;
    }

    void IDisposable.Dispose()
    {
        _inventory.OnItemAdded -= OnInventoryAdded;
        _inventory.OnItemRemoved -= OnInventoryRemoved;
    }

    private void OnInventoryAdded(Item item)
    {
        if (item.Flags.HasFlag(ItemFlags.EQUPPABLE))
        {
            _equipmentsID.Add(item.Name);
        }
    }

    private void OnInventoryRemoved(Item item)
    {
        if (item.Flags.HasFlag(ItemFlags.EQUPPABLE))
        {
            _equipmentsID.Remove(item.Name);
        }
    }

    [Button]
    private void UseEquipment(string itemName)
    {
        if (_inventory.FindItem(itemName, out Item item))
        {
            EquipmentComponent component = 
                item.GetComponent<EquipmentComponent>();

            //if (_equipment.Setup(component.Type, item))
            //{
            //    string stat = _statNames.GetStatName(component.CharacterStat);
            //    _character.SetStat(stat, component.Value);
            //}
        }
    }

}