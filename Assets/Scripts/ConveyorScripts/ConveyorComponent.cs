using Game.Engine;
using ResourcesStorage;
using UnityEngine;

namespace Conveyor
{
    public class ConveyorComponent : MonoBehaviour
    {
        public string LoadID => _loadStorage.ResourceID;

        public string UnloadID => _unloadStorage.ResourceID;

        public HarvestComponent UnloadHarvest => _unloadHarvest;

        [SerializeField]
        private ResourceStorage _loadStorage;

        [SerializeField]
        private ResourceStorage _unloadStorage;

        [SerializeField]
        private int _conversionInValue;

        [SerializeField]
        private int _conversionOutValue;

        [SerializeField]
        private HarvestComponent _unloadHarvest;

        private (int InValue, int OutValue) _conversionRatio;

        private void Awake()
        {
            _conversionRatio = (_conversionInValue, _conversionOutValue);
        }

        public void Start()
        {
            _unloadHarvest.AddCondition(CanMakeOneCycleConversion);

            _unloadHarvest.SetProcessAction(MakeOneCycleConvertion);
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

        public bool TryUnloadConveyor(string id, out int unloadedValue)
        {
            return _unloadStorage.TryRemoveResource(id, _unloadStorage.Count, out unloadedValue);
        }


        public void MakeOneCycleConvertion()
        {
            _loadStorage.RemoveResource(_conversionRatio.InValue);
            _unloadStorage.AddResource(_conversionRatio.OutValue);
        }

        private int GetConvertionInfo(int inResources, out int outResources)
        {
            var cyclesNumber = (int)(inResources / _conversionRatio.InValue);

            outResources = cyclesNumber * _conversionRatio.OutValue;

            return cyclesNumber;
        }

        private bool CanMakeOneCycleConversion()
        {
            if (!_loadStorage.CanRemoveResources(LoadID, _conversionRatio.InValue))
            {
                Debug.Log($"need at least {_conversionRatio.InValue} in load conveyor with {_loadStorage.ResourceID} resources");
                return false;
            }

            if (!_unloadStorage.CanAddResources(UnloadID, _conversionRatio.OutValue))
            {
                Debug.Log($"need at least {_conversionRatio.OutValue} free space in unload conveyor with {_unloadStorage.ResourceID} resources");
                return false;
            }

            return true;
        }
    }
}