using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class UIBackgroundListeners : MonoBehaviour,
        GameListeners.IStartGame,
        GameListeners.IFinishGame,
        GameListeners.IPauseGame,
        GameListeners.IResumeGame
    {

        public void OnStart()
        {
            gameObject.SetActive(false);
        }

        public void OnFinish()
        {
            gameObject.SetActive(true);
        }

        public void OnPause()
        {
            gameObject?.SetActive(true);
        }

        public void OnResume()
        {
            gameObject.SetActive(false);
        }

    }
}
