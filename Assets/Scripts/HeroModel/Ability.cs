using UnityEngine;

[CreateAssetMenu(fileName = "New ability", menuName = "Hero model/Create ability", order = 0)]

public class Ability : ScriptableObject
{
    [SerializeReference]
    public IEffect effect;
}