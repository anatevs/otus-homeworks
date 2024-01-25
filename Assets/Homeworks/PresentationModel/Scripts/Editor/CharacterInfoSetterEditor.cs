using UnityEngine;
using UnityEditor;

namespace Lessons.Architecture.PM
{
    [CustomEditor(typeof(CharacterInfoSetter))]
    public sealed class CharacterInfoSetterEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI();

            var installer = (CharacterInfoSetter)target;

            if (GUILayout.Button("Add Stat"))
            {
                installer.AddStat();
            }

            if (GUILayout.Button("Remove Stat"))
            {
                installer.RemoveStat();
            }

            if (GUILayout.Button("Change Stat"))
            {
                installer.ChangeStat();
            }
        }
    }
}