using UnityEngine;
using System;

namespace ResourcesStorage
{
    [Serializable]
    public sealed class ResourceStorage
    {
        public bool IsEmpty => _count == 0;

        [SerializeField]
        private int _capacity;

        [SerializeField]
        private int _count;

        public ResourceStorage(int capacity, int count)
        {
            _capacity = capacity;
            _count = count;
        }

        public void AddResource(int value)
        {
            _count = Math.Min(_count + value, _capacity);
        }

        public void RemoveResource(int value)
        {
            _count = Math.Max(_count + value, 0);
        }

        public bool TryAddResource(int value, out int enabledValue)
        {
            if (!CanAddResources(value))
            {
                enabledValue = 0;
                return false;
            }

            AddResource(value);
            enabledValue = value;
            return true;
        }

        public bool TryRemoveResource(int value, out int enabledValue)
        {
            if (!CanRemoveResources(value))
            {
                enabledValue = 0;
                return false;
            }

            RemoveResource(value);
            enabledValue = value;
            return true;
        }

        public bool CanAddResources(int value)
        {
            var newValue = _count + value;

            if (newValue > _capacity)
            {
                return false;
            }

            return true;
        }

        public bool CanRemoveResources(int value)
        {
            var newValue = _count - value;

            if (newValue < 0)
            {
                return false;
            }

            return true;
        }

        public bool IsFull()
        {
            return _count == _capacity;
        }
    }
}