using System;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfoPresenter : ICharacterInfoPresenter
    {
        public event Action<string, int> OnCharacterStatAdd;

        public event Action<string> OnCharacterStatRemove;

        public event Action<string, int> OnCharacterStatChanged;

        public CharacterInfo CharacterInfo { get => _characterInfo; }

        private readonly CharacterInfo _characterInfo;

        public CharacterInfoPresenter(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;

            _characterInfo.OnStatAdded += AddStat;
            _characterInfo.OnStatRemoved += RemoveStat;
            _characterInfo.OnStatChanged += ChangeStatValue;
        }

        public void AddStat(CharacterStat stat)
        {
            OnCharacterStatAdd?.Invoke(stat.Name, stat.Value);
        }

        public void ChangeStatValue(string name, int newValue)
        {
            OnCharacterStatChanged?.Invoke(name, newValue);
        }

        public void RemoveStat(CharacterStat stat)
        {
            OnCharacterStatRemove?.Invoke(stat.Name);
        }

        public void AssingCharacterStats()
        {
            CharacterStat[] stats = _characterInfo.GetStats();
            for (int i = 0; i < stats.Length; i++)
            {
                OnCharacterStatAdd?.Invoke(stats[i].Name, stats[i].Value);
            }
        }

        ~CharacterInfoPresenter()
        {
            _characterInfo.OnStatAdded -= AddStat;
            _characterInfo.OnStatRemoved -= RemoveStat;
            _characterInfo.OnStatChanged -= ChangeStatValue;
        }
    }
}