using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class LevelBackgroundParams
    {
        public float startPositionY;

        public float endPositionY;

        public float movingSpeedY;

        public Transform levelBackgroundTransform;

    }
}
