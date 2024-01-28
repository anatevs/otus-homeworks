using System;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPresenter : 
        IPlayerLevelPresenter,
        IDisposable
    {
        public event Action<int, int> OnXPChanged;
        public event Action<int, int, int> OnLevelChanged;
        public event Action OnAvailableLevelUp;

        public int CurrentXP => _playerLevel.CurrentExperience;

        public int RequiredXP => _playerLevel.RequiredExperience;

        public int CurrentLevel => _playerLevel.CurrentLevel;

        public string LevelString => $"Level: {_playerLevel.CurrentLevel}";

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

        public void Dispose()
        {
            _playerLevel.OnExperienceChanged -= ChangeXP;
            _playerLevel.OnLevelUp -= ChangeLevel;
        }
    }
}