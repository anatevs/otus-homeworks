using System;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(fileName = "HarvestAnimConfig",
        menuName = "Configs/HarvestAnimation")]
    public sealed class HarvestAnimConfig : ScriptableObject
    {
        public IDAnimationNames[] Names;
    }

    [Serializable]
    public struct IDAnimationNames
    {
        public ResourceID IDconfig;
        public string AnimName;
    }
}