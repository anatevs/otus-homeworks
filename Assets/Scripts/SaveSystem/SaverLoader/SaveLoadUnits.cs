using GameEngine;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class SaveLoadUnits : SaveLoader<UnitsParams, UnitManager>
{
    protected override void SetupParamsData(UnitsParams unitsParams, IObjectResolver context)
    {
        SceneObjectsService sceneObjectsService = context.Resolve<SceneObjectsService>();
        UnitPrefabsManager unitPrefabsManager = context.Resolve<UnitPrefabsManager>();
        UnitManager unitManager = context.Resolve<UnitManager>();

        sceneObjectsService.RemoveSceneObjects<Unit>();
        foreach (OneUnitParams oneUnitParams in unitsParams.Params)
        {
            if (unitPrefabsManager.TryGetPrefab(oneUnitParams.type, out Unit prefab))
            {
                Unit unit = unitManager.SpawnUnit(prefab, oneUnitParams.position, oneUnitParams.rotation);
                unit.HitPoints = oneUnitParams.hitPoints;
            }
            else
            {
                throw new Exception($"there is no unit with type {oneUnitParams.type} in the prefabs list");
            }
        }
    }

    protected override void LoadDefault(IObjectResolver context)
    {
        Dictionary<string, ScriptableObject> defaultConfigs = context.Resolve<Dictionary<string, ScriptableObject>>();

        if (defaultConfigs.TryGetValue(typeof(DefaultUnits).Name, out ScriptableObject SO))
        {
            IDefaultConfig<UnitsParams> defaultUnits = SO as IDefaultConfig<UnitsParams>;
            SetupParamsData(defaultUnits.GetParamsObject(), context);
        }
        else
        {
            Debug.Log($"there is no default config {typeof(DefaultUnits).Name} in defaultConfigs list");
        }
    }

    protected override UnitsParams ConvertDataToParams(UnitManager unitManager)
    {
        var units = unitManager.GetAllUnits();
        UnitsParams unitsParams = new UnitsParams();
        unitsParams.SetupParams(units);

        return unitsParams;
    }
}