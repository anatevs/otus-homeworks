using UnityEditor;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CustomEditor(typeof(CharacterPopupHelper))]
    public sealed class CharacterPopupHelperEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();

            CharacterPopupHelper installer = (CharacterPopupHelper)target;

            if (GUILayout.Button("Show popup"))
            {
                installer.Show();
            }
        }
    }
}