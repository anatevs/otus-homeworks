using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class EnemyPoolParams
    {
        public Transform worldTransform;
        public Transform poolTransform;
        public int poolSize;
        public GameObject enemyPrefab;
    }
}
