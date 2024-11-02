using UnityEngine;
using UnityEngine.AddressableAssets;


namespace SampleGame
{
    public class LocationsTriggers : MonoBehaviour
    {
        [SerializeField]
        private Transform _locationsParent;

        [SerializeField]
        private int _locationIndex = 2;

        private readonly string[] _locationName = new string[2]
        {
            "Assets/Game/Prefabs/Gameplay/SceneLocations/Location",
            ".prefab"
        };

        private void Start()
        {
            LoadLocation(_locationIndex);
        }

        public async void LoadLocation(int locationIndex)
        {
            var path = $"{_locationName[0]}{locationIndex}{_locationName[1]}";

            var operation = Addressables.LoadAssetAsync<GameObject>(path);

            var prefab = await operation.Task;

            Instantiate(prefab, _locationsParent);
        }
    }
}