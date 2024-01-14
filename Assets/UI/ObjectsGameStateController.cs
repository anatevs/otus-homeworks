using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class ObjectsGameStateController : MonoBehaviour,
        IStartGame,
        IPauseGame,
        IResumeGame,
        IFinishGame
    {
        [SerializeField]
        private GameState[] _activeGameStates = { GameState.Playing };

        public void OnStart()
        {
            gameObject.SetActive(_activeGameStates.Contains(GameState.Playing));
        }
        public void OnPause()
        {
            gameObject.SetActive(_activeGameStates.Contains(GameState.Paused));
        }

        public void OnResume()
        {
            gameObject.SetActive(_activeGameStates.Contains(GameState.Playing));
        }
        public void OnFinish()
        {
            gameObject.SetActive(_activeGameStates.Contains(GameState.Finished));
        }
    }
}