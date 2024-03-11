using UnityEngine;
using VContainer;

public class UIViewManager : MonoBehaviour
{
    [SerializeField]
    private GameInfoView _gameInfoView;

    private GameInfoPresenter _gameInfoPresenter;

    [Inject]
    public void Construct(GameInfoPresenter gameInfoPresenter)
    {
        _gameInfoPresenter = gameInfoPresenter;
    }

    private void Start()
    {
        _gameInfoView.Show(_gameInfoPresenter);
    }

    private void OnDisable()
    {
        _gameInfoView.Hide();
    }
}