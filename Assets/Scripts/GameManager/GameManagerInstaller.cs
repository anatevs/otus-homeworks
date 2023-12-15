using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    
    public class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private GameObject[] sceneObjectsWithBehaviour;

        private GameManager _gameManager;

        private void Awake()
        {                
            if (TryGetComponent<GameManager>(out _gameManager)) 
            {
                AddObjectGameListeners(gameObject, false);

                for (int i = 0; i < sceneObjectsWithBehaviour.Length; i++)
                {
                    AddObjectGameListeners(sceneObjectsWithBehaviour[i], true);
                }
            }
        }

        public void AddObjectGameListeners(GameObject go, bool includeInactive)
        {
            var listeners = go.GetComponentsInChildren<GameListeners.IGameListener>(includeInactive);
            if (listeners != null && listeners.Length != 0)
            {
                foreach (var listener in listeners)
                {
                    _gameManager.AddGameListener(listener);
                }
            }
        }

        public void RemoveObjectGameListeners(GameObject go)
        {
            var listeners = go.GetComponentsInChildren<GameListeners.IGameListener>(true);
            if (listeners != null && listeners.Length != 0)
            {
                foreach (var listener in listeners)
                {
                    _gameManager.RemoveGameListener(listener);
                }
            }
        }

    }
}