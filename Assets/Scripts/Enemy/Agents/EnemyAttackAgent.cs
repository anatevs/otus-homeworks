using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;

        private GameObject target;
        private float currentTime;

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        private void FixedUpdate()
        {
            if (this.moveAgent == null) 
            {
                Debug.Log("there is no EnemyMoveAgent assigned in EnemyAttackAgent!");
                return;
            }

            if (!this.moveAgent.IsReached)
            {
                return;
            }

            if ((this.currentTime -= Time.fixedDeltaTime) <= 0)
            {
                this.Fire();
                ResetTimer();
            }
        }

        private void ResetTimer()
        {
            this.currentTime = this.countdown;
        }

        private void Fire()
        {
            if (this.weaponComponent != null && this.target != null)
            {
                var startPosition = this.weaponComponent.Position;
                var vector = (Vector2)this.target.transform.position - startPosition;
                var direction = vector.normalized;
                this.OnFire?.Invoke(startPosition, direction);
            }
        }
    }
}