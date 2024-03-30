using System;
using UnityEngine;

[Serializable]
public struct PrefabAndPoolParams
{
    public Prefab prefab;
    public ObjectType objectType;
    public Team teamType;
    public Transform poolTransform;
    public Transform worldTransform;
    public int initPoolCount;
}