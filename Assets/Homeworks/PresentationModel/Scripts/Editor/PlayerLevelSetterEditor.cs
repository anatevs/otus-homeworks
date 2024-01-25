using UnityEngine;
using UnityEditor;

namespace Lessons.Architecture.PM
{
    [CustomEditor(typeof(PlayerLevelSetter))]
    public class PlayerLevelSetterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var installer = (PlayerLevelSetter)target;

            if (GUILayout.Button("Add experience"))
            {
                installer.AddExperience();
            }

            if (GUILayout.Button("Print level and XP"))
            {
                installer.PrintLevelAndXP();
            }
        }
    }
}