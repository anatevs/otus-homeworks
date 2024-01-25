using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelSetter : MonoBehaviour
    {
        [SerializeField]
        private int _XPToAdd;

        private PlayerLevel _playerLevel;

        [Inject]
        public void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public void PrintLevelAndXP()
        {
            Debug.Log($"Level: {_playerLevel.CurrentLevel}," +
                $" CurrentXP: {_playerLevel.CurrentExperience}," +
                $" RequiredXP: {_playerLevel.RequiredExperience}");
        }

        public void AddExperience()
        {
            _playerLevel.AddExperience(_XPToAdd);
        }
    }
}
