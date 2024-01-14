using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class PoolParams<T>
        where T : MonoBehaviour
    {
        public Transform worldTransform;

        public Transform poolTransform;

        public int initialPoolSize;

        public T prefab;
    }
}