using Game.Engine;
using ResourcesStorage;
using UnityEngine;

namespace Game.Content
{
    [RequireComponent(typeof(ResourceStorage))]
    public sealed class Tree : MonoBehaviour
    {
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop");

        public float TreeStopDistance => _treeStopDistance;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _treeStopDistance;

        private ResourceStorage _resourceStorage;

        private void Awake()
        {
            _resourceStorage = GetComponent<ResourceStorage>();
        }

        private void OnEnable()
        {
            _resourceStorage.OnStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _resourceStorage.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (_resourceStorage.IsEmpty)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _animator.Play(ChopAnimHash, -1, 0);
            }
        }
    }
}