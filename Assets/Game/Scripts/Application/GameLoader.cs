using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class GameLoader
    {
        //TODO: Сделать через Addressables
        public void UnloadGame()
        {
            SceneManager.UnloadSceneAsync("Game");
        }
        
        //TODO: Сделать через Addressables
        public async void LoadGame()
        {
            await Addressables.LoadSceneAsync("Assets/Game/Scenes/Game.unity").Task;
            //SceneManager.LoadScene("Game");
        }
    }
}