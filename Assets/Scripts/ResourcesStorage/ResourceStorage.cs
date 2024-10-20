﻿using UnityEngine;
using System;

namespace ResourcesStorage
{
    public class ResourceStorage : MonoBehaviour
    {
        public event Action OnStateChanged;

        public string ResourceID => _resourceIDConfig.ID;

        public bool IsFull => _count == _capacity;

        public bool IsEmpty => _count == 0;

        public int Count => _count;

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

        private bool CanAddResources(int value)
        {
            return _count + value <= _capacity;
        }

        private bool CanRemoveResources(int value)
        {
            return _count - value >= 0;
        }
    }
}