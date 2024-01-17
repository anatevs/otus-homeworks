using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        public bool IsPlayer
        {
            get => _isPlayer;
        }
        
        [SerializeField]
        private bool _isPlayer;
    }
}