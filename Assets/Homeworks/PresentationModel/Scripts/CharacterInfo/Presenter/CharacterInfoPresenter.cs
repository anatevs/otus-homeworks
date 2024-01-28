using System;
using System.Collections.Generic;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfoPresenter : 
        ICharacterInfoPresenter,
        IDisposable
    {
        public event Action<string, int, IStatPresenter> OnCharacterStatAdd;

        public event Action<string> OnCharacterStatRemove;

        public event Action<string, int> OnCharacterStatChanged;

        public CharacterInfo CharacterInfo { get => _characterInfo; }

        private readonly CharacterInfo _characterInfo;

        private Dictionary<CharacterStat, IStatPresenter> _statPresenters = new();

        public CharacterInfoPresenter(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;

            _characterInfo.OnStatAdded += AddStat;
            _characterInfo.OnStatRemoved += RemoveStat;
            _characterInfo.OnStatChanged += ChangeStatValue;
        }

        public void AddStat(CharacterStat stat)
        {
            StatPresenter statPresenter = new StatPresenter(stat);
            _statPresenters.Add(stat, statPresenter);
            OnCharacterStatAdd?.Invoke(stat.Name, stat.Value, statPresenter);
        }

        public void ChangeStatValue(string name, int newValue)
        {
            OnCharacterStatChanged?.Invoke(name, newValue);
        }

        public void RemoveStat(CharacterStat stat)
        {
            _statPresenters[stat].Dispose();
            _statPresenters.Remove(stat);
            OnCharacterStatRemove?.Invoke(stat.Name);
        }

        public void AssingCharacterStats()
        {
            CharacterStat[] stats = _characterInfo.GetStats();
            for (int i = 0; i < stats.Length; i++)
            {
                OnCharacterStatAdd?.Invoke(stats[i].Name, stats[i].Value, _statPresenters[stats[i]]);
            }
        }

        public void Dispose()
        {
            _characterInfo.OnStatAdded -= AddStat;
            _characterInfo.OnStatRemoved -= RemoveStat;
            _characterInfo.OnStatChanged -= ChangeStatValue;
        }
    }
}