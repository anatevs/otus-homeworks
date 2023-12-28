using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class BulletPoolParams
    {
        public Transform worldTransform;

        public Transform poolTransform;
        public int initialCount;
        public Bullet bulletPrefab;
    }
}
