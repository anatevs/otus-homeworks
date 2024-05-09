using System;
using System.Collections.Generic;

public sealed class CharacterStatsNames
{
    private readonly Dictionary<CharacterStat, String> _stats = new() 
    {
        {CharacterStat.Speed, "Speed"},
        {CharacterStat.Damage, "Damage"},
        {CharacterStat.Defence, "Defence"}
    };

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