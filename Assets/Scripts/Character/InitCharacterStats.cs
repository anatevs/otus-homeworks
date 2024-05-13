using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InitStats",
    menuName = "Sample/new InitStats")]
public sealed class InitCharacterStats : ScriptableObject
{
    [SerializeField]
    private StatStruct[] _statStructs;

    public KeyValuePair<string, int>[] GetInitStats(CharacterStatsNames statsNames)
    {
        KeyValuePair<string, int>[] res = new KeyValuePair<string, int>[_statStructs.Length];

        for (int i = 0; i < _statStructs.Length; i++)
        {
            string statName = statsNames.GetStatName(_statStructs[i].Name);
            res[i] = new KeyValuePair<string, int>(statName, _statStructs[i].Value);
        }

        return res;
    }
}

[Serializable]
public struct StatStruct
{
    [SerializeField]
    public CharacterStat Name;

    [SerializeField]
    public int Value;
}