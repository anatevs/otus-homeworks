using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Hero Model/Create ability", order = 0)]

public class Ability : ScriptableObject
{
    [SerializeReference]
    public IEffect effect;
}