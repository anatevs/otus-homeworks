using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjectCompoonents
{
    public class Tree : MonoBehaviour
    {
        [SerializeField]
        private string resourceID = "log";

        [SerializeField]
        private int resourceCount = 5;

        private ResourceStorage _resourceStorage;

        private void Awake()
        {
            _resourceStorage = new ResourceStorage(resourceCount, resourceCount);
        }
    }
}