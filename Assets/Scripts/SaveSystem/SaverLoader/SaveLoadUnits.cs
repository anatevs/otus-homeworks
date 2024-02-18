using GameEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadUnits : SaveLoader<UnitsParams, UnitManager>
{
    private readonly UnitManager _unitManager;
    private readonly UnitPrefabsManager _unitPrefabsManager;
    private readonly OnSceneObjectsService _objectsOnScene;
    
    private readonly Dictionary<string, ScriptableObject> _defaultConfigs;

    public SaveLoadUnits(UnitManager unitManager, 
        UnitPrefabsManager unitPrefabsManager, 
        OnSceneObjectsService objectsOnScene, 
        Dictionary<string, ScriptableObject> defaultConfigs)
    {
        _unitManager = unitManager;
        _unitPrefabsManager = unitPrefabsManager;
        _objectsOnScene = objectsOnScene;
        _defaultConfigs = defaultConfigs;
    }

    protected override void SetupParamsData(UnitsParams unitsParams)
    {
        _objectsOnScene.RemoveSceneObjects<Unit>();
        foreach (OneUnitParams oneUnitParams in unitsParams.Params)
        {
            if (_unitPrefabsManager.TryGetPrefab(oneUnitParams.type, out Unit prefab))
            {
                Unit unit = _unitManager.SpawnUnit(prefab, oneUnitParams.position, oneUnitParams.rotation);
                unit.HitPoints = oneUnitParams.hitPoints;
            }
            else
            {
                throw new Exception($"there is no unit with type {oneUnitParams.type} in the prefabs list");
            }
        }
    }

    protected override void LoadDefault()
    {
        if (_defaultConfigs.TryGetValue(typeof(DefaultUnits).Name, out ScriptableObject SO))
        {
            IDefaultConfig<UnitsParams> defaultUnits = SO as IDefaultConfig<UnitsParams>;
            SetupParamsData(defaultUnits.GetParamsObject());
        }
        else
        {
            Debug.Log($"there is no default config {typeof(DefaultUnits).Name} in defaultConfigs list");
        }
    }

    protected override UnitsParams ConvertDataToParams()
    {
        var units = _unitManager.GetAllUnits();
        UnitsParams unitsParams = new UnitsParams();
        unitsParams.SetupParams(units);

        return unitsParams;
    }
}