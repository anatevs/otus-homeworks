using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public class LocationsLoader : MonoBehaviour
    {
        [SerializeField]
        private Transform _locationsParent;

        private readonly string[] _locationName = new string[2]
        {
            "Assets/Game/Prefabs/Gameplay/SceneLocations/Location",
            ".prefab"
        };

        private readonly List<string> _loadedLocations = new();

        public async void LoadLocation(string locationIndex)
        {
            if (IsLocationLoaded(locationIndex))
            {
                return;
            }

            var path = $"{_locationName[0]}{locationIndex}{_locationName[1]}";

            var operation = Addressables.LoadAssetAsync<GameObject>(path);

            var prefab = await operation.Task;

            Instantiate(prefab, _locationsParent);

            _loadedLocations.Add(locationIndex);

            Addressables.Release(operation);
        }

        private bool IsLocationLoaded(string locationIndex)
        {
            return _loadedLocations.Contains(locationIndex);
        }
    }
}