using UnityEngine;

namespace ResourcesStorage
{
    [CreateAssetMenu(fileName = "ResourceID",
        menuName = "Configs/ResourceID config")]
    public sealed class ResourceID : ScriptableObject
    {
        public string ID;
    }
}