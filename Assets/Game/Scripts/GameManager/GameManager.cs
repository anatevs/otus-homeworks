using UnityEngine;
using VContainer;

public class GameManager : MonoBehaviour
{
    private GameListenersContainer _gameListenersContainer;

    [Inject]
    public void Construct(GameListenersContainer gameListenersContainer)
    {
        _gameListenersContainer = gameListenersContainer;
    }

    private void Awake()
    {
        GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
        _gameListenersContainer.AddListeners(rootGameObjects);
    }

    public void FinishGame()
    {
        _gameListenersContainer.FinishListeners();
    }
}