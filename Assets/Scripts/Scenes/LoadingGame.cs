using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Scripts.Scenes
{
    public class LoadingGame : MonoBehaviour
    {
        private TimeService _timeService;

        private readonly int _gameSceneID = 1;

        [Inject]
        public void Construct(TimeService timeService)
        {
            _timeService = timeService;
        }

        private async void Start ()
        {
            await _timeService.InitAsync();

            await SceneManager.LoadSceneAsync(_gameSceneID);
        }
    }
}