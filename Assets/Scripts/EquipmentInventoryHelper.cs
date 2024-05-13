using Sample;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public sealed class EquipmentInventoryHelper : MonoBehaviour, IDisposable
{
    [ShowInInspector]
    private readonly List<string> _unusedEquipmentNames = new();

    [ShowInInspector]
    private Character _character;

    private Inventory _inventory;

    private Equipment.Equipment _equipment;

    private CharacterStatNames _statNames;

    [Inject]
    public void Construct(Inventory inventory, Character character, Equipment.Equipment equipment, CharacterStatNames statsNames)
    {
        _inventory = inventory;
        _character = character;
        _equipment = equipment;
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
            _unusedEquipmentNames.Add(item.Name);
        }
    }

    private void OnInventoryRemoved(Item item)
    {
        if (item.Flags.HasFlag(ItemFlags.EQUPPABLE))
        {
            _unusedEquipmentNames.Remove(item.Name);
        }
    }

    [Button]
    private void UseEquipment(string itemName)
    {
        if (_inventory.FindItem(itemName, out Item newItem))
        {
            EquipmentComponent newEquipment = 
                newItem.GetComponent<EquipmentComponent>();

            string newStat = _statNames.GetStatName(newEquipment.CharacterStat);

            if (_equipment.TryGetItem(newEquipment.Type, out Item currItem))
            {
                var currEquipment = currItem.GetComponent<EquipmentComponent>();

                string currStat = _statNames.GetStatName(currEquipment.CharacterStat);

                if (currStat == newStat)
                {
                    UpdateStat(newStat, newEquipment.Value - currEquipment.Value);
                }
                else
                {
                    UpdateStat(currStat, -currEquipment.Value);
                    UpdateStat(newStat, newEquipment.Value);
                }

                _equipment.ChangeItem(newEquipment.Type, newItem);

                _unusedEquipmentNames.Add(currItem.Name);
            }
            else
            {
                UpdateStat(newStat, newEquipment.Value);

                _equipment.AddItem(newEquipment.Type, newItem);
            }

            _unusedEquipmentNames.Remove(newItem.Name);
        }
    }

    private void UpdateStat(string statName, int addValue)
    {
        _character.SetStat(statName, _character.GetStat(statName) + addValue);
    }
}