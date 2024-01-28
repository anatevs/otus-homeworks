using System;

namespace Lessons.Architecture.PM
{
    public interface IPlayerLevelPresenter : IPresenter
    {
        event Action<int, int> OnXPChanged;
        event Action<int, int, int> OnLevelChanged;
        event Action OnAvailableLevelUp;

        public int CurrentXP { get; }
        public int RequiredXP { get; }
        public int CurrentLevel { get; }
        public string LevelString { get; }

        public void LevelUp();
    }
}