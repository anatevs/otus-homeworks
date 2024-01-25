using System;
using System.Collections.Generic;
using System.Linq;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfo
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;
        public event Action<string, int> OnStatChanged;

        private readonly HashSet<CharacterStat> _stats = new();

        public void AddStat(CharacterStat stat)
        {
            if (_stats.Add(stat))
            {
                OnStatAdded?.Invoke(stat);
            }
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (_stats.Remove(stat))
            {
                OnStatRemoved?.Invoke(stat);
            }
        }

        public void ChangeStat(string name, int newValue)
        {
            CharacterStat characterStat = GetStat(name);
            characterStat.ChangeValue(newValue);
            OnStatChanged?.Invoke(name, newValue);
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in _stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return _stats.ToArray();
        }
    }
}