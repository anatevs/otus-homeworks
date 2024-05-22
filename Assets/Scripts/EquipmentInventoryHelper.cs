using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Sample
{
    public sealed class EquipmentInventoryHelper : MonoBehaviour, IDisposable
    {
        [ShowInInspector]
        private readonly List<string> _unusedEquipmentNames = new();

        [ShowInInspector]
        private readonly List<string> _inusedEquipmentNames = new();

        [ShowInInspector]
        private Character _character;

        private Inventory _inventory;

        private Equipment _equipment;

        [Inject]
        public void Construct(Inventory inventory, Character character, Equipment equipment)
        {
            _inventory = inventory;
            _character = character;
            _equipment = equipment;
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

                string newStat = newEquipment.StatName;

                if (_equipment.TryGetItem(newEquipment.Type, out Item currItem))
                {
                    var currEquipment = currItem.GetComponent<EquipmentComponent>();

                    string currStat = currEquipment.StatName;

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

                _inusedEquipmentNames.Add(itemName);
                _unusedEquipmentNames.Remove(itemName);
            }
        }

        [Button]
        private void RemoveEquipment(string itemName)
        {
            if (_inventory.FindItem(itemName, out Item newItem))
            {
                EquipmentComponent component =
                    newItem.GetComponent<EquipmentComponent>();

                string stat = component.StatName;

                if (_equipment.TryGetItem(component.Type, out Item item))
                {
                    _equipment.RemoveItem(component.Type, item);
                    UpdateStat(stat, -component.Value);
                }

                _inusedEquipmentNames.Remove(itemName);
                _unusedEquipmentNames.Add(itemName);
            }
        }

        private void UpdateStat(string statName, float addValue)
        {
            _character.SetStat(statName, _character.GetStat(statName) + addValue);
        }
    }
}