using ResourcesStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyorScripts
{
    public sealed class Conveyor
    {
        private string _loadID;

        private string _unloadID;

        private int _loadCapacity;

        private int _unloadCapacity;

        private ResourceStoragePlainCS _loadStorage;

        private ResourceStoragePlainCS _unloadStorage;

        private (int InValue, int OutValue) _conversionRatio;

        public Conveyor(int loadValue, int unloadValue, (int, int) convertionRatio)
        {
            _loadStorage = new ResourceStoragePlainCS(_loadCapacity, loadValue);
            _unloadStorage = new ResourceStoragePlainCS(_unloadCapacity, unloadValue);
            _conversionRatio = convertionRatio;
        }

        public bool TryLoadConveyor(int value, out int enabledValue)
        {
            return _loadStorage.TryAddResource(value, out enabledValue);
        }

        public bool TryUnloadConveyor(int value, out int enabledValue)
        {
            return _unloadStorage.TryRemoveResource(value, out enabledValue);
        }

        public void Convert()
        {
            var cyclesNumber = GetConvertionInfo(_loadCapacity, out _);

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
                Debug.Log($"need at least {_conversionRatio.InValue} in load conveyor with {_loadID} resources");
                return false;
            }

            if (!_unloadStorage.CanAddResources(_conversionRatio.OutValue))
            {
                Debug.Log($"need at least {_conversionRatio.OutValue} free space in unload conveyor with {_unloadID} resources");
                return false;
            }

            return true;
        }
    }
}