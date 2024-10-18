using System.Collections.Generic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class TreeService : MonoBehaviour
    {
        public IReadOnlyList<GameObject> Trees => _trees;

        [SerializeField]
        private GameObject[] _trees;

        public bool FindClosest(Vector3 position, out GameObject closestResource)
        {
            float minDistance = float.MaxValue;
            closestResource = default;

            for (int i = 0, count = _trees.Length; i < count; i++)
            {
                GameObject resource = _trees[i];
                if (!resource.activeSelf)
                {
                    continue;
                }

                Vector3 resourcePosition = resource.transform.position;
                Vector3 distanceVector = resourcePosition - position;
                distanceVector.y = 0;

                float resourceDistance = distanceVector.sqrMagnitude;
                if (resourceDistance < minDistance)
                {
                    minDistance = resourceDistance;
                    closestResource = resource;
                }
            }

            return closestResource != default;
        }
    }
}