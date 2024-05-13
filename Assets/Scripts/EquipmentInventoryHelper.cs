using Sample;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EquipmentInventoryHelper : MonoBehaviour, IDisposable
{
    [ShowInInspector]
    private readonly List<string> _equipmentsNames = new List<string>();

    private Inventory _inventory;

    private Equipment.Equipment _equipment;

    [ShowInInspector]
    private Character _character;

    private CharacterStatsNames _statNames;

    [Inject]
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
            _equipmentsNames.Add(item.Name);
        }
    }

    private void OnInventoryRemoved(Item item)
    {
        if (item.Flags.HasFlag(ItemFlags.EQUPPABLE))
        {
            _equipmentsNames.Remove(item.Name);
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
                    //int currStatVal = _character.GetStat(newStat);
                    UpdateStat(newStat, newEquipment.Value - currEquipment.Value);
                    //_character.SetStat(newStat, _character.GetStat(newStat) - currEquipment.Value + newEquipment.Value);
                }
                else
                {
                    UpdateStat(currStat, -currEquipment.Value);
                    UpdateStat(newStat, newEquipment.Value);
                    //_character.SetStat(currStat, _character.GetStat(currStat) - currEquipment.Value);
                    //_character.SetStat(newStat, _character.GetStat(newStat) + newEquipment.Value);
                }

                _equipment.ChangeItem(newEquipment.Type, newItem);
            }
            else
            {
                UpdateStat(newStat, newEquipment.Value);

                _equipment.AddItem(newEquipment.Type, newItem);
                //_character.SetStat(newStat, _character.GetStat(newStat) + newEquipment.Value);
            }
        }
    }

    private void UpdateStat(string statName, int addValue)
    {
        _character.SetStat(statName, _character.GetStat(statName) + addValue);
    }
}