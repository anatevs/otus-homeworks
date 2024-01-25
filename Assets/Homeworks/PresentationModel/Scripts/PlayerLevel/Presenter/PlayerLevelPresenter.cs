using System;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPresenter : IPlayerLevelPresenter
    {
        public event Action<int, int> OnXPChanged;
        public event Action<int, int, int> OnLevelChanged;
        public event Action OnAvailableLevelUp;

        public int CurrentXP 
        { 
            get => _playerLevel.CurrentExperience;
            private set => _ = _playerLevel.CurrentExperience; 
        }
        public int RequiredXP 
        { 
            get => _playerLevel.RequiredExperience;
            private set => _ = _playerLevel.RequiredExperience;
        }
        public int CurrentLevel 
        { 
            get => _playerLevel.CurrentLevel;
            private set => _ = _playerLevel.CurrentLevel;
        }

        private readonly PlayerLevel _playerLevel;

        public PlayerLevelPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _playerLevel.OnExperienceChanged += ChangeXP;
            _playerLevel.OnLevelUp += ChangeLevel;
        }

        public void LevelUp()
        {
            _playerLevel.LevelUp();
        }

        private void ChangeXP(int newXP)
        {
            OnXPChanged?.Invoke(newXP, _playerLevel.RequiredExperience);
            if (_playerLevel.CanLevelUp())
            {
                OnAvailableLevelUp?.Invoke();
            }
        }

        private void ChangeLevel()
        {
            OnLevelChanged?.Invoke(_playerLevel.CurrentExperience, 
                _playerLevel.RequiredExperience, _playerLevel.CurrentLevel);
        }

        ~PlayerLevelPresenter()
        {
            _playerLevel.OnExperienceChanged -= ChangeXP;
            _playerLevel.OnLevelUp -= ChangeLevel;
        }
    }
}