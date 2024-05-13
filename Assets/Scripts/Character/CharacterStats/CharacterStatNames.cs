using System;
using System.Collections.Generic;

public sealed class CharacterStatNames
{
    private readonly Dictionary<CharacterStat, String> _stats = new();

    public CharacterStatNames()
    {
        foreach (CharacterStat stat in Enum.GetValues(typeof(CharacterStat)))
        {
            _stats.Add(stat, Enum.GetName(typeof(CharacterStat), stat));
        }
    }

    public string GetStatName(CharacterStat stat)
    {
        if (_stats.ContainsKey(stat))
        {
            return _stats[stat];
        }
        else
        {
            throw new Exception($"there is no stat name for stat {stat}");
        }
    }
}