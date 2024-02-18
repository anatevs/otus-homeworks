using GameEngine;
using System;
using UnityEngine;

[Serializable]
public struct OneUnitParams
{
    public string type;

    public Vector3 position;

    public Quaternion rotation;

    public int hitPoints;

    public OneUnitParams(Unit unit)
    {
        type = unit.Type;
        position = unit.Position;
        rotation = unit.transform.rotation;
        hitPoints = unit.HitPoints;
    }
}