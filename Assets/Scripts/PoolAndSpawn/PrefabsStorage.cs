using UnityEngine;

public class PrefabsStorage : MonoBehaviour
{
    [SerializeField]
    private PrefabParams[] _prefabs;

    public PrefabParams[] GetPrefabs()
    {
        return _prefabs;
    }
}