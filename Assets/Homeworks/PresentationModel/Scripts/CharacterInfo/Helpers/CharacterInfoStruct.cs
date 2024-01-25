using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public struct CharacterStatStruct
    {
        [SerializeField] public string name;
        [SerializeField] public int value;
    }
}