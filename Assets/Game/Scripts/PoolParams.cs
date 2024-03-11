using System;
using UnityEngine;

[Serializable]
public sealed class PoolParams<T> where T : Component
{
    public Transform worldTransform;

    public Transform poolTransform;

    public T prefab;

    public int initCount;
}
