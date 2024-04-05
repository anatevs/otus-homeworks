using UnityEngine;
using TMPro;

public class FinishGameWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private void SetFinishText(TeamType winner)
    {
        _text.text = $"Game is over!\nWinner: {winner} team";
    }

    public void Show(TeamType winner)
    {
        SetFinishText(winner);
        gameObject.SetActive(true);
    }
}