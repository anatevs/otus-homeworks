using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        private GameManagerData _gameManagerData;

        [Inject]
        public void Construct(GameManagerData gameManagerData)
        {
            _gameManagerData = gameManagerData;
        }

        private void Awake()
        {
            GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
            _gameManagerData.AddListeners(rootGameObjects);
        }

        private void Update()
        {
            if (_gameState == GameState.Playing)
            {
                _gameManagerData.UpdateListeners();
            }
            else if (_gameState == GameState.Paused)
            {
                _gameManagerData.PausedUpdateListeners();
            }
        }

        private void FixedUpdate()
        {
            if (_gameState == GameState.Playing)
            {
                _gameManagerData.FixedUpdateListeners();
            }
            else if (_gameState == GameState.Paused)
            {
                _gameManagerData.PausedFixedUpdateListeners();
            }
        }

        public void StartGame()
        {   
            if (_gameState != GameState.NotReady)
            {
                return;
            }
            _gameManagerData.StartListeners();
            _gameState = GameState.Playing;
        }

        public void PauseGame()
        {
            if (_gameState != GameState.Playing)
            {
                return;
            }
            _gameManagerData.PauseListeners();
            _gameState = GameState.Paused;
        }

        public void ResumeGame()
        {
            if (_gameState != GameState.Paused)
            {
                return;
            }
            _gameManagerData.ResumeListeners();
            _gameState = GameState.Playing;
        }

        public void FinishGame()
        {
            if (_gameState is GameState.Finished or GameState.NotReady)
            {
                return;
            }
            _gameManagerData.FinishListeners();
            _gameState = GameState.Finished;
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}