using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : 
        IFixedUpdate,
        IPausedFixedUpdate

    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;

        private float _positionZ;

        private Transform _myTransform;

        public LevelBackground(LevelBackgroundParams levelBackgroundParams)
        {
            _startPositionY = levelBackgroundParams.startPositionY;
            _endPositionY = levelBackgroundParams.endPositionY;
            _movingSpeedY = levelBackgroundParams.movingSpeedY;
            _myTransform = levelBackgroundParams.levelBackgroundTransform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFixedUpdate()
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        public void OnPausedFixedUpdate()
        {
            return;
        }

        //[Serializable]
        //public sealed class Params
        //{
        //    [SerializeField]
        //    public float startPositionY;

        //    [SerializeField]
        //    public float endPositionY;

        //    [SerializeField]
        //    public float movingSpeedY;
        //}
    }
}