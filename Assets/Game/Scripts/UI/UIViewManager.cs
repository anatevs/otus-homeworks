using UnityEngine;
using VContainer;

public class UIViewManager : 
    MonoBehaviour,
    IEndGameListener
{
    [SerializeField]
    private GameInfoView _gameInfoView;

    [SerializeField]
    private EndGameView _endGameView;

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

    public void OnEndGame()
    {
        _endGameView.Show();
    }
}