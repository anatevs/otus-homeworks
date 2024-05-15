using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InitStats",
    menuName = "Sample/new InitStats")]
public sealed class InitCharacterStats : ScriptableObject
{
    [SerializeField]
    private StatStruct[] _statStructs;

    public KeyValuePair<string, int>[] GetInitStats(CharacterStatNames statsNames)
    {
        KeyValuePair<string, int>[] res = new KeyValuePair<string, int>[_statStructs.Length];

        for (int i = 0; i < _statStructs.Length; i++)
        {
            string statName = statsNames.GetName(_statStructs[i].Name);
            res[i] = new KeyValuePair<string, int>(statName, _statStructs[i].Value);
        }

        return res;
    }
}