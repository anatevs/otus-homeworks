using GameEngine;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsList", menuName = "Configs/PrefabsList/Units")]
public class UnitPrefabsList : ScriptableObject
{
    public Unit[] unitPrefabs;
}