using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class LevelBordersStorage
    {
        [SerializeField]
        public Transform leftBorder;

        [SerializeField]
        public Transform rightBorder;

        [SerializeField]
        public Transform bottomBorder;

        [SerializeField]
        public Transform topBorder;
    }
}