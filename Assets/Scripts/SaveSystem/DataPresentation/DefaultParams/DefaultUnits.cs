using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaulUnitsParams", menuName = "Configs/DefaultObjectsParams/UnitsParams")]
public class DefaultUnits : ScriptableObject, IDefaultConfig<UnitsParams>
{
    public Unit[] units;

    [ShowInInspector]
    public OneUnitParams[] objectsParams;

    public UnitsParams GetParamsObject()
    {
        UnitsParams defaultUnitsParams = new UnitsParams();
        defaultUnitsParams.SetupParams(objectsParams);
        return defaultUnitsParams;
    }

    [Button]
    public void CopyUnitsDataToParams()
    {
        objectsParams = new OneUnitParams[units.Length];
        for (int i = 0; i < units.Length; i++)
        {
            objectsParams[i] = new OneUnitParams(units[i]);
        }
        units = new Unit[] { };
    }
}