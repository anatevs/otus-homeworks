using System;
using System.Collections.Generic;

namespace Game.Engine
{
    [Serializable]
    public sealed class AndCondition
    {
        private readonly List<Func<bool>> _conditions = new();

        private readonly Dictionary<string, List<Func<bool>>> _idConditions = new();

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public void AddCondition(string id, Func<bool> condition)
        {
            if (!_idConditions.ContainsKey(id))
            {
                var conditions = new List<Func<bool>>() {condition};
                _idConditions[id] = conditions;
                return;
            }

            _idConditions[id].Add(condition);
        }

        public bool Invoke()
        {
            foreach (var member in _conditions)
            {
                if (!member.Invoke())
                {
                    return false;
                }
            }

            return true;
        }

        public bool Invoke(string id)
        {
            if (!_idConditions.TryGetValue(id, out var conditions))
            {
                return false;
            }

            foreach (var member in _conditions)
            {
                if (!member.Invoke())
                {
                    return false;
                }
            }

            return true;
        }
    }
}