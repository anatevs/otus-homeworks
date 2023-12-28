using UnityEngine.UI;

namespace ShootEmUp
{
    public class PauseResumeButtonsObserver : 
        IStartGame,
        IFinishGame
    {
        private Button _pauseButton;
        
        private Button _resumeButton;
        
        private GameManager _gameManager;

        public PauseResumeButtonsObserver(GameManager gameManager, PauseResumeButtons pauseResumeButtons)
        {
            _gameManager = gameManager;
            _pauseButton = pauseResumeButtons.pauseButton;
            _resumeButton = pauseResumeButtons.resumeButton;
        }

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