using TMPro;
using UnityEngine;

public class BaseHPView : MonoBehaviour
{
    [SerializeField]
    TMP_Text _text;

    public void SetText(TeamType team, int hp)
    {
        _text.text = $"{team} HP: {hp}";
    }
}