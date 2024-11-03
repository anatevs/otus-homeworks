using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly string _scenePath = "Assets/Game/Scenes/Game.unity";

        private AsyncOperationHandle _operationHandle;

        public void UnloadGame()
        {
            Addressables.UnloadSceneAsync(_operationHandle);
        }

        public async void LoadGame()
        {
            _operationHandle = Addressables.LoadSceneAsync(_scenePath);
            await _operationHandle.Task;
        }
    }
}