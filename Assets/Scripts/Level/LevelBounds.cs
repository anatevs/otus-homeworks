using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        private readonly Transform _leftBorder;

        private readonly Transform _rightBorder;

        private readonly Transform _bottomBorder;

        private readonly Transform _topBorder;

        public LevelBounds(LevelBordersStorage levelBordersStorage)
        {
            _leftBorder = levelBordersStorage.leftBorder;
            _rightBorder = levelBordersStorage.rightBorder;
            _bottomBorder = levelBordersStorage.bottomBorder;
            _topBorder = levelBordersStorage.topBorder;
        }

        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > _leftBorder.position.x
                   && positionX < _rightBorder.position.x
                   && positionY > _bottomBorder.position.y
                   && positionY < _topBorder.position.y;
        }
    }
}