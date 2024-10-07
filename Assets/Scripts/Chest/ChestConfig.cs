using System;
using UnityEngine;

namespace Scripts.Chest
{
    [CreateAssetMenu(
        fileName = "ChestConfig",
        menuName = "Configs/New Chest Configs"
        )]
    public sealed class ChestConfig : ScriptableObject
    {
        public ChestParams Params;
    }
}