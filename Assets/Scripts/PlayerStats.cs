using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public sealed class PlayerStats
    {
        [SerializeReference, ShowInInspector]
        private readonly Dictionary<PlayerStatType, int> _stats = new();

        public void AddStat(PlayerStatType name, int value)
        {
            _stats.Add(name, value);
        }

        public int GetStat(PlayerStatType name)
        {
            return _stats[name];
        }

        public void SetStat(PlayerStatType name, int value)
        {
            if (_stats.ContainsKey(name))
            {
                _stats[name] = value;
            }
            else
            {
                _stats.Add(name, value);
            }
        }

        public IReadOnlyDictionary<PlayerStatType, int> GetStats()
        {
            return _stats;
        }

        public void RemoveStat(PlayerStatType name)
        {
            _stats.Remove(name);
        }
    }
}