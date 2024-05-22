using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Sample
{
    //Нельзя менять!
    public sealed class Character
    {
        public event Action OnStateChanged;
        
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<string, float> stats;

        public Character()
        {
            this.stats = new Dictionary<string, float>();
        }

        public Character(params KeyValuePair<string, float>[] stats)
        {
            this.stats = new Dictionary<string, float>(stats);
        }

        public float GetStat(string name)
        {
            return this.stats[name];
        }

        public void SetStat(string name, float value)
        {
            this.stats[name] = value;
            this.OnStateChanged?.Invoke();
        }

        public void RemoveStat(string name)
        {
            if (this.stats.Remove(name))
            {
                this.OnStateChanged?.Invoke();
            }
        }

        public KeyValuePair<string, float>[] GetStats()
        {
            return this.stats.ToArray();
        }
    }
}