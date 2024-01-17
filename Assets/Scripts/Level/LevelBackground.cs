using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : 
        IFixedUpdate,
        IPausedFixedUpdate

    {
        private readonly float _startPositionY;

        private readonly float _endPositionY;

        private readonly float _movingSpeedY;

        private readonly float _positionX;

        private readonly float _positionZ;

        private readonly Transform _myTransform;

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
    }
}