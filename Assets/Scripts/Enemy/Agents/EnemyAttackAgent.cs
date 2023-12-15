using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour,
        GameListeners.IStartGame,
        GameListeners.IPauseGame,
        GameListeners.IResumeGame,
        GameListeners.IFixedUpdate
    {
        public bool Enabled { get; private set; }
        
        [SerializeField]
        private WeaponComponent _weaponComponent;
        
        [SerializeField]
        private EnemyMoveAgent _moveAgent;
        
        [SerializeField]
        private float _countdown;

        [SerializeField]
        private BulletConfig _bulletConfig;

        private BulletSystem _bulletSystem;
        private GameObject _target;
        private float _currentTime;

        private void Awake()
        {
            Enabled = true;
        }

        public void SetBulletSystem(BulletSystem bulletSystem)
        {
            this._bulletSystem = bulletSystem;
        }
        
        public void SetTarget(GameObject target)
        {
            this._target = target;
        }

        public void OnStart()
        {
            Enabled = true;
        }
        public void OnPause()
        {
            Enabled = false;
        }
        public void OnResume()
        {
            Enabled = true;
        }

        public void OnFixedUpdate()
        {
            if (this._moveAgent == null)
            {
                Debug.Log("there is no EnemyMoveAgent assigned in EnemyAttackAgent!");
                return;
            }

            if (!this._moveAgent.IsReached)
            {
                return;
            }

            if ((this._currentTime -= Time.fixedDeltaTime) <= 0)
            {
                this.Fire();
                ResetTimer();
            }
        }
        
        private void ResetTimer()
        {
            this._currentTime = this._countdown;
        }

        private void Fire()
        {
            if (this._weaponComponent != null && this._target != null)
            {
                var startPosition = this._weaponComponent.Position;
                var vector = (Vector2)this._target.transform.position - startPosition;
                var direction = vector.normalized;
                this._bulletSystem.FlyBulletByArgs(new BulletSystem.BulletArgs
                {
                    isPlayer = false,
                    physicsLayer = (int)this._bulletConfig.physicsLayer,
                    color = this._bulletConfig.color,
                    damage = this._bulletConfig.damage,
                    position = startPosition,
                    velocity = direction * this._bulletConfig.speed
                });
            }
        }
    }
}