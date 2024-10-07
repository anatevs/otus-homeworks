using Cysharp.Threading.Tasks;
using Scripts.SaveLoadNamespace;
using Scripts.Time;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Scripts.Scenes
{
    public sealed class LoadingGame : MonoBehaviour
    {
        private TimeService _timeService;

        private IEnumerable<ISaveLoad> _saveLoads;

        private readonly int _gameSceneID = 1;

        [Inject]
        public void Construct(TimeService timeService, IEnumerable<ISaveLoad> saveLoads)
        {
            _timeService = timeService;

            _saveLoads = saveLoads;
        }

        private async void Start()
        {
            foreach (var saveLoad in _saveLoads)
            {
                saveLoad.Load();
            }

            await _timeService.InitAsync();

            await SceneManager.LoadSceneAsync(_gameSceneID);
        }
    }
}