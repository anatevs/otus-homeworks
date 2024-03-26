using UnityEngine;

public class UIViewManager : 
    MonoBehaviour,
    IEndGameListener
{
    [SerializeField]
    private EndGameView _endGameView;

    public void OnEndGame()
    {
        _endGameView.Show();
    }
}