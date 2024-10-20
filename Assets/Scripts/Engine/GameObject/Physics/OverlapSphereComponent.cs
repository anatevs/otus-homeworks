using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    public sealed class OverlapSphereComponent : MonoBehaviour
    {
        private static readonly Collider[] buffer = new Collider[32];

        [SerializeField]
        private Transform _center;

        [SerializeField]
        private float _radius;

        [SerializeField]
        private LayerMask _layerMask;

        [SerializeField]
        private QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

        [Button]
        public void OverlapSphere(Predicate<GameObject> action)
        {
            int size = Physics.OverlapSphereNonAlloc(
                this._center.position,
                this._radius,
                buffer,
                this._layerMask,
                this.queryTriggerInteraction
            );

            for (int i = 0; i < size; i++)
            {
                Collider collider = buffer[i];
                GameObject target = collider.gameObject;
                if (action.Invoke(target))
                {
                    return;
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            if (this._center != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(this._center.position, this._radius);
            }
        }
    }
}