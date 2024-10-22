using ResourcesStorage;
using System;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(fileName = "HarvestAnimConfig",
        menuName = "Configs/HarvestAnimation")]
    public class HarvestAnimConfig : ScriptableObject
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