using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        public async void LoadMenu()
        {
            await Addressables.LoadSceneAsync("Assets/Game/Scenes/Menu.unity").Task;
        }
    }
}