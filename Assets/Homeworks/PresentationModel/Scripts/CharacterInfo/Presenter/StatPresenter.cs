using System;

namespace Lessons.Architecture.PM
{
    public class StatPresenter : IStatPresenter,
        IDisposable
    {
        public event Action OnCharacterStatChanged;
        public string StatText => $"{_characterStat.Name}: {_characterStat.Value}";

        private CharacterStat _characterStat;

        public StatPresenter(CharacterStat characterStat)
        {
            _characterStat = characterStat;
            _characterStat.OnValueChanged += ChangeStat;
        }

        private void ChangeStat(int newValue)
        {
            OnCharacterStatChanged?.Invoke();
        }

        public void Dispose()
        {
            _characterStat.OnValueChanged -= ChangeStat;
        }
    }
}