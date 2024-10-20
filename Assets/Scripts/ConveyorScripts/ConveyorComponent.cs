using ResourcesStorage;
using UnityEngine;

namespace Conveyor
{
    public class ConveyorComponent : MonoBehaviour
    {
        public string LoadID => _loadStorage.ResourceID;

        public string UnloadID => _unloadStorage.ResourceID;

        [SerializeField]
        private ResourceStorage _loadStorage;

        [SerializeField]
        private ResourceStorage _unloadStorage;

        [SerializeField]
        private int _conversionInValue;

        [SerializeField]
        private int _conversionOutValue;

        [SerializeField]
        private Transform _loadPoint;

        [SerializeField]
        private Transform _unloadPoint;

        [SerializeField]
        private float _stopDistance;

        private (int InValue, int OutValue) _conversionRatio;

        private void Awake()
        {
            _conversionRatio = (_conversionInValue, _conversionOutValue);
        }

        public void LoadToConveyor(int value)
        {
            _loadStorage.AddResource(value);
        }

        public void UnloadConveyor(int value)
        {
            _unloadStorage.RemoveResource(value);
        }

        public bool TryLoadConveyor(string id, int value, out int enabledValue)
        {
            return _loadStorage.TryAddResource(id, value, out enabledValue);
        }

        public bool TryUnloadConveyor(string id, int value, out int enabledValue)
        {
            return _unloadStorage.TryRemoveResource(id, value, out enabledValue);
        }

        public void Convert()
        {
            var cyclesNumber = GetConvertionInfo(_loadStorage.Capacity, out _);

            for (int cycle = 0; cycle < cyclesNumber; cycle++)
            {
                if (!TryMakeOneCycleConversion())
                {
                    break;
                }
            }
        }

        private int GetConvertionInfo(int inResources, out int outResources)
        {
            var cyclesNumber = (int)(inResources / _conversionRatio.InValue);

            outResources = cyclesNumber * _conversionRatio.OutValue;

            return cyclesNumber;
        }

        private bool TryMakeOneCycleConversion()
        {
            if (!_loadStorage.CanRemoveResources(_conversionRatio.InValue))
            {
                Debug.Log($"need at least {_conversionRatio.InValue} in load conveyor with {_loadStorage.ResourceID} resources");
                return false;
            }

            if (!_unloadStorage.CanAddResources(_conversionRatio.OutValue))
            {
                Debug.Log($"need at least {_conversionRatio.OutValue} free space in unload conveyor with {_unloadStorage.ResourceID} resources");
                return false;
            }

            return true;
        }
    }
}