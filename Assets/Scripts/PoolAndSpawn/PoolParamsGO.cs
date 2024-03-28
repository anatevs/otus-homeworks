using System;
using UnityEngine;

[Serializable]
public sealed class PoolParamsGO
{
    public Transform worldTransform;

    public Transform poolTransform;

    public GameObject prefab;

    public int initCount;
}