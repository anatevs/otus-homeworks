using GameEngine;
using System.Collections.Generic;

public class UnitPrefabsManager
{
    private readonly UnitPrefabsList _unitPrefabs;

    private readonly Dictionary<string, Unit> _prefabsDict = new Dictionary<string, Unit>();

    public UnitPrefabsManager(UnitPrefabsList unitPrefabs)
    {
        _unitPrefabs = unitPrefabs;
        InitPrefabNames();
    }

    private void InitPrefabNames()
    {
        for (int i = 0; i < _unitPrefabs.unitPrefabs.Length; i++)
        {
            _prefabsDict.Add(_unitPrefabs.unitPrefabs[i].Type, _unitPrefabs.unitPrefabs[i]);
        }
    }

    public bool TryGetPrefab(string objectType, out Unit prefab)
    {
        return _prefabsDict.TryGetValue(objectType, out prefab);
    }
}