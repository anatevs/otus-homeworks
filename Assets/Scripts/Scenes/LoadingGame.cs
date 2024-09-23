using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Scripts.Scenes
{
    public class LoadingGame : MonoBehaviour
    {
        [Inject]
        private TimeService _timeService;

        private int _gameSceneID = 1;

        private async void Start ()
        {
            await _timeService.InitAsync();

            //var sceneLoading = SceneManager.LoadSceneAsync(_gameSceneID);

            await SceneManager.LoadSceneAsync(_gameSceneID);
        }
    }
}