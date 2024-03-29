using UnityEngine;

public class PrefabsStorage : MonoBehaviour
{
    [SerializeField]
    private PrefabAndPoolParams[] _prefabs;

    public PrefabAndPoolParams[] GetPrefabs()
    {
        return _prefabs;
    }
}