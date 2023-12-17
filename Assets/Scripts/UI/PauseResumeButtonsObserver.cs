using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class PauseResumeButtonsObserver : MonoBehaviour,
        IStartGame,
        IFinishGame
    {

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private GameManager _gameManager;


        public void OnStart()
        {
            _pauseButton.onClick.AddListener(_gameManager.PauseGame);
            _resumeButton.onClick.AddListener(_gameManager.ResumeGame);
        }

        public void OnFinish()
        {
            _pauseButton.onClick.RemoveListener(_gameManager.PauseGame);
            _resumeButton.onClick.RemoveListener(_gameManager.ResumeGame);
        }
    }
}