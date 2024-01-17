using UnityEngine;

namespace ShootEmUp
{
    public class BulletController :
            IPauseGame,
            IResumeGame
    {
        private Bullet _bullet;

        private Vector2 _currVelocity;

        public BulletController(Bullet bullet)
        {
            _bullet = bullet;
        }

        public void OnPause()
        {
            _currVelocity = _bullet.GetCurrentVelocity();
            _bullet.SetVelocity(Vector2.zero);
        }

        public void OnResume()
        {
            _bullet.SetVelocity(_currVelocity);
        }
    }
}