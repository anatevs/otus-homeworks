using System;
using System.Collections.Generic;
using Sample;
using Sirenix.OdinInspector;

namespace Equipment
{
    public sealed class Equipment
    {
        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;
        public event Action<Item, Item> OnItemChanged;

        private readonly Dictionary<EquipmentType, Item> _items = new();

        public Item GetItem(EquipmentType type)
        {
            return _items[type];
        }

        public bool TryGetItem(EquipmentType type, out Item result)
        {
            return _items.TryGetValue(type, out result);
        }

        public void RemoveItem(EquipmentType type, Item item)
        {
            _items.Remove(type);

            OnItemRemoved?.Invoke(item);
        }

        public void AddItem(EquipmentType type, Item item)
        {
            _items.Add(type, item);

            OnItemAdded?.Invoke(item);
        }

        public void ChangeItem(EquipmentType type, Item item)
        {
            Item prevItem = _items[type];
            _items[type] = item;

            OnItemChanged?.Invoke(prevItem, item);
        }

        public bool HasItemOfType(EquipmentType type)
        {
            return _items.ContainsKey(type);
        }

        public bool HasItem(Item item)
        {
            var component = item.GetComponent<EquipmentComponent>();
            if (TryGetItem(component.Type, out var equipmentItem))
            {
                if (equipmentItem.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public KeyValuePair<EquipmentType, Item>[] GetItems()
        {
            KeyValuePair<EquipmentType, Item>[] res =
                new KeyValuePair<EquipmentType, Item>[_items.Count];

            int i = 0;
            foreach (KeyValuePair<EquipmentType, Item> pair in _items)
            {
                res[i] = pair;
            }

            return res;
        }
    }
}