using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PauseButtonListeners : MonoBehaviour,
        GameListeners.IStartGame,
        GameListeners.IFinishGame,
        GameListeners.IPauseGame,
        GameListeners.IResumeGame
    {

        public void OnStart()
        {
            gameObject.SetActive(true);
        }

        public void OnFinish()
        {
            gameObject.SetActive(false);
        }

        public void OnPause()
        {
            gameObject?.SetActive(false);
        }

        public void OnResume()
        {
            gameObject.SetActive(true);
        }

    }
}
