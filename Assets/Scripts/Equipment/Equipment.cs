using System;
using System.Collections.Generic;
using UnityEngine;
using Sample;

namespace Equipment
{
    //TODO: Реализовать экипировку
    public sealed class Equipment
    {
        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;
        public event Action<Item> OnItemChanged; 

        private readonly Dictionary<EquipmentType, Item> _items = new();

        public bool Setup(EquipmentType type, Item item)
        {
            if (!_items.TryAdd(type, item))
            {
                Debug.Log($"character also use other " +
                    $"equipment ({_items[type]}) at {type}");

                return false;
            }

            return true;
        }

        public Item GetItem(EquipmentType type)
        {
            throw new NotImplementedException();
        }

        public bool TryGetItem(EquipmentType type, out Item result)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(EquipmentType type, Item item)
        {
            throw new NotImplementedException();
        }

        public void AddItem(EquipmentType type, Item item)
        {
            throw new NotImplementedException();
        }

        public void ChangeItem(EquipmentType type, Item item)
        {
            throw new NotImplementedException();
        }

        public bool HasItem(EquipmentType type)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<EquipmentType, Item>[] GetItems()
        {
            throw new NotImplementedException();
        }
    }
}