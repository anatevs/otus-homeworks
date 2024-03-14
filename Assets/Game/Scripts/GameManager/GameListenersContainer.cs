using System.Collections.Generic;
using UnityEngine;

public class GameListenersContainer
{
    private List<IGameListener> _gameListeners = new List<IGameListener>();

    public void AddListener(IGameListener listener)
    {
        _gameListeners.Add(listener);
    }

    public void AddListeners(IEnumerable<IGameListener> listeners)
    {
        foreach (IGameListener listener in listeners)
        {
            AddListener(listener);
        }
    }

    public void AddListener(GameObject gameObject)
    {
        IGameListener[] listeners = gameObject.GetComponentsInChildren<IGameListener>();
        AddListeners(listeners);
    }

    public void AddListeners(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            AddListener(gameObject);
        }
    }

    public void RemoveListener(IGameListener listener)
    {
        _gameListeners.Remove(listener);
    }

    public void RemoveListeners(IEnumerable<IGameListener> listeners)
    {
        foreach (IGameListener listener in listeners)
        {
            RemoveListener(listener);
        }
    }

    public void FinishListeners()
    {
        for (int i = 0; i < _gameListeners.Count; i++)
        {
            if (_gameListeners[i] is IFinishGameListener finishListener)
            {
                finishListener.OnFinishGame();
            }
        }
    }
}