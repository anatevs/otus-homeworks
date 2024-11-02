using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        //TODO: Сделать через Addressables
        public async void LoadMenu()
        {
            await Addressables.LoadSceneAsync("Assets/Game/Scenes/Menu.unity").Task;
            //SceneManager.LoadScene("Menu");
        }
    }
}