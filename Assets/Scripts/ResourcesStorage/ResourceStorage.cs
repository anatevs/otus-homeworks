using UnityEngine;
using System;
using System.Globalization;

namespace ResourcesStorage
{
    public class ResourceStorage : MonoBehaviour
    {
        public event Action OnStateChanged;

        public string ResourceID => _resourceIDConfig.ID;

        public bool IsFull => _count == _capacity;

        public bool IsEmpty => _count == 0;

        public int Count => _count;

        public int Capacity => _capacity;

        [SerializeField]
        private ResourceID _resourceIDConfig;

        [SerializeField]
        private int _capacity;

        [SerializeField]
        private int _count;

        public bool IsNotFull()
        {
            return _count < _capacity;
        }

        public void AddResource(int value)
        {
            _count = Mathf.Min(_count + value, _capacity);
            OnStateChanged?.Invoke();
        }

        public void RemoveResource(int value)
        {
            _count = Mathf.Max(_count - value, 0);
            OnStateChanged?.Invoke();
        }

        public bool TryAddResource(string id, int value, out int enabledValue)
        {
            if (id != ResourceID)
            {
                enabledValue = 0;
                return false;
            }

            if (!CanAddResources(value))
            {
                enabledValue = _capacity - _count;
                return false;
            }

            AddResource(value);
            enabledValue = value;
            return true;
        }

        public bool TryRemoveResource(string id, int value, out int enabledValue)
        {
            if (id != ResourceID)
            {
                enabledValue = 0;
                return false;
            }

            if (!CanRemoveResources(value))
            {
                enabledValue = _count;
                return false;
            }

            RemoveResource(value);
            enabledValue = value;
            return true;
        }

        public bool CanAddResources(int value)
        {
            return _count + value <= _capacity;
        }

        public bool CanRemoveResources(int value)
        {
            return _count - value >= 0;
        }
    }
}