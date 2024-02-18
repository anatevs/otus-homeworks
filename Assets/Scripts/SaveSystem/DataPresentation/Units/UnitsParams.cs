using GameEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

public class UnitsParams
{
    public List<OneUnitParams> Params 
    {
        get => _unitParamsList;
        private set => _ = _unitParamsList;
    }

    private List<OneUnitParams> _unitParamsList = new List<OneUnitParams>();

    public void AddParams(OneUnitParams unitParams)
    {
        _unitParamsList.Add(unitParams);
    }

    public void SetupParams(IEnumerable<Unit> units)
    {
        foreach (Unit unit in units)
        {
            OneUnitParams unitParams = new OneUnitParams(unit);

            _unitParamsList.Add(unitParams);
        }
    }

    public void SetupParams(IEnumerable<OneUnitParams> unitsParams)
    {
        _unitParamsList.AddRange(unitsParams);
    }
}