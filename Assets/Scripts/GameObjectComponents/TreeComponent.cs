using ResourcesStorage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameObjectCompoonents
{
    public sealed class TreeComponent : MonoBehaviour
    {
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop");

        public float TreeStopDistance => _treeStopDistance;

        [ShowInInspector]
        private ResourceStorage _resourceStorage;

        [SerializeField]
        private ResourceStorageConfig _config;

        [SerializeField]
        private float _treeStopDistance;

        private void Awake()
        {
            _resourceStorage = new ResourceStorage(_config.Info.Capacity, _config.Info.Count);
        }

        //private void OnEnable()
        //{
        //    this.storage.OnStateChanged += this.OnStateChanged;
        //}

        //private void OnDisable()
        //{
        //    this.storage.OnStateChanged -= this.OnStateChanged;
        //}

        //private void OnStateChanged()
        //{
        //    if (this.storage.IsEmpty())
        //    {
        //        this.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        _animator.Play(ChopAnimHash, -1, 0);
        //    }
        //}
    }
}