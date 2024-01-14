using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyPoolParams
    {
        public Transform worldTransform;

        public Transform poolTransform;

        public int poolSize;

        public GameObject enemyPrefab;
    }
}