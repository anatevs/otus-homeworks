using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Hero Model", order = 0)]

public class Ability : ScriptableObject
{
    [SerializeReference]
    public IEffect effect;
}