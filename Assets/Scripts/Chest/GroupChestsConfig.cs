using UnityEngine;

namespace Scripts.Chest
{
    [CreateAssetMenu(
        fileName = "GroupChestsConfig",
        menuName = "Configs/New Group Chests Config"
        )]

    public sealed class GroupChestsConfig : ScriptableObject
    {
        public ChestConfig[] Configs;
    }
}