using System;
using UnityEngine;

namespace Scripts.Chest
{
    [CreateAssetMenu(
        fileName = "ChestConfig",
        menuName = "Configs/New Chest Configs"
        )]
    public class ChestConfig : ScriptableObject
    {
        public ChestParams Params;

        [SerializeField]
        private Chest _prefab;
    }
}