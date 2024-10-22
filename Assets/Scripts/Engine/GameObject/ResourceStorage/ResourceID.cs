using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(fileName = "ResourceID",
        menuName = "Configs/ResourceID config")]
    public sealed class ResourceID : ScriptableObject
    {
        public string ID;
    }
}