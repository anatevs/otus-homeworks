using UnityEngine;

public class EndGameView : MonoBehaviour
{
    [SerializeField]
    private GameObject _endGameView;

    public void Show()
    {
        _endGameView.SetActive(true);
    }
}